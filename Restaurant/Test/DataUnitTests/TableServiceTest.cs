using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Services;
using Restaurant.ViewModels.Restaurants;
using Xunit;

namespace Test.DataUnitTests
{
    public class TableServiceTest
    {
        [Fact]
        public async Task TestCreateTable()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var user = new IdentityUser("Test");
            await dbContext.Users.AddAsync(user);

            var restaurantService = new RestaurantService(dbContext);
            var restaurant = new RestaurantCreateInputModel
            {
                Address = "Test",
                Name = "Test",
                PhoneNumber = "0888888888"
            };
            await restaurantService.CreateAsync(restaurant);
            dbContext.Restaurants.First().Users.Add(user);
            await dbContext.SaveChangesAsync();

            var service = new TablesService(dbContext);
            

            await service.CreateAsync(user.Id, 1);

            Assert.Single(dbContext.Tables);
        }

        [Fact]
        public async Task TestDeleteTable()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var user = new IdentityUser("Test");
            await dbContext.Users.AddAsync(user);

            var restaurantService = new RestaurantService(dbContext);
            var restaurant = new RestaurantCreateInputModel
            {
                Address = "Test",
                Name = "Test",
                PhoneNumber = "0888888888"
            };
            await restaurantService.CreateAsync(restaurant);
            dbContext.Restaurants.First().Users.Add(user);
            await dbContext.SaveChangesAsync();

            var service = new TablesService(dbContext);


            await service.CreateAsync(user.Id, 1);
            await service.RemoveAsync(dbContext.Tables.First().Id);

            Assert.True(dbContext.Tables.First().IsDelete);
        }

        [Fact]
        public async Task TestCheckForOpenOrder()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var user = new IdentityUser("Test");
            await dbContext.Users.AddAsync(user);

            var restaurantService = new RestaurantService(dbContext);
            var restaurant = new RestaurantCreateInputModel
            {
                Address = "Test",
                Name = "Test",
                PhoneNumber = "0888888888"
            };
            await restaurantService.CreateAsync(restaurant);
            dbContext.Restaurants.First().Users.Add(user);
            await dbContext.SaveChangesAsync();

            var service = new TablesService(dbContext);


            await service.CreateAsync(user.Id, 1);
            var isFalse = service.CheckForOpenOrder(dbContext.Tables.First().Id);

            Assert.False(isFalse);
        }

        [Fact]
        public async Task TestGetAllTablesByRestaurant()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var user = new IdentityUser("Test");
            await dbContext.Users.AddAsync(user);

            var restaurantService = new RestaurantService(dbContext);
            var restaurant = new RestaurantCreateInputModel
            {
                Address = "Test",
                Name = "Test",
                PhoneNumber = "0888888888"
            };
            await restaurantService.CreateAsync(restaurant);
            dbContext.Restaurants.First().Users.Add(user);
            await dbContext.SaveChangesAsync();

            var service = new TablesService(dbContext);


            await service.CreateAsync(user.Id, 1);
            await service.CreateAsync(user.Id, 3);
            await service.CreateAsync(user.Id, 8);

            var tables = service.GetAllByRestaurantId(user.Id);

            Assert.Equal(3, tables.Count());
        }
    }
}
