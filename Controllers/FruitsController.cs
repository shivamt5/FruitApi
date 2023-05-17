using AutoMapper;
using FruitApi.CustomActionFilters;
using FruitApi.Data;
using FruitApi.Models.Domain;
using FruitApi.Models.DTOs;
using FruitApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FruitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IFruitRepository _fruitRepository;
        private readonly IMapper _mapper;
        public FruitsController(ApiDbContext context, IFruitRepository fruitRepository, IMapper mapper)
        {
            _context = context;
            _fruitRepository = fruitRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fruitsDomain = await _fruitRepository.GetAllAsync();

            return Ok(_mapper.Map<List<FruitDto>>(fruitsDomain));
        }

        [HttpGet]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var fruitDomain = await _fruitRepository.GetByIdAsync(id);

            if (fruitDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FruitDto>(fruitDomain));
        }

        [HttpGet]
        [Route("{name}")]
        [ValidateModel]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var fruitDomain = await _fruitRepository.GetByNameAsync(name);

            if (fruitDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FruitDto>(fruitDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddFruitDto addFruitDto)
        {
            var fruitDomainModel = _mapper.Map<Fruit>(addFruitDto);

            fruitDomainModel = await _fruitRepository.CreateAsync(fruitDomainModel);

            var fruitDto = _mapper.Map<Fruit>(fruitDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = fruitDto.Id}, fruitDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var fruitDomainModel = await _fruitRepository.Deleteasync(id);

            if (fruitDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FruitDto>(fruitDomainModel));
        }
    }
}
