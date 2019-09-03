using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.ReportingYearDocumentTitles;
using FileManager.Services.SmartBreadcrumbService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartBreadcrumbs.Attributes;
using SmartBreadcrumbs.Nodes;

namespace FileManager.Pages.Directory
{
    public class DepartmentModel : PageModel
    {
        private readonly FileManagerContext db;
        private readonly ILogger<DepartmentModel> _logger;
        private readonly ISmartBreadcrumbService _breadcrumbService;

        public List<DocumentType> DocumentTypes;

        public Guid selectedReportingYearId;
        public Guid selectedDepartmentId;

        public DepartmentModel(FileManagerContext context,
            ILogger<DepartmentModel> logger,
            ISmartBreadcrumbService breadcrumbService)
        {
            db = context;
            _logger = logger;
            _breadcrumbService = breadcrumbService;
        }
        public async Task<IActionResult> OnGetAsync(Guid yearId, Guid departmentId)
        {
            try
            {
                selectedReportingYearId = yearId;
                selectedDepartmentId = departmentId;

                DocumentTypes = db.DocumentType.ToList();

                ViewData["BreadcrumbNode"] = await _breadcrumbService.GetDepartmentBreadCrumbNodeAsync(
                    yearId,
                    departmentId);

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting department page");
                return NotFound();
            }


        }
    }
}