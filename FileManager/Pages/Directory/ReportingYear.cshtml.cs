using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
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
    public class ReportingYearModel : PageModel
    {
        private readonly FileManagerContext db;
        private readonly ILogger<ReportingYearModel> _logger;
        private readonly ISmartBreadcrumbService _breadcrumbService;
        private readonly IGetAccountDataService _getAccountDataService;

        public List<Department> Departments;
        public Guid selectedReportingYearId;


        public ReportingYearModel(FileManagerContext context,
            ILogger<ReportingYearModel> logger,
            ISmartBreadcrumbService breadcrumbService,
            IGetAccountDataService getAccountDataService)
        {
            db = context;
            _logger = logger;
            _breadcrumbService = breadcrumbService;
            _getAccountDataService = getAccountDataService;
        }
        public async Task<IActionResult> OnGetAsync(Guid yearId)
        {
            try
            {
                selectedReportingYearId = yearId;

                if (_getAccountDataService.IsSystemAdmin())
                {
                    Departments = await db.Department.ToListAsync();
                }
                else
                {
                    Departments = await _getAccountDataService.SelectDepartmentsWhereUserHaveAnyRole(db.Department).ToListAsync();
                }

                var CurrentBreadCrumb = await _breadcrumbService.GetReportingYearBreadCrumbNodeAsync(yearId);
                ViewData["BreadcrumbNode"] = CurrentBreadCrumb;
                ViewData["Title"] = CurrentBreadCrumb.Title;

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting year page");
                return NotFound();
            }


        }
    }
}