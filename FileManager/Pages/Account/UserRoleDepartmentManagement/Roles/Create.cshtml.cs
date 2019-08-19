using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.Models.Database.UserSystemRoles;
using FileManager.Services.GetAccountDataService;
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
        private readonly IGetAccountDataService _getAccountDataService;

        public CreateModel(FileManagerContext context,
            ILogger<CreateModel> logger,
            IGetAccountDataService getAccountDataService)
        {
            db = context;
            _logger = logger;
            _getAccountDataService = getAccountDataService;
        }
        public IActionResult OnGet()
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting page of Creating role");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(string name)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
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
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding new Department Role");
                return NotFound();
            }
        }
    }
}