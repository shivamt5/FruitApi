using AutoMapper;
using FruitApi.Models.DTOs;
using FruitApi.Queries;
using FruitApi.Repositories;
using MediatR;

namespace FruitApi.Handlers
{
    public class GetByNameHandler : IRequestHandler<GetByNameQuery, FruitDto>
    {
        private readonly IFruitRepository _fruitRepository;
        private readonly IMapper _mapper;

        public GetByNameHandler(IFruitRepository fruitRepository, IMapper mapper)
        {
            _fruitRepository = fruitRepository;
            _mapper = mapper;
        }
        public async Task<FruitDto> Handle(GetByNameQuery request, CancellationToken cancellationToken)
        {
            var fruitDomain = await _fruitRepository.GetByNameAsync(request.Name);

            if (fruitDomain == null)
            {
                return null;
            }

            return _mapper.Map<FruitDto>(fruitDomain);
        }
    }
}
