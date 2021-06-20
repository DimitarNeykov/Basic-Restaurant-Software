namespace Restaurant.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Restaurant.ViewModels.Dishes;

    public interface IDishTypesService
    {
        Task CreateAsync(string name, string userId);

        IEnumerable<DishTypeViewModel> GetAllByRestaurantId(string userId);

        Task RemoveAsync(string dishTypeId);
    }
}
