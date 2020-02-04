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

namespace FileManager.Pages.Managing.DocumentTypes.DocumentTitles
{
    public class CreateModel : PageModel
    {
        private readonly FileManagerContext db;
        private readonly ILogger<CreateModel> _logger;
        private readonly IGetAccountDataService _getAccountDataService;

        public DocumentType selectedDocumentType;

        public CreateModel(FileManagerContext context,
            ILogger<CreateModel> logger,
            IGetAccountDataService getAccountDataService)
        {
            db = context;
            _logger = logger;
            _getAccountDataService = getAccountDataService;
        }
        public async Task<IActionResult> OnGetAsync(Guid documentTypeId)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    selectedDocumentType = await db.DocumentType.FirstOrDefaultAsync(dt => dt.Id.Equals(documentTypeId));
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting page of Creating document title");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(string name, Guid documentTypeId, string description)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    if (await CreateNewDocumentTitle(name, documentTypeId, description) > 0)
                    {
                        //if (await CreateNewReportingYearDocumentTitleRecord(name, documentTypeId) > 0)
                        //{
                        return RedirectToPage("Index", routeValues: new { documentTypeId });
                        //}
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
                _logger.LogError(e, "Error while adding new Document title");
                return NotFound();
            }
        }

        private async Task<int> CreateNewDocumentTitle(string name, Guid documentTypeId, string description)
        {
            await db.DocumentTitle.AddAsync(new DocumentTitle(name, documentTypeId, description));
            return await db.SaveChangesAsync();
        }


        //private async Task<int> CreateNewReportingYearDocumentTitleRecord(string name, Guid documentTypeId)
        //{
        //    Guid documentTitleId = (await db.DocumentTitle.FirstOrDefaultAsync(dt => dt.Name == name && dt.DocumentTypeId == documentTypeId)).Id;
        //    Guid reportingYearId = (await db.ReportingYear.FirstOrDefaultAsync(ry => ry.Number == DateTime.Now.Year)).Id;

        //    await db.ReportingYearDocumentTitle.AddAsync(new ReportingYearDocumentTitle(reportingYearId, documentTitleId));

        //    return await db.SaveChangesAsync();
        //}
    }
}