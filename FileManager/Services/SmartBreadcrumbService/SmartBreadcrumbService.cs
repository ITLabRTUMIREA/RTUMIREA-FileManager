using FileManager.Models;
using FileManager.Pages.Directory;
using SmartBreadcrumbs.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services.SmartBreadcrumbService
{
    public class SmartBreadcrumbService : ISmartBreadcrumbService
    {
        private readonly FileManagerContext db;

        public SmartBreadcrumbService(FileManagerContext context)
        {
            db = context;
        }

        public RazorPageBreadcrumbNode GetReportingYearBreadCrumbNode(Guid yearId)
        {
            return new RazorPageBreadcrumbNode(SmartBreadcrumbs
                .Extensions
                .ReflectionExtensions
                .ExtractRazorPageKey(typeof(ReportingYearModel)),
                        db.ReportingYear
                        .FirstOrDefault(y => y.Id.Equals(yearId))
                        .Number.ToString())
            {
                OverwriteTitleOnExactMatch = true,
               RouteValues = new {yearId = yearId}

            };
        }
        public RazorPageBreadcrumbNode GetDepartmentBreadCrumbNode(Guid yearId,Guid departmentId, RazorPageBreadcrumbNode parentBreadcrumbNode)
        {
            return new RazorPageBreadcrumbNode("/Path",
                    db.Department
                    .FirstOrDefault(y => y.Id.Equals(departmentId)).Name)
            {
                OverwriteTitleOnExactMatch = true,
                Parent = parentBreadcrumbNode,
                RouteValues = new { yearId = yearId,
                    departmentId = departmentId}
                
            };
        }
    }
}
