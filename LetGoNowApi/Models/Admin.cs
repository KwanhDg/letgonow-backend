using Postgrest.Attributes;
using Postgrest.Models;

namespace LetGoNowApi.Models
{
    [Table("Admins")]
    public class Admin : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("role")]
        public string Role { get; set; }
    }
}