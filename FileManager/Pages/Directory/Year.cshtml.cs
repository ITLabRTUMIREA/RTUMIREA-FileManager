using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
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

        public List<Department> Departments;
        public Guid selectedReportingYearId;
       


        public ReportingYearModel(FileManagerContext context,
            ILogger<ReportingYearModel> logger)
        {
            db = context;
            _logger = logger;
        }
        public IActionResult OnGet(Guid yearId)
        {
            try
            {
                selectedReportingYearId = yearId;

                Departments = db.Department.ToList();

                RazorPageBreadcrumbNode ReportingYearBreadCrumbNode = new RazorPageBreadcrumbNode("/Path",
                    db.ReportingYear
                    .FirstOrDefault(y => y.Id.Equals(yearId))
                    .Number.ToString())
                {
                    OverwriteTitleOnExactMatch = true,
                };

                ViewData["BreadcrumbNode"] = ReportingYearBreadCrumbNode;

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