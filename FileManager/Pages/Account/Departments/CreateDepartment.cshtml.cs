using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileManager.Pages.Account.Departments
{
    public class CreateDepartmentModel : PageModel
        // TODO Make departaments managing 
    {
        private readonly RoleManager<Role> _roleManager;
        public CreateDepartmentModel(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new Role(name));
                if (result.Succeeded)
                {
                    return RedirectToPage("DepartmentsList");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Page();
        }
    }
}