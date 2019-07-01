using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
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
                return Content("Email Verified");
                // TODO Make success Email verification Page info
            }

            // TODO Make invalid Email verification Page info
            return Content("Invalid Email Verification Token");

        }
    }
}