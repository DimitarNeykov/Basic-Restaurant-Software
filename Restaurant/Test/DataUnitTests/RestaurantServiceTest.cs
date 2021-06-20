using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Services;
using Restaurant.ViewModels.Restaurants;
using Xunit;

namespace Test.DataUnitTests
{
    public class RestaurantServiceTest
    {
        [Fact]
        public async Task TestCreateRestaurant()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var service = new RestaurantService(dbContext);

            var restaurant = new RestaurantCreateInputModel()
            {
                Name = "Test",
                Address = "Test",
                PhoneNumber = "0888888888"
            };

            await service.CreateAsync(restaurant);

            Assert.Single(dbContext.Restaurants);
        }

        [Fact]
        public async Task TestDeleteAsyncRestaurant()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var service = new RestaurantService(dbContext);

            var restaurant = new RestaurantCreateInputModel()
            {
                Name = "Test",
                Address = "Test",
                PhoneNumber = "0888888888"
            };

            await service.CreateAsync(restaurant);
            await service.DeleteAsync(dbContext.Restaurants.First().Id);

            Assert.True(dbContext.Restaurants.First().IsDelete);
        }

        [Fact]
        public async Task TestGetAllRestaurant()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var service = new RestaurantService(dbContext);

            var restaurant = new RestaurantCreateInputModel()
            {
                Name = "Test",
                Address = "Test",
                PhoneNumber = "0888888888"
            };

            await service.CreateAsync(restaurant);
            await service.CreateAsync(restaurant);

            var restaurants = service.GetAllRestaurants();

            Assert.Equal(2, restaurants.Count());
        }
    }
}
