using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Controllers.Account.LogOut
{
    [Route("Account/[controller]")]
    [ApiController]
    public class LogOutController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        public LogOutController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync();

            return RedirectToPagePermanent("/Account/Info",
                "GetInfoMessage",
                new { message = "Вы успешно вышли из аккаунта"},
                null);
        }

    }
}