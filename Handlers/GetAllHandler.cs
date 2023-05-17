using AutoMapper;
using FruitApi.Models.DTOs;
using FruitApi.Queries;
using FruitApi.Repositories;
using MediatR;

namespace FruitApi.Handlers
{
    public class GetAllHandler : IRequestHandler<GetAllQuery, List<FruitDto>>
    {
        private readonly IFruitRepository _fruitRepository;
        private readonly IMapper _mapper;
        public GetAllHandler(IFruitRepository fruitRepository, IMapper mapper)
        {
            _fruitRepository = fruitRepository;
            _mapper = mapper;
        }
        public async Task<List<FruitDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var fruitsDomain = await _fruitRepository.GetAllAsync();

            return _mapper.Map<List<FruitDto>>(fruitsDomain);
        }
    }
}
