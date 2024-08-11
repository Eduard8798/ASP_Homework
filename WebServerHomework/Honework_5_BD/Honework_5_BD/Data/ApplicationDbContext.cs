using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Honework_5_BD.Models.Entities;
using Honework_5_BD.Models.PhoneNumber;

namespace Honework_5_BD.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<SubscribeModel> Subscribers { get; set; }
    public DbSet<NumberBookModel> NumberBooks { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}