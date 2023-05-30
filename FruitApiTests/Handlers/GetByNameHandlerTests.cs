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
    public class GetByNameHandlerTests
    {
        public async Task Handle_ExistingFruitName_ReturnsFruitDto()
        {
            // Arrange
            var fixture = new Fixture();
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var mapperMock = new Mock<IMapper>();
            string fruit = "Some Fruit";

            var fruitDomain = fixture.Create<Fruit>();
            var fruitDto = fixture.Create<FruitDto>();

            fruitRepositoryMock.Setup(repo => repo.GetByNameAsync(fruit))
                .ReturnsAsync(fruitDomain);

            mapperMock.Setup(mapper => mapper.Map<FruitDto>(fruitDomain))
                .Returns(fruitDto);

            var handler = new GetByNameHandler(fruitRepositoryMock.Object, mapperMock.Object);
            var request = new GetByNameQuery(fruit);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FruitDto>(result);
        }

        [Fact]
        public async Task Handle_NonExistentFruit_ReturnsNull()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var mapperMock = new Mock<IMapper>();

            string fruit = "Some Fruit";
            Fruit fruitDomain = null;

            fruitRepositoryMock.Setup(repo => repo.GetByNameAsync(fruit))
                .ReturnsAsync(fruitDomain);

            var handler = new GetByNameHandler(fruitRepositoryMock.Object, mapperMock.Object);
            var request = new GetByNameQuery(fruit);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
