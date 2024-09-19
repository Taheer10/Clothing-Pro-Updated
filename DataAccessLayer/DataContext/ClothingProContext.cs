using ClothingPro.DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace ClothingPro.DataAccessLayer.DataContext;
public class ClothingProContext : DbContext
{

    public ClothingProContext()
    {
    }
    public ClothingProContext(DbContextOptions<ClothingProContext> options) : base(options)
    {

    }
    // public DbSet<Staff> Staff { get; set; }
    public DbSet<Stock> Stock { get; set; }
    public DbSet<MenuHeader> MenuHeader { get; set; }
    public DbSet<Company> Company { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            object value = optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
