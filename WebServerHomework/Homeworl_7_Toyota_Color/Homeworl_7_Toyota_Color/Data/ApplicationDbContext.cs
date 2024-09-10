using Homeworl_7_Toyota_Color.Models.Toyota;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Homeworl_7_Toyota_Color.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<ColorModel> Color { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}