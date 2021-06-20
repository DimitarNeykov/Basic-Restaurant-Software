namespace Restaurant.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using Restaurant.Data;
    using Restaurant.DataModels;
    using Restaurant.ViewModels.Restaurants;

    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext dbContext;

        public RestaurantService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddUserAsync(IdentityUser user, string restaurantId)
        {
            var restaurant = this.dbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == restaurantId && x.IsDelete == false);

            restaurant.Users.Add(user);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(RestaurantCreateInputModel input)
        {
            var restaurant = new RestaurantInfo
            {
                Name = input.Name,
                Address = input.Address,
                PhoneNumber = input.PhoneNumber,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };

            await this.dbContext.Restaurants.AddAsync(restaurant);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string restaurantId)
        {
            var restaurant = this.dbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == restaurantId && x.IsDelete == false);

            restaurant.IsDelete = true;
            restaurant.DeletedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(RestaurantEditInputModel input)
        {
            var restaurant = this.dbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == input.Id && x.IsDelete == false);
            
            if (restaurant == null)
            {
                return;
            }

            restaurant.Name = input.Name;
            restaurant.Address = input.Address;
            restaurant.PhoneNumber = input.PhoneNumber;
            restaurant.ModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<RestaurantViewModel> GetAllRestaurants()
        {
            var restaurants = this.dbContext
                .Restaurants
                .Where(x => x.IsDelete == false)
                .Select(x => new RestaurantViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn,
                    Users = x.Users.Select(u => new UserViewModel
                    {
                        Email = u.Email,
                        Username = u.UserName,
                    }).ToList()
                })
                .ToList();

            return restaurants;
        }

        public RestaurantViewModel GetById(string restaurantId)
        {
            var restaurant = this.dbContext
                .Restaurants
                .Where(x => x.Id == restaurantId && x.IsDelete == false)
                .Select(x => new RestaurantViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn,
                    Users = x.Users.Select(u => new UserViewModel
                    {
                        Id = u.Id,
                        PhoneNumber = u.PhoneNumber,
                        Email = u.Email,
                        Username = u.UserName,
                    }).ToList()
                }).FirstOrDefault();

            return restaurant;
        }
    }
}
