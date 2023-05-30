using AutoFixture;
using AutoMapper;
using FruitApi.Commands;
using FruitApi.Handlers;
using FruitApi.Models.Domain;
using FruitApi.Models.DTOs;
using FruitApi.Queries;
using FruitApi.Repositories;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitApiTests.Handlers
{
    public class CreateFruitHandlerTests
    {
        [Fact]
        public async Task Handle_CreatedFruit_ReturnsNewFruit()
        {
            // Arrange
            var fixture = new Fixture();
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var mapperMock = new Mock<IMapper>();

            var fruitDomain = fixture.Create<Fruit>();
            var fruitDto = fixture.Create<FruitDto>();

            fruitRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Fruit>()))
                .ReturnsAsync(fruitDomain);

            mapperMock.Setup(mapper => mapper.Map<FruitDto>(It.IsAny<Fruit>()))
                .Returns(fruitDto);

            var handler = new CreateFruitHandler(fruitRepositoryMock.Object, mapperMock.Object);
            var request = new CreateFruitCommand();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FruitDto>(result);
        }
    }
}
