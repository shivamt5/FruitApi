using FruitApi.Models.DTOs;
using MediatR;

namespace FruitApi.Commands
{
    public class DeleteFruitCommand : IRequest<FruitDto>
    {
        public Guid Id { get; }

        public DeleteFruitCommand(Guid id)
        {
            Id = id;
        }
    }
}
