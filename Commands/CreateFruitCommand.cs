using FruitApi.Models.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FruitApi.Commands
{
    public class CreateFruitCommand : IRequest<FruitDto>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Family { get; set; }
        [JsonIgnore]
        public DateTime DateCreated { get; set; }
        [JsonIgnore]
        public DateTime? DateDeleted { get; set; } = null;
        [JsonIgnore]
        public bool isDeleted { get; set; } = false;
    }
}
