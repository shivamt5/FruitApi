using AutoMapper;
using FruitApi.Models.DTOs;
using FruitApi.Queries;
using FruitApi.Repositories;
using MediatR;

namespace FruitApi.Handlers
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, FruitDto>
    {
        private readonly IFruitRepository _fruitRepository;
        private readonly IMapper _mapper;

        public GetByIdHandler(IFruitRepository fruitRepository, IMapper mapper)
        {
            _fruitRepository = fruitRepository;
            _mapper = mapper;
        }
        public async Task<FruitDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var fruitDomain = await _fruitRepository.GetByIdAsync(request.Id);

            if (fruitDomain == null)
            {
                return null;
            }

            return _mapper.Map<FruitDto>(fruitDomain);
        }
    }
}
