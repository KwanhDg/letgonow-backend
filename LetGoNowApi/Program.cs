using LetGoNowApi;
using Microsoft.EntityFrameworkCore;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Đăng ký DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}
Console.WriteLine($"Connection String: {connectionString}");
builder.Services.AddDbContext<LetGoNowDbContext>(options =>
    options.UseNpgsql(connectionString));

// Thêm Supabase Client
var supabaseUrl = "https://yhrbfhguyvswticgjznl.supabase.co"; // Thay bằng Supabase URL của bạn
var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InlocmJmaGd1eXZzd3RpY2dqem5sIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDI5OTM5OTIsImV4cCI6MjA1ODU2OTk5Mn0.Oh9YV7jalOkqtFGtjxU-8sn5zw72WbgoX1S8Ll9LI4Q"; // Thay bằng Supabase anon key của bạn
var supabaseClient = new Supabase.Client(supabaseUrl, supabaseKey);
builder.Services.AddSingleton(supabaseClient);

// Thêm CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Sử dụng CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();