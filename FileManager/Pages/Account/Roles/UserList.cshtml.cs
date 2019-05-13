using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Pages.Account.Roles
{
    public class UserListModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        public UserListModel (UserManager<User> userManager)
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