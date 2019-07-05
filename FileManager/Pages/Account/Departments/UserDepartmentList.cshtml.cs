using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserRole;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Pages.Account.Departments
{
    public class UserDepartmentListModel : PageModel
        // TODO Make departaments managing 
    {
        private readonly UserManager<User> _userManager;
        public UserDepartmentListModel (UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public List<User> Users;
        public async Task<IActionResult> OnGet()
        {
            Users = await _userManager.Users.ToListAsync();
            return Page();
        }
    }
}