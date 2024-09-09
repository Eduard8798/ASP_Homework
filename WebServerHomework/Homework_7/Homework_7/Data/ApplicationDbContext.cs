using Homework_7.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Homework_7.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<AreaModel> Areas { get; set; }
    public DbSet<CityModel> Cities { get; set; }
    public DbSet<CityTypeModel> CityTypes { get; set; }
    public DbSet<StreetModel> Streets { get; set; }

}