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

namespace FileManager.Pages.Account.Roles
{
    public class IndexModel : PageModel
    {
            private readonly RoleManager<Role> _roleManager;
        public IndexModel(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public List<Role> Roles = null;
        public async Task<IActionResult> OnGet()
        {
            Roles = await _roleManager.Roles.ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToPage("/Account/UserRoleDepartmentManagement/Roles/Index");
        }
    }
}