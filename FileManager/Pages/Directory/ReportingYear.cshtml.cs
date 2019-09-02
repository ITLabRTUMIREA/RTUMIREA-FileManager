using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Services.SmartBreadcrumbService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public List<Department> Departments;
        public Guid selectedReportingYearId;


        public ReportingYearModel(FileManagerContext context,
            ILogger<ReportingYearModel> logger,
            ISmartBreadcrumbService breadcrumbService)
        {
            db = context;
            _logger = logger;
            _breadcrumbService = breadcrumbService;
        }
        public IActionResult OnGet(Guid yearId)
        {
            try
            {
                selectedReportingYearId = yearId;

                Departments = db.Department.ToList();

                ViewData["BreadcrumbNode"] = _breadcrumbService.GetReportingYearBreadCrumbNode(yearId);

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