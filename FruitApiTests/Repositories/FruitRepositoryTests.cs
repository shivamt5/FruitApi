using AutoFixture;
using AutoFixture.AutoMoq;
using FruitApi.Data;
using FruitApi.Models.Domain;
using FruitApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitApiTests.Repositories
{
    public class FruitRepositoryTests
    {
        
        private async Task<ApiDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApiDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Fruits.CountAsync() <= 0)
            {
                databaseContext.Fruits.AddRange(
                    new Fruit()
                    {
                        Id= new Guid("96369C41-C037-4F00-A5A8-1E1CE177C6B6"),
                        Name = "fruit1",
                        Family = "family1",
                        DateCreated = DateTime.Now,
                        DateDeleted = null,
                        isDeleted = false,
                    },
                    new Fruit()
                    {
                        Id = new Guid("96369C41-C037-4F00-A5A9-1E1CE177C6B6"),
                        Name = "fruit1",
                        Family = "family1",
                        DateCreated = DateTime.Now,
                        DateDeleted = null,
                        isDeleted = false,
                    },
                    new Fruit()
                    {
                        Id = new Guid("96369C41-C037-4F00-A5A7-1E1CE177C6B6"),
                        Name = "fruit1",
                        Family = "family1",
                        DateCreated = DateTime.Now,
                        DateDeleted = null,
                        isDeleted = false,
                    },
                    new Fruit()
                    {
                        Id = new Guid("96369C41-C037-4F00-A5A6-1E1CE177C6B6"),
                        Name = "fruit1",
                        Family = "family1",
                        DateCreated = DateTime.Now,
                        DateDeleted = null,
                        isDeleted = false,
                    },
                    new Fruit()
                    {
                        Id = new Guid("96369C41-C037-4F00-A5A4-1E1CE177C6B6"),
                        Name = "fruit1",
                        Family = "family1",
                        DateCreated = DateTime.Now,
                        DateDeleted = null,
                        isDeleted = false,
                    }
                    );
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllFruits()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var fruitRepository = new FruitRepository( dbContext );

            // Act
            var result = await fruitRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dbContext.Fruits.Count(), result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnFruitWithMatchingId()
        {
            // Arrange
            Guid id = new Guid("96369C41-C037-4F00-A5A8-1E1CE177C6B6");
            var dbContext = await GetDatabaseContext();
            var fruitRepository = new FruitRepository(dbContext);
            
            // Act
            var result = await fruitRepository.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnFruitWithMatchingName()
        {
            //Arrange
            var name = "fruit1";
            var dbContext = await GetDatabaseContext();
            var fruiRepository = new FruitRepository(dbContext);

            //Act
            var result = fruiRepository.GetByNameAsync(name);
            
            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedFruit()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var initialCount = dbContext.Fruits.Count();
            var fruitRepository = new FruitRepository(dbContext);
            var fixture = new Fixture();
            var fruitDomain = fixture.Create<Fruit>();

            //Act
            var result = fruitRepository.CreateAsync(fruitDomain);

            //Assert
            Assert.NotNull(result);
            int expectedCount = initialCount + 1;
            Assert.Equal(expectedCount, dbContext.Fruits.Count());
        }

        [Fact]
        public async Task DeleteAsync_ShouldSetIsDeletedToTrue()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var fruitRepository = new FruitRepository(dbContext);
            Guid id = new Guid("96369C41-C037-4F00-A5A8-1E1CE177C6B6");

            //Act
            var result = fruitRepository.DeleteAsync(id);

            //Assert
            var deletedFruit = await dbContext.Fruits.FirstOrDefaultAsync(f => f.Id == id);

            Assert.NotNull(deletedFruit);
            Assert.True(deletedFruit.isDeleted);
        }
    }
}
