using AutoMapper;
using FruitApi.Commands;
using FruitApi.CustomActionFilters;
using FruitApi.Data;
using FruitApi.Models.Domain;
using FruitApi.Models.DTOs;
using FruitApi.Queries;
using FruitApi.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FruitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FruitsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("{name}")]
        [ValidateModel]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var query = new GetByNameQuery(name);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateFruitCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var command = new DeleteFruitCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
