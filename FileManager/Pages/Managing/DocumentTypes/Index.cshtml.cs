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

namespace FileManager.Pages.Managing.DocumentTypes
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

        public List<DocumentType> DocumentTypes;
        public async Task<IActionResult> OnGet()
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    DocumentTypes = await db.DocumentType.ToListAsync();
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting page of showing list of document types");
                return NotFound();
            }
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            try
            {
                if (_getAccountDataService.IsSystemAdmin())
                {
                    DocumentType type = await db.DocumentType.FirstAsync(r => r.Id.ToString() == id);
                    if (type != null)
                    {
                        db.DocumentType.Remove(type);
                        await db.SaveChangesAsync();
                    }
                    return RedirectToPage("/Managing/DocumentTypes/Index");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting document type");
                return NotFound();
            }
        }
    }
}