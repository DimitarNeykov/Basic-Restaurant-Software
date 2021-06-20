namespace Restaurant.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Restaurant.Services;
    using Restaurant.ViewModels.Restaurants;
    using Restaurant.Attributes;

    [AuthorizeRoles(new[] { "Administrator" })]
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantService restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RestaurantCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.restaurantService.CreateAsync(input);

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(string restaurantId)
        {
            var restaurant = this.restaurantService.GetById(restaurantId);

            var model = new RestaurantEditInputModel
            {
                Id = restaurant.Id,
                Address = restaurant.Address,
                Name = restaurant.Name,
                PhoneNumber = restaurant.PhoneNumber,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RestaurantEditInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.restaurantService.EditAsync(input);

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            var restaurants = this.restaurantService.GetAllRestaurants();

            return this.View(restaurants);
        }

        public IActionResult Details(string restaurantId)
        {
            var restaurant = this.restaurantService.GetById(restaurantId);

            return this.View(restaurant);
        }

        public async Task<IActionResult> Delete(string restaurantId)
        {
            await this.restaurantService.DeleteAsync(restaurantId);

            return RedirectToAction("All");
        }
    }
}
