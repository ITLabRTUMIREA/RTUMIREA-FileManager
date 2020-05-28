using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Controllers.Account.EmailConfirmation
{
    [Route("Account/[controller]")]
    [ApiController]
    public class EmailConfirmationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public EmailConfirmationController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            User user = await _userManager.FindByIdAsync(userId);
            IdentityResult emailConfirmationResult = await _userManager.ConfirmEmailAsync(user, token);

            if (emailConfirmationResult.Succeeded)
            {
                return RedirectToPagePermanent("/Account/Info",
                    "GetInfoMessage",
                    new { message = "Почта успешно подтверждена" },
                    null);
            }
            else
            {
                return RedirectToPagePermanent("/Account/Info",
                    "GetInfoMessage",
                    new { message = "Неверная ссылка для подтверждения почты"},
                    null);
            }

        }
    }
}