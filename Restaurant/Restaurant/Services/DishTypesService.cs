namespace Restaurant.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Restaurant.Data;
    using Restaurant.DataModels;
    using Restaurant.ViewModels.Dishes;

    public class DishTypesService : IDishTypesService
    {
        private readonly ApplicationDbContext dbContext;

        public DishTypesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(string name, string userId)
        {
            var restaurantId = this.dbContext.Restaurants
                .FirstOrDefault(x => x.Users.Any(x => x.Id == userId)).Id;

            var dishType = new DishesType
            {
                Name = name,
                RestaurantId = restaurantId,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };

            await this.dbContext.DishesTypes.AddAsync(dishType);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<DishTypeViewModel> GetAllByRestaurantId(string userId)
        {
            var restaurantId = this.dbContext.Restaurants
                .FirstOrDefault(x => x.Users.Any(x => x.Id == userId)).Id;

            var dishTypes = this.dbContext.DishesTypes
                .Where(x => x.RestaurantId == restaurantId && x.IsDelete == false)
                .Select(x => new DishTypeViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Dishes = x.Dishes.Where(x=>x.IsDelete == false).ToList().Count,
                })
                .ToList();

            return dishTypes;
        }

        public async Task RemoveAsync(string dishTypeId)
        {
            var dishTypes = this.dbContext.DishesTypes.FirstOrDefault(x => x.Id == dishTypeId);

            dishTypes.IsDelete = true;
            dishTypes.ModifiedOn = DateTime.UtcNow;
            dishTypes.DeletedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }
    }
}
