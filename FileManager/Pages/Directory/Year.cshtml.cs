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
    public class YearModel : PageModel
    {
        private readonly FileManagerContext db;
        private readonly ILogger<YearModel> _logger;

        public List<Department> Departments;
        public Guid selectedYearId;
       


        public YearModel(FileManagerContext context,
            ILogger<YearModel> logger)
        {
            db = context;
            _logger = logger;
        }
        public IActionResult OnGet(Guid yearId)
        {
            try
            {
                selectedYearId = yearId;

                Departments = db.Department.ToList();

                RazorPageBreadcrumbNode YearBreadCrumbNode = new RazorPageBreadcrumbNode("/Path",
                    db.Year
                    .FirstOrDefault(y => y.Id.Equals(yearId))
                    .Number.ToString())
                {
                    OverwriteTitleOnExactMatch = true,
                };

                ViewData["BreadcrumbNode"] = YearBreadCrumbNode;

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