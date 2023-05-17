namespace FruitApi.Models.DTOs
{
    public class FruitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; } = null;
        public bool isDeleted { get; set; } = false;
    }
}
