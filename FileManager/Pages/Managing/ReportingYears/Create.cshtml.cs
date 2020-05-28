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


        private async Task AddDocumentTitlesToNewReportingYearAsync(Guid yearId, List<Guid> titles, List<Guid> types, string addAll)
        {
            //  If user selected all, then get all documentTitles and make connection between each documentTitle and new reportingYear
            if (addAll != null && addAll == "on")
            {
                List<ReportingYearDocumentTitle> reportingYearDocumentTitles = new List<ReportingYearDocumentTitle>();
                (await db.DocumentTitle.ToListAsync())
                    .ForEach(ac => reportingYearDocumentTitles.Add(new ReportingYearDocumentTitle(yearId, ac.Id)));
                await db.ReportingYearDocumentTitle.AddRangeAsync(reportingYearDocumentTitles);
            }
            else
            {

                List<ReportingYearDocumentTitle> reportingYearDocumentTitles = new List<ReportingYearDocumentTitle>();
                (await db.DocumentTitle.ToListAsync())
                    .FindAll(dt =>
                    {
                        if (types.Contains(dt.DocumentTypeId))
                        {
                            return true;
                        }
                        if (titles.Contains(dt.Id))
                        {
                            return true;
                        }
                        return false;
                    })
                    .ForEach(ac => reportingYearDocumentTitles.Add(new ReportingYearDocumentTitle(yearId, ac.Id)));
                await db.ReportingYearDocumentTitle.AddRangeAsync(reportingYearDocumentTitles);
            }
        }
        public async Task<IActionResult> OnPostAsync(int number, List<Guid> titles, List<Guid> types, string addAll)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    var newReportingYear = new ReportingYear(number);
                    await db.ReportingYear.AddAsync(newReportingYear);
                    int result = await db.SaveChangesAsync();
                    if (result > 0)
                    {
                        await AddDocumentTitlesToNewReportingYearAsync(newReportingYear.Id, titles, types, addAll);

                        result = await db.SaveChangesAsync();
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
                _logger.LogError(e, "Error while adding new ReportingYear");
                return NotFound();
            }
        }
    }
}