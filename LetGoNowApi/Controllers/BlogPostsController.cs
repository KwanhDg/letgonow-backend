using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LetGoNowApi.Models;

namespace LetGoNowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly LetGoNowDbContext _context;

        public BlogPostsController(LetGoNowDbContext context)
        {
            _context = context;
        }

        // GET: api/BlogPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts()
        {
            return await _context.BlogPosts
                .Include(bp => bp.Author)
                .ToListAsync();
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts
                .Include(bp => bp.Author)
                .FirstOrDefaultAsync(bp => bp.Id == id);

            if (blogPost == null) return NotFound();
            return blogPost;
        }

        // POST: api/BlogPosts
        [HttpPost]
        public async Task<ActionResult<BlogPost>> CreateBlogPost(BlogPostDto blogPostDto)
        {
            var blogPost = new BlogPost
            {
                Title = blogPostDto.Title,
                Content = blogPostDto.Content,
                AuthorId = blogPostDto.AuthorId,
                Tags = blogPostDto.Tags,
                CreatedAt = DateTime.UtcNow
            };

            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();

            // Tải lại blog post với thông tin tác giả
            var createdBlogPost = await _context.BlogPosts
                .Include(bp => bp.Author)
                .FirstOrDefaultAsync(bp => bp.Id == blogPost.Id);

            return CreatedAtAction(nameof(GetBlogPost), new { id = createdBlogPost.Id }, createdBlogPost);
        }

        // PUT: api/BlogPosts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(int id, BlogPostDto blogPostDto)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null) return NotFound();

            blogPost.Title = blogPostDto.Title;
            blogPost.Content = blogPostDto.Content;
            blogPost.AuthorId = blogPostDto.AuthorId;
            blogPost.Tags = blogPostDto.Tags;
            blogPost.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BlogPosts.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null) return NotFound();

            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class BlogPostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public string Tags { get; set; }
    }
}