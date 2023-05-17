using FruitApi.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FruitApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options)
        {
            
        }

        public DbSet<Fruit> Fruits { get; set; }
    }
}
