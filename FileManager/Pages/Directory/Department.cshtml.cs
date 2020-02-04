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
using Microsoft.EntityFrameworkCore;
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
        private readonly IGetAccountDataService _getAccountDataService;

        public List<DocumentType> DocumentTypes;

        public Guid selectedReportingYearId;
        public Guid selectedDepartmentId;

        public DepartmentModel(FileManagerContext context,
            ILogger<DepartmentModel> logger,
            ISmartBreadcrumbService breadcrumbService,
            IGetAccountDataService getAccountDataService)
        {
            db = context;
            _logger = logger;
            _breadcrumbService = breadcrumbService;
            _getAccountDataService = getAccountDataService;
        }
        public async Task<IActionResult> OnGetAsync(Guid yearId, Guid departmentId)
        {
            try
            {
                if (!await _getAccountDataService.UserIsHaveAnyRoleOnDepartment(departmentId))
                {
                    return NotFound();
                }
                selectedReportingYearId = yearId;
                selectedDepartmentId = departmentId;

                // Get all documentTitles in current year
                var allTitlesInReportingYear = await db.ReportingYearDocumentTitle
                    .Where(rydt => rydt.ReportingYearId == yearId)
                    .ToListAsync();

                // Show document types, which have any document Title connected to current year 
                DocumentTypes = await db.DocumentType
                    .Include(dt => dt.DocumentTitles)
                    .Where(dtype => dtype.DocumentTitles.Exists(dt => allTitlesInReportingYear.Exists(tit=>tit.DocumentTitleId == dt.Id)))
                    .ToListAsync();

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