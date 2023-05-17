using FruitApi.Data;
using FruitApi.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FruitApi.Repositories
{
    public class FruitRepository : IFruitRepository
    {
        private readonly ApiDbContext _context;
        public FruitRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Fruit> CreateAsync(Fruit fruit)
        {
            fruit.Id = Guid.NewGuid();
            fruit.DateCreated = DateTime.Now;
            await _context.AddAsync(fruit);
            await _context.SaveChangesAsync();
            return fruit;
        }

        public async Task<Fruit?> Deleteasync(Guid id)
        {
            var existingFruit = await _context.Fruits.FirstOrDefaultAsync(x => x.Id == id);

            if (existingFruit == null || existingFruit.isDeleted == true)
            {
                return null;
            }

            existingFruit.isDeleted = true;
            existingFruit.DateDeleted = DateTime.Now;
            await _context.SaveChangesAsync();
            return existingFruit;
        }

        public async Task<List<Fruit>> GetAllAsync()
        {
            var fruits = _context.Fruits;

            return await fruits.ToListAsync();
        }

        public async Task<Fruit?> GetByIdAsync(Guid id)
        {
            return await _context.Fruits.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Fruit?> GetByNameAsync(string name)
        {
            return await _context.Fruits.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
