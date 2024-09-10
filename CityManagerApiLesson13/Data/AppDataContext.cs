using CityManagerApiLesson13.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityManagerApiLesson13.Data;
public class AppDataContext : DbContext
{
    // parametric constructor for injecting : 
    public AppDataContext(DbContextOptions<AppDataContext> opt) : base(opt) { }

    // tables for DB : 
    public DbSet<City> Cities { get; set; }
    public DbSet<User> Users { get; set; }  
    public DbSet<CityImage> CityImages { get; set; }
}
