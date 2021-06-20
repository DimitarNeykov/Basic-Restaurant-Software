namespace Restaurant.Controllers
{
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.WebUtilities;

    using Restaurant.Services;
    using Restaurant.ViewModels.Users;
    using Restaurant.Attributes;

    [AuthorizeRoles(new[] { "Administrator" })]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IRestaurantService restaurantService;

        public UsersController(UserManager<IdentityUser> userManager, IRestaurantService restaurantService)
        {
            this.userManager = userManager;
            this.restaurantService = restaurantService;
        }

        public IActionResult Create(string restaurantId)
        {
            var model = new UserCreateInputModel
            {
                RestaurantId = restaurantId,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var newUser = new IdentityUser
            {
                PhoneNumber = input.PhoneNumber,
                UserName = input.Username,
                Email = input.Email,
            };

            var result = await this.userManager.CreateAsync(newUser, input.Password);
            if (result.Succeeded)
            {
                var code = await this.userManager.GenerateEmailConfirmationTokenAsync(newUser);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = this.Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = newUser.Id, code = code },
                    protocol: this.Request.Scheme);
            }

            await this.restaurantService.AddUserAsync(newUser, input.RestaurantId);

            switch (input.Role)
            {
                case "Manager":
                    await this.userManager.AddToRoleAsync(newUser, "Manager");
                    break;
                case "Waiter":
                    await this.userManager.AddToRoleAsync(newUser, "Waiter");
                    break;
            }

            return this.RedirectToAction("Details", "Restaurants", new { restaurantId = input.RestaurantId });
        }
    }
}