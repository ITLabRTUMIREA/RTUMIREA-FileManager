using FileManager.Models;
using FileManager.Pages.Directory;
using SmartBreadcrumbs.Extensions;
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
            return new RazorPageBreadcrumbNode(ReflectionExtensions
                .ExtractRazorPageKey(typeof(ReportingYearModel)),
                        db.ReportingYear
                        .FirstOrDefault(y => y.Id.Equals(yearId))
                        .Number.ToString())
            {
                OverwriteTitleOnExactMatch = true,
                RouteValues = new { yearId = yearId }

            };
        }
        public RazorPageBreadcrumbNode GetDepartmentBreadCrumbNode(Guid yearId,
            Guid departmentId)
        {
            return new RazorPageBreadcrumbNode(ReflectionExtensions
                .ExtractRazorPageKey(typeof(DepartmentModel)),
                    db.Department
                    .FirstOrDefault(y => y.Id.Equals(departmentId)).Name)
            {
                OverwriteTitleOnExactMatch = true,
                Parent = GetReportingYearBreadCrumbNode(yearId),
                RouteValues = new
                {
                    yearId = yearId,
                    departmentId = departmentId
                }

            };
        }
        public RazorPageBreadcrumbNode GetDocumentTypeBreadCrumbNode(Guid yearId,
            Guid departmentId,
            Guid documentTypeId)
        {
            return new RazorPageBreadcrumbNode(ReflectionExtensions
                .ExtractRazorPageKey(typeof(DocumentTypeModel)),
                    db.DocumentType
                    .FirstOrDefault(y => y.Id.Equals(documentTypeId)).Type)
            {
                OverwriteTitleOnExactMatch = true,
                Parent = GetDepartmentBreadCrumbNode(yearId, departmentId),
                RouteValues = new
                {
                    yearId = yearId,
                    departmentId = departmentId,
                    documentTypeId = documentTypeId
                }

            };
        }
    }
}
