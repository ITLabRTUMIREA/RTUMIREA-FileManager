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

namespace FileManager.Pages.Managing.DocumentTypes.DocumentTitles
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

        public List<DocumentTitle> DocumentTitles;
        public DocumentType selectedDocumentType;

        public async Task<IActionResult> OnGet(Guid documentTypeId)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    selectedDocumentType = await db.DocumentType.FirstOrDefaultAsync(dt => dt.Id.Equals(documentTypeId));

                    DocumentTitles = await db.DocumentTitle
                        .Where(dt => dt.DocumentTypeId.Equals(documentTypeId))
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
                _logger.LogError(e, "Error while getting page of showing list of document titles");
                return NotFound();
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(Guid titleId, Guid documentTypeId)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    DocumentTitle deletingTitle = await db.DocumentTitle
                        .Where(dt => dt.DocumentTypeId.Equals(documentTypeId))
                        .FirstOrDefaultAsync(r => r.Id.Equals(titleId));
                    if (deletingTitle != null)
                    {
                        db.DocumentTitle.Remove(deletingTitle);
                        await db.SaveChangesAsync();
                    }
                    return RedirectToPage("Index", new { documentTypeId });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting document title");
                return NotFound();
            }
        }
    }
}