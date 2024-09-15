using Homerwork_7_dolly_17_08.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Homerwork_7_dolly_17_08.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<DollyClothesModel> DollyClothes { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}