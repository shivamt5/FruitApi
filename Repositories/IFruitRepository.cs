using FruitApi.Models.Domain;

namespace FruitApi.Repositories
{
    public interface IFruitRepository
    {
        Task<List<Fruit>> GetAllAsync();
        Task<Fruit> CreateAsync(Fruit fruit);
        Task<Fruit?> GetByIdAsync(Guid id);
        Task<Fruit?> Deleteasync(Guid id);
        Task<Fruit?> GetByNameAsync(string name);
    }
}
