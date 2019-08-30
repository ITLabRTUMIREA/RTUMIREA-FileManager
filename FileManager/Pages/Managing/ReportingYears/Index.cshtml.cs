using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.Models.Database.ReportingYearDocumentTitles;
using FileManager.Services.GetAccountDataService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Managing.ReportingYears
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

        public List<ReportingYear> ReportingYears;
        public async Task<IActionResult> OnGet()
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    ReportingYears = await db.ReportingYear.ToListAsync();
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting page of showing list of reporing years");
                return NotFound();
            }
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    ReportingYear year = await db.ReportingYear.FirstAsync(r => r.Id.ToString() == id);
                    if (year != null)
                    {
                        db.ReportingYear.Remove(year);
                        await db.SaveChangesAsync();
                    }
                    return RedirectToPage("/Managing/ReportingYears/Index");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting year");
                return NotFound();
            }
        }
    }
}