using Homework_links_6.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Homework_links_6.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<CategoryModel> Categories { get; set; }
         public DbSet<PostModel> Posts { get; set; }
         public DbSet<TagModel> Tags { get; set; }
}