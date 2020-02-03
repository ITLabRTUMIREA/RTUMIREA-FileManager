using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.Models.Database.UserSystemRoles;
using FileManager.Models.Database.ReportingYearDocumentTitles;
using FileManager.Services.GetAccountDataService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Pages.Managing.ReportingYears
{
    public class CreateModel : PageModel
    {
        private readonly FileManagerContext db;
        private readonly ILogger<CreateModel> _logger;
        private readonly IGetAccountDataService _getAccountDataService;
        public List<DocumentType> documentTypes;

        // Document titles, which will be used in new reportingYear
        public List<string> addingDocumentTitles = new List<string>();

        public CreateModel(FileManagerContext context,
            ILogger<CreateModel> logger,
            IGetAccountDataService getAccountDataService)
        {
            db = context;
            _logger = logger;
            _getAccountDataService = getAccountDataService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    documentTypes = await db.DocumentType
                        .Include(dt => dt.DocumentTitles)
                        .ToListAsync();
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting page of Creating year");
                return NotFound();
            }
        }



        public async Task<IActionResult> OnPostAsync(int number, List<string> title, List<string> type, string addAll)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    await db.ReportingYear.AddAsync(new ReportingYear(number));
                    int result = await db.SaveChangesAsync();
                    if (result > 0)
                    {
                        return RedirectToPage("Index");
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
                _logger.LogError(e, "Error while adding new ReportingYear");
                return NotFound();
            }
        }
    }
}