
using Microsoft.EntityFrameworkCore;
using SmartHome.Models.Auth;

namespace SmartHome.Context
{
    public class SmartHomeContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public SmartHomeContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DbConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey("UserId");
        }

        public DbSet<User> Users { get; set; }
    }
}
