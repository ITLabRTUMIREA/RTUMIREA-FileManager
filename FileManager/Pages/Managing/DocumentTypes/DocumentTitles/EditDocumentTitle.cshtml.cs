using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.ReportingYearDocumentTitles;
using FileManager.Services.GetAccountDataService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Managing.DocumentTypes.DocumentTitles
{
    public class EditDocumentTitleModel : PageModel
    {
        private readonly FileManagerContext db;
        private readonly ILogger<EditDocumentTitleModel> _logger;
        private readonly IGetAccountDataService _getAccountDataService;

        public DocumentTitle selectedDocumentTitle;

        public EditDocumentTitleModel(FileManagerContext context,
            ILogger<EditDocumentTitleModel> logger,
            IGetAccountDataService getAccountDataService)
        {
            db = context;
            _logger = logger;
            _getAccountDataService = getAccountDataService;
        }
        public async Task<IActionResult> OnGetAsync(Guid titleId)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {

                    selectedDocumentTitle = await db.DocumentTitle
                       .Include(dt => dt.DocumentType)
                       .FirstOrDefaultAsync(dt => dt.Id == titleId);
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting page of Editing document title");
                return NotFound();
            }
        }
        public async Task<IActionResult> OnPostAsync(Guid titleId)
        {
            // TODO Fix updating document title and his description
            // TODO Do nice view of document title and his description 
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    if (selectedDocumentTitle != null)
                    {
                        db.DocumentTitle.Update(selectedDocumentTitle);

                        if (await db.SaveChangesAsync() > 0)
                            return RedirectToPage("Index", new { selectedDocumentTitle.DocumentType.Id });

                        _logger.LogError("Error while saving changed on page of Editing document title.\nFailed to save changes");
                        return NotFound();

                    }
                    else
                    {
                        _logger.LogError("Error while saving changed on page of Editing document title.\nFailed to load document title");
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while saving changed on page of Editing document title");
                return NotFound();
            }
        }
    }
}