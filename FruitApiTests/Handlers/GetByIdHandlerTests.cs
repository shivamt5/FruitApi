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
    public class GetByIdHandlerTests
    {
        [Fact]
        public async Task Handle_ExistingFruitId_ReturnsFruitDto()
        {
            // Arrange
            var fixture = new Fixture();
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var mapperMock = new Mock<IMapper>();

            Guid fruitId = Guid.NewGuid();
            var fruitDomain = fixture.Create<Fruit>();
            var fruitDto = fixture.Create<FruitDto>();
                        
            fruitRepositoryMock.Setup(repo => repo.GetByIdAsync(fruitId))
                .ReturnsAsync(fruitDomain);

            mapperMock.Setup(mapper => mapper.Map<FruitDto>(fruitDomain))
                .Returns(fruitDto);

            var handler = new GetByIdHandler(fruitRepositoryMock.Object, mapperMock.Object);
            var request = new GetByIdQuery(fruitId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FruitDto>(result);
            
        }

        [Fact]
        public async Task Handle_NonexistentFruitId_ReturnsNull()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var mapperMock = new Mock<IMapper>();

            Guid fruitId = Guid.NewGuid();
            Fruit fruitDomain = null;

            fruitRepositoryMock.Setup(repo => repo.GetByIdAsync(fruitId))
                .ReturnsAsync(fruitDomain);

            var handler = new GetByIdHandler(fruitRepositoryMock.Object, mapperMock.Object);
            var request = new GetByIdQuery(fruitId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Null(result);
            
        }
    }
}
