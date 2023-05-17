namespace FruitApi.Models.Domain
{
    public class Fruit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; } = null;
        public bool isDeleted { get; set; } = false;
    }
}
