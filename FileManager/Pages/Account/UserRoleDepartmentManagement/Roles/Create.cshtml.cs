using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.Models.Database.UserSystemRoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Account.Roles
{
    public class CreateModel : PageModel
    {
        private readonly FileManagerContext db;
        private readonly ILogger<CreateModel> _logger;
        public CreateModel(FileManagerContext context,
            ILogger<CreateModel> logger)
        {
            db = context;
            _logger = logger;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string name)
        {
            try
            {

                if (!string.IsNullOrEmpty(name))
                {
                    await db.AddAsync(new Role(name));
                    int result = await db.SaveChangesAsync();
                    if (result > 0)
                    {
                        return RedirectToPage("Index");
                    }
                }
                return Page();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding new System Role");
                return NotFound();
            }
        }
    }
}