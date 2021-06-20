namespace Restaurant.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Restaurant.Services;
    using Restaurant.ViewModels.Dishes;
    using Restaurant.Attributes;

    [AuthorizeRoles(new[] { "Manager"})]
    public class DishesController : Controller
    {
        private readonly IDishesService dishesService;
        private readonly IDishTypesService dishTypesService;
        private readonly UserManager<IdentityUser> userManager;

        public DishesController(IDishesService dishesService,
            IDishTypesService dishTypesService,
            UserManager<IdentityUser> userManager)
        {
            this.dishesService = dishesService;
            this.dishTypesService = dishTypesService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            var userId = this.userManager.GetUserId(this.User);
            var dishTypes = this.dishTypesService.GetAllByRestaurantId(userId);

            var dishTypesDropDown = dishTypes.Select(x => new DishTypeDropDownViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            var viewModel = new DishCreateInputModel
            {
                DishTypes = dishTypesDropDown,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DishCreateInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.ModelState.IsValid)
            {
                var dishTypes = this.dishTypesService.GetAllByRestaurantId(userId);

                var dishTypesDropDown = dishTypes.Select(x => new DishTypeDropDownViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

                input.DishTypes = dishTypesDropDown;

                return this.View(input);
            }

            await this.dishesService.CreateAsync(input, userId);

            return this.RedirectToAction("MyDishes");
        }

        [HttpPost]
        public async Task<IActionResult> CreateType(string name)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.dishTypesService.CreateAsync(name, userId);
            return this.RedirectToAction("DishTypes");
        }

        public IActionResult DishTypes()
        {
            var userId = this.userManager.GetUserId(this.User);
            var dishTypes = this.dishTypesService.GetAllByRestaurantId(userId);

            return this.View(dishTypes);
        }

        public IActionResult MyDishes()
        {
            var userId = this.userManager.GetUserId(this.User);

            var dishes = this.dishesService.GetAllByRestaurantId(userId);

            return this.View(dishes);
        }

        public IActionResult Manage()
        {
            return this.View();
        }

        public async Task<IActionResult> RemoveDishType(string dishTypeId)
        {
            await this.dishTypesService.RemoveAsync(dishTypeId);

            return this.RedirectToAction("DishTypes");
        }

        public async Task<IActionResult> RemoveDish(string dishId)
        {
            await this.dishesService.RemoveAsync(dishId);

            return this.RedirectToAction("MyDishes");
        }
    }
}
