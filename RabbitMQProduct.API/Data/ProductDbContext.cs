using Microsoft.EntityFrameworkCore;
using RabbitMQProduct.API.Models;

namespace RabbitMQProduct.API.Data
{
    public class ProductDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public ProductDbContext(IConfiguration configuration) => _configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Product> Products { get; set; }
    }
}
