namespace Restaurant.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using Restaurant.ViewModels.Restaurants;

    public interface IRestaurantService
    {
        Task CreateAsync(RestaurantCreateInputModel input);

        RestaurantViewModel GetById(string restaurantId);

        IEnumerable<RestaurantViewModel> GetAllRestaurants();

        Task EditAsync(RestaurantEditInputModel input);

        Task AddUserAsync(IdentityUser user, string restaurantId);

        Task DeleteAsync(string restaurantId);
    }
}
