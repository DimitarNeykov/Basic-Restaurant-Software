namespace Restaurant.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Restaurant.Data;
    using Restaurant.DataModels;
    using Restaurant.ViewModels.Dishes;

    public class DishesService : IDishesService
    {
        private readonly ApplicationDbContext dbContext;

        public DishesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(DishCreateInputModel input, string userId)
        {
            var restaurantId = this.dbContext.Restaurants
                .FirstOrDefault(x => x.Users.Any(x => x.Id == userId)).Id;

            var dish = new Dish
            {
                Name = input.Name,
                Price = input.Price,
                DishesTypeId = input.DishesTypeId,
                RestaurantId = restaurantId,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };

            await this.dbContext.Dishes.AddAsync(dish);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<DishViewModel> GetAllByRestaurantId(string userId)
        {
            var restaurantId = this.dbContext.Restaurants
                .FirstOrDefault(x => x.Users.Any(x => x.Id == userId)).Id;

            var dishes = this.dbContext.Dishes
                .Where(x => x.RestaurantId == restaurantId && x.IsDelete == false && x.DishesType.IsDelete == false)
                .Select(x => new DishViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    TypeName = x.DishesType.Name,
                    Price = x.Price
                })
                .ToList();

            return dishes;
        }

        public IEnumerable<DishViewModel> GetAllByRestaurantIdAndType(string userId, string dishTypeId)
        {
            var restaurantId = this.dbContext.Restaurants
                .FirstOrDefault(x => x.Users.Any(x => x.Id == userId)).Id;

            var dishes = this.dbContext.Dishes
                .Where(x => x.RestaurantId == restaurantId && x.IsDelete == false && x.DishesTypeId == dishTypeId)
                .Select(x => new DishViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    TypeName = x.DishesType.Name,
                    Price = x.Price
                })
                .ToList();

            return dishes;
        }

        public async Task RemoveAsync(string dishId)
        {
            var dish = this.dbContext.Dishes.FirstOrDefault(x => x.Id == dishId);

            dish.IsDelete = true;
            dish.ModifiedOn = DateTime.UtcNow;
            dish.DeletedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }
    }
}
