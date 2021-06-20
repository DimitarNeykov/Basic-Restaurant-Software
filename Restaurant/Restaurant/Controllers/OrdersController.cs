namespace Restaurant.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Restaurant.Services;
    using Restaurant.ViewModels.Orders;
    using Restaurant.Attributes;

    [AuthorizeRoles(new[] { "Manager", "Waiter" })]
    public class OrdersController : Controller
    {
        private readonly ITablesService tablesService;
        private readonly IOrdersService ordersService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IDishTypesService dishTypesService;
        private readonly IDishesService dishesService;

        public OrdersController(ITablesService tablesService,
            IOrdersService ordersService,
            UserManager<IdentityUser> userManager,
            IDishTypesService dishTypesService,
            IDishesService dishesService)
        {
            this.tablesService = tablesService;
            this.ordersService = ordersService;
            this.userManager = userManager;
            this.dishTypesService = dishTypesService;
            this.dishesService = dishesService;
        }
        
        public async Task<IActionResult> Index(string tableId)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (this.tablesService.CheckForOpenOrder(tableId) == false)
            {
                await this.ordersService.CreateAsync(userId, tableId);
            }

            var order = this.ordersService.GetByTableId(tableId);

            return this.View(order);
        }

        public async Task<IActionResult> Pay(string orderId)
        {
            await this.ordersService.PayAsync(orderId);

            return this.RedirectToAction("All", "Tables");
        }

        public IActionResult DishTypes(string orderId)
        {
            var userId = this.userManager.GetUserId(this.User);

            var dishTypes = this.dishTypesService.GetAllByRestaurantId(userId);

            var viewModel = dishTypes.Select(x => new DishTypesViewModel
            {
                Name = x.Name,
                Id = x.Id,
                OrderId = orderId,
            });

            return this.View(viewModel);
        }

        public IActionResult Dishes(string orderId, string dishTypeId)
        {
            var userId = this.userManager.GetUserId(this.User);

            var dishes = this.dishesService.GetAllByRestaurantIdAndType(userId, dishTypeId);

            var viewModel = dishes.Select(x => new DishViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                OrderId = orderId,
            }).ToList();

            return this.View(viewModel);
        }

        public async Task<IActionResult> AddDishToOrder(string orderId, string dishId)
        {
            var tableId = this.ordersService.GetById(orderId).TableId;

            await this.ordersService.AddDishToOrderAsync(dishId, orderId);

            return this.RedirectToAction("Index", "Orders", new { tableId });
        }

        public async Task<IActionResult> RemoveDishFromOrder(string orderId, string dishId)
        {
            var tableId = this.ordersService.GetById(orderId).TableId;

            await this.ordersService.RemoveDishFromOrder(dishId, orderId);

            return this.RedirectToAction("Index", "Orders", new { tableId });
        }

        public async Task<IActionResult> Cancel(string orderId)
        {
            var tableId = this.ordersService.GetById(orderId).TableId;

            await this.ordersService.Cancel(orderId);

            return this.RedirectToAction("All", "Tables", new { tableId });
        }
    }
}
