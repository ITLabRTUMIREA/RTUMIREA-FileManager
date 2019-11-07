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
        public async Task<IActionResult> OnPostAsync(Guid titleId, string titleName, string titleDescription)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    selectedDocumentTitle = await db.DocumentTitle
                       .Include(dt => dt.DocumentType)
                       .FirstOrDefaultAsync(dt => dt.Id == titleId);

                    if (selectedDocumentTitle != null)
                    {

                        if (selectedDocumentTitle.Description != titleDescription || selectedDocumentTitle.Name != titleName)
                        {
                            if (selectedDocumentTitle.Name != titleName)
                                selectedDocumentTitle.Name = titleName;

                            if (selectedDocumentTitle.Description != titleDescription)
                                selectedDocumentTitle.Description = titleDescription;

                            if (await TryUpdateModelAsync<DocumentTitle>(selectedDocumentTitle))
                            {
                                if (await db.SaveChangesAsync() > 0)
                                    return RedirectToPage("Index", new { documentTypeId = selectedDocumentTitle.DocumentType.Id });
                            }
                        }
                        else
                        {
                            return RedirectToPage("Index", new { documentTypeId = selectedDocumentTitle.DocumentType.Id });
                        }

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