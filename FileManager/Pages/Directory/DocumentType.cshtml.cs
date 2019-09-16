using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.ReportingYearDocumentTitles;
using FileManager.Services.GetAccountDataService;
using FileManager.Services.SmartBreadcrumbService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Directory
{
    public class DocumentTypeModel : PageModel
    {
        private readonly FileManagerContext db;
        private readonly ILogger<DocumentTypeModel> _logger;
        private readonly ISmartBreadcrumbService _breadcrumbService;
        private readonly IGetAccountDataService _getAccountDataService;

        public List<DocumentTitle> DocumentsTitles;

        public Guid selectedReportingYearId;
        public Guid selectedDepartmentId;
        public Guid selectedDocumentTypeId;

        public DocumentTypeModel(FileManagerContext context,
            ILogger<DocumentTypeModel> logger,
            ISmartBreadcrumbService breadcrumbService,
            IGetAccountDataService getAccountDataService)
        {
            db = context;
            _logger = logger;
            _breadcrumbService = breadcrumbService;
            _getAccountDataService = getAccountDataService;
        }
        public async Task<IActionResult> OnGetAsync(Guid yearId, Guid departmentId, Guid documentTypeId)
        {
            try
            {
                if (!await _getAccountDataService.UserIsHaveAnyRoleOnDepartment(departmentId))
                {
                    return NotFound();
                }
                selectedReportingYearId = yearId;
                selectedDepartmentId = departmentId;
                selectedDocumentTypeId = documentTypeId;

                DocumentsTitles = db.DocumentTitle
                    .Where(dt => dt.DocumentTypeId.Equals(documentTypeId))
                    .ToList();

                ViewData["BreadcrumbNode"] = await _breadcrumbService.GetDocumentTypeBreadCrumbNodeAsync(
                    yearId,
                    departmentId,
                    documentTypeId);

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting documentType page");
                return NotFound();
            }


        }
    }
}