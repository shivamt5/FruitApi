using AutoFixture;
using AutoMapper;
using FruitApi.Commands;
using FruitApi.Handlers;
using FruitApi.Models.Domain;
using FruitApi.Models.DTOs;
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
    public class DeleteFruitHandlerTests
    {
        [Fact]
        public async Task Handle_DeleteFruit_ReturnsDeletedFruit()
        {
            // Arrange
            var fixture = new Fixture();
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var mapperMock = new Mock<IMapper>();

            var fruitDomain = fixture.Create<Fruit>();
            var fruitDto = fixture.Create<FruitDto>();
            Guid fruitId = Guid.NewGuid();

            fruitRepositoryMock.Setup(repo => repo.DeleteAsync(fruitId))
            .ReturnsAsync(fruitDomain);

            mapperMock.Setup(mapper => mapper.Map<FruitDto>(fruitDomain))
                .Returns(fruitDto);

            var handler = new DeleteFruitHandler(fruitRepositoryMock.Object, mapperMock.Object);
            var request = new DeleteFruitCommand(fruitId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FruitDto>(result);
        }
        


    }
}
