using Microsoft.EntityFrameworkCore;
using YoussofPortfolio.API.Models;

namespace YoussofPortfolio.API.Database;

public class DataContext : DbContext
{
    protected readonly IConfiguration _configuration;

    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<Project> Project { get; set; }
}