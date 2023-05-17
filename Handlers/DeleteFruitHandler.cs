using AutoMapper;
using FruitApi.Commands;
using FruitApi.Models.DTOs;
using FruitApi.Repositories;
using MediatR;

namespace FruitApi.Handlers
{
    public class DeleteFruitHandler : IRequestHandler<DeleteFruitCommand, FruitDto>
    {
        private readonly IFruitRepository _fruitRepository;
        private readonly IMapper _mapper;

        public DeleteFruitHandler(IFruitRepository fruitRepository, IMapper mapper)
        {
            _fruitRepository = fruitRepository;
            _mapper = mapper;
        }

        public async Task<FruitDto> Handle(DeleteFruitCommand request, CancellationToken cancellationToken)
        {
            var fruitDomainModel = await _fruitRepository.Deleteasync(request.Id);

            //if (fruitDomainModel == null)
            //{
            //    return null;
            //}

            var fruitDto = _mapper.Map<FruitDto>(fruitDomainModel);

            return fruitDto;
        }
    }
}
