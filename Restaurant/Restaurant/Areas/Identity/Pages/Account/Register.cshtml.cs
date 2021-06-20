using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Restaurant.Areas.Identity.Pages.Account
{
    [Authorize("Administrator")]
    public class RegisterModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("Login");
        }
    }
}
