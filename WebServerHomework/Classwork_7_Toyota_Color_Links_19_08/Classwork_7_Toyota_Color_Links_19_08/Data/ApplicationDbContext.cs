using Classwork_7_Toyota_Color_Links_19_08.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Classwork_7_Toyota_Color_Links_19_08.Data;

public class ApplicationDbContext : IdentityDbContext
{
    
    public DbSet<ColorModel> Color { get; set; }
     
    
    public DbSet<ConfigurationColorsModel> ConfigurationColors { get; set; }
    
    public DbSet<ConfigurationModel> Configurations { get; set; }
    
    public DbSet<ToyotaModel> Toyota { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}