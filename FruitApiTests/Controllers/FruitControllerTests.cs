using Autofac.Extras.Moq;
using AutoFixture;
using AutoFixture.AutoMoq;
using FruitApi.Commands;
using FruitApi.Controllers;
using FruitApi.Models.Domain;
using FruitApi.Models.DTOs;
using FruitApi.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FruitApiTests.Controllers
{
    public class FruitControllerTests
    {
        [Fact]
        public async Task FruitsController_GetAll_ReturnsOk()
        {
            //Arrange
            var _fixture = new Fixture();
            var conceptUnitBuilder = _fixture.Build<FruitDto>();
            var expectedItems = _fixture.Repeat(conceptUnitBuilder.Create).ToList();

            var _mediator = new Mock<IMediator>();
            _mediator.Setup(x => x.Send(It.IsAny<GetAllQuery>(), default))
             .ReturnsAsync(expectedItems);
            
            var fruitsController = new FruitsController(_mediator.Object);

            //Act
            var result = await fruitsController.GetAll();

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualItems = Assert.IsAssignableFrom<IEnumerable<FruitDto>>(okResult.Value);
            Assert.Equal(expectedItems, actualItems);
        }

        [Fact]
        public async Task FruitsController_GetById_ReturnsCorrectFruit()
        {
            // Arrange
            var fixture = new Fixture();
            Guid fruitId = new Guid("E13D4AB9-62DF-424B-8618-3821592E600A");
            var expectedFruit = fixture.Create<FruitDto>();

            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetByIdQuery>(), default))
                .ReturnsAsync(expectedFruit);

            var controller = new FruitsController(mediatorMock.Object);

            // Act
            var result = await controller.GetById(fruitId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualFruit = Assert.IsAssignableFrom<FruitDto>(okResult.Value);
            Assert.Equal(expectedFruit, actualFruit);
        }

        [Fact]
        public async Task FruitsController_GetByName_ReturnsCorrectFruit()
        {
            //Arrange
            var fixture = new Fixture();
            var fruitName = fixture.Create<string>();
            var expectedFruit= fixture.Create<FruitDto>();

            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetByNameQuery>(), default))
                .ReturnsAsync(expectedFruit);
            var controller = new FruitsController(mediatorMock.Object);

            // Act
            var result = await controller.GetByName(fruitName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualFruit = Assert.IsAssignableFrom<FruitDto>(okResult.Value);
            Assert.Equal(expectedFruit, actualFruit);
        }

        [Fact]
        public async Task FruitsController_Create_ReturnsCreatedFruit()
        {
            // Arrange
            var fixture = new Fixture();
            var validFruit = fixture.Create<CreateFruitCommand>();
            var createdFruit = fixture.Create<FruitDto>();

            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateFruitCommand>(), default))
                .ReturnsAsync(createdFruit);

            var controller = new FruitsController(mediatorMock.Object);

            // Act
            var result = await controller.Create(validFruit);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var fruit = Assert.IsType<FruitDto>(createdResult.Value);
            Assert.Equal(createdFruit, fruit);
        }

        [Fact]
        public async Task FruitsController_Delete_ReturnsDeletedFruitWithIsDeletedAsTrue()
        {
            // Arrange
            var fixture = new Fixture();
            Guid fruitId = Guid.NewGuid();
            var deletedFruit = fixture.Create<FruitDto>();
            
            var deleteFruit = new FruitDto
            {
                Name = "fruit",
                Family = "family",
                isDeleted = true
            };

            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteFruitCommand>(), default))
                .ReturnsAsync(deleteFruit);

            var controller = new FruitsController(mediatorMock.Object);

            // Act
            var result = await controller.Delete(fruitId);

            // Assert
            Assert.True(deleteFruit.isDeleted);
        }

    }
}
