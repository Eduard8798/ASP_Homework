using Honework_5_BD.Models.Category;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Honework_5_BD.Models.Entities;
using Honework_5_BD.Models.PhoneNumber;
using Honework_5_BD.Models.Tag;
using Honework_5_BD.Models.Vender;

namespace Honework_5_BD.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<SubscribeModel> Subscribers { get; set; }
    public DbSet<NumberBookModel> NumberBooks { get; set; }
    
    public DbSet<CategoryModel> Category { get; set; }
    
    public DbSet<TagModel> Tag { get; set; }
    
    public DbSet<VenderModel> Vender { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}