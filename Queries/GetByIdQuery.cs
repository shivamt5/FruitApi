using FruitApi.Models.DTOs;
using MediatR;

namespace FruitApi.Queries
{
    public class GetByIdQuery : IRequest<FruitDto>
    {
        public Guid Id { get; }

        public GetByIdQuery(Guid id) 
        {
            Id = id;
        }
    }
}
