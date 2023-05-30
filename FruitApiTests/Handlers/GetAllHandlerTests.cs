using AutoFixture;
using AutoMapper;
using FruitApi.Handlers;
using FruitApi.Models.Domain;
using FruitApi.Models.DTOs;
using FruitApi.Queries;
using FruitApi.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitApiTests.Handlers
{
    public class GetAllHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsListOfFruitDtos()
        {
            // Arrange
            var fixture = new Fixture();
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var mapperMock = new Mock<IMapper>();

            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();

            var fruitDomain = fixture.Build<Fruit>();
            var fruitDto = fixture.Build<FruitDto>();

            var fruitsDomain = fixture.Repeat(fruitDomain.Create).ToList();
            var fruitsDto = fixture.Repeat(fruitDto.Create).ToList();
                        
            fruitRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(fruitsDomain);

            mapperMock.Setup(mapper => mapper.Map<List<FruitDto>>(fruitsDomain))
                .Returns(fruitsDto);

            var handler = new GetAllHandler(fruitRepositoryMock.Object, mapperMock.Object);
            var request = new GetAllQuery();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<FruitDto>>(result);
            
        }
    }
}
