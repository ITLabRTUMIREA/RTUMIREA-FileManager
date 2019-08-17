using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Pages.Account.Departments
{
    public class UserDepartmentListModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        public bool IsSystemAdmin = false;

        public UserDepartmentListModel (UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public List<User> Users;
        public async Task<IActionResult> OnGet()
        {
            Users = await _userManager.Users.ToListAsync();

            IsSystemAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(HttpContext.User), "SystemAdmin");

            return Page();
        }
    }
}