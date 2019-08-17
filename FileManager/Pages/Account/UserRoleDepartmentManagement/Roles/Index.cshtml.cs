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
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Account.Roles
{
    public class IndexModel : PageModel
    {
        private readonly FileManagerContext db;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(FileManagerContext context,
            ILogger<IndexModel> logger)
        {
            db = context;
            _logger = logger;
        }

        public List<Role> Roles = null;
        public async Task<IActionResult> OnGet()
        {
            Roles = await db.Role.ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            try
            {

                Role role = await db.Role.FirstAsync(r => r.Id.ToString() == id);
                if (role != null)
                {
                    db.Role.Remove(role);
                    await db.SaveChangesAsync();
                }
                return RedirectToPage("/Account/UserRoleDepartmentManagement/Roles/Index");
            }catch(Exception e)
            {
                _logger.LogError(e, "Error while deleting System Role");
                return NotFound();
            }
        }
    }
}