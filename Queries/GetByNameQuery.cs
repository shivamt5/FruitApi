using FruitApi.Models.DTOs;
using MediatR;

namespace FruitApi.Queries
{
    public class GetByNameQuery : IRequest<FruitDto>
    {
        public string Name { get; }

        public GetByNameQuery(string name)
        {
            Name = name;
        }
    }
}
