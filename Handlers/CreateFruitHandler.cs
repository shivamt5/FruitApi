using AutoMapper;
using FruitApi.Commands;
using FruitApi.Models.Domain;
using FruitApi.Models.DTOs;
using FruitApi.Repositories;
using MediatR;

namespace FruitApi.Handlers
{
    public class CreateFruitHandler : IRequestHandler<CreateFruitCommand, FruitDto>
    {
        private readonly IFruitRepository _fruitRepository;
        private readonly IMapper _mapper;

        public CreateFruitHandler(IFruitRepository fruitRepository, IMapper mapper)
        {
            _fruitRepository = fruitRepository;
            _mapper = mapper;
        }
        public async Task<FruitDto> Handle(CreateFruitCommand request, CancellationToken cancellationToken)
        {
            var fruitDomainModel = _mapper.Map<Fruit>(request);

            fruitDomainModel = await _fruitRepository.CreateAsync(fruitDomainModel);

            var fruitDto =  _mapper.Map<FruitDto>(fruitDomainModel);

            return (fruitDto);
        }
    }
}
