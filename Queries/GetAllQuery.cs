using FruitApi.Models.DTOs;
using MediatR;

namespace FruitApi.Queries
{
    public class GetAllQuery : IRequest<List<FruitDto>>
    {
    }
}
