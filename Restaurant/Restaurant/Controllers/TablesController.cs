namespace Restaurant.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Restaurant.Services;
    using Restaurant.Attributes;

    [Authorize]
    public class TablesController : Controller
    {
        private readonly ITablesService tablesService;
        private readonly UserManager<IdentityUser> userManager;

        public TablesController(ITablesService tablesService, UserManager<IdentityUser> userManager)
        {
            this.tablesService = tablesService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            var userId = this.userManager.GetUserId(this.User);

            var tables = this.tablesService.GetAllByRestaurantId(userId);

            return this.View(tables);
        }

        [HttpPost]
        [AuthorizeRoles(new[] { "Manager" })]
        public async Task<IActionResult> Create(int number)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.tablesService.CreateAsync(userId, number);

            return this.RedirectToAction("All");
        }

        [AuthorizeRoles(new[] { "Manager" })]
        public async Task<IActionResult> Remove(string tableId)
        {
            await this.tablesService.RemoveAsync(tableId);

            return this.RedirectToAction("All");
        }
    }
}
