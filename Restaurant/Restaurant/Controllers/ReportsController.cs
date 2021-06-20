namespace Restaurant.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Restaurant.Services;
    using Restaurant.Attributes;

    [AuthorizeRoles(new[] { "Manager" })]
    public class ReportsController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly UserManager<IdentityUser> userManager;

        public ReportsController(IOrdersService ordersService, UserManager<IdentityUser> userManager)
        {
            this.ordersService = ordersService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Cancel()
        {
            var userId = this.userManager.GetUserId(this.User);

            var viewModel = this.ordersService.GetAllCanceledByRestaurant(userId);

            return this.View(viewModel);
        }

        public IActionResult Paid()
        {
            var userId = this.userManager.GetUserId(this.User);

            var viewModel = this.ordersService.GetAllPaidByRestaurant(userId);

            return this.View(viewModel);
        }
    }
}
