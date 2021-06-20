namespace Restaurant.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Restaurant.ViewModels.Dishes;

    public interface IDishesService
    {
        Task CreateAsync(DishCreateInputModel input, string userId);

        IEnumerable<DishViewModel> GetAllByRestaurantId(string userId);

        IEnumerable<DishViewModel> GetAllByRestaurantIdAndType(string userId, string dishTypeId);

        Task RemoveAsync(string dishId);
    }
}
