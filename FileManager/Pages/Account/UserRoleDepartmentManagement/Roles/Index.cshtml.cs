using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.Services.GetAccountDataService;
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
        private readonly IGetAccountDataService _getAccountDataService;

        public IndexModel(FileManagerContext context,
            ILogger<IndexModel> logger,
             IGetAccountDataService getAccountDataService)
        {
            db = context;
            _logger = logger;
            _getAccountDataService = getAccountDataService;
        }

        public List<Role> Roles = null;
        public async Task<IActionResult> OnGet()
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    Roles = await db.Role.ToListAsync();
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting page of showing list of department roles");
                return NotFound();
            }
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    Role role = await db.Role.FirstAsync(r => r.Id.ToString() == id);
                    if (role != null)
                    {
                        db.Role.Remove(role);
                        await db.SaveChangesAsync();
                    }
                    return RedirectToPage("/Account/UserRoleDepartmentManagement/Roles/Index");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting department Role");
                return NotFound();
            }
        }
    }
}