using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.ReportingYearDocumentTitles;
using FileManager.Services.SmartBreadcrumbService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Directory
{
    public class DocumentModel : PageModel
    {
        private readonly FileManagerContext db;
        private readonly ILogger<DocumentModel> _logger;
        private readonly ISmartBreadcrumbService _breadcrumbService;

        public DocumentTitle DocumentsTitle;

        public Guid selectedReportingYearId;
        public Guid selectedDepartmentId;
        public Guid selectedDocumentTypeId;

        public DocumentModel(FileManagerContext context,
            ILogger<DocumentModel> logger,
            ISmartBreadcrumbService breadcrumbService)
        {
            db = context;
            _logger = logger;
            _breadcrumbService = breadcrumbService;
        }
        public async Task<IActionResult> OnGetAsync(Guid yearId, Guid departmentId, Guid documentTypeId, Guid documentTitleId)
        {
            try
            {
                ViewData["BreadcrumbNode"] = await _breadcrumbService.GetDocumentBreadCrumbNodeAsync(
                    yearId,
                    departmentId,
                    documentTypeId,
                    documentTitleId);

                selectedReportingYearId = yearId;
                selectedDepartmentId = departmentId;
                selectedDocumentTypeId = documentTypeId;

                DocumentsTitle = await db.DocumentTitle.FirstOrDefaultAsync(dt => dt.Id.Equals(documentTitleId));


                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting document page");
                return NotFound();
            }
        }
    }
}