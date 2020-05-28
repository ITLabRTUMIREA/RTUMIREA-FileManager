using FileManager.Models;
using FileManager.Pages.Directory;
using Microsoft.EntityFrameworkCore;
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

        public async Task<RazorPageBreadcrumbNode> GetReportingYearBreadCrumbNodeAsync(Guid yearId)
        {
            var Year = (await db.ReportingYear
                            .FirstOrDefaultAsync(y => y.Id.Equals(yearId))).Number.ToString();
            return new RazorPageBreadcrumbNode(ReflectionExtensions
                .ExtractRazorPageKey(typeof(ReportingYearModel)), Year)
            {
                OverwriteTitleOnExactMatch = true,
                RouteValues = new { yearId }

            };
        }
        public async Task<RazorPageBreadcrumbNode> GetDepartmentBreadCrumbNodeAsync(Guid yearId,
            Guid departmentId)
        {
            return new RazorPageBreadcrumbNode(ReflectionExtensions
                .ExtractRazorPageKey(typeof(DepartmentModel)),
                    (await db.Department
                    .FirstOrDefaultAsync(y => y.Id.Equals(departmentId))).Name)
            {
                OverwriteTitleOnExactMatch = true,
                Parent = await GetReportingYearBreadCrumbNodeAsync(yearId),
                RouteValues = new
                {
                    yearId,
                    departmentId
                }

            };
        }
        public async Task<RazorPageBreadcrumbNode> GetDocumentTypeBreadCrumbNodeAsync(Guid yearId,
            Guid departmentId,
            Guid documentTypeId)
        {
            return new RazorPageBreadcrumbNode(ReflectionExtensions
                .ExtractRazorPageKey(typeof(DocumentTypeModel)),
                    (await db.DocumentType
                    .FirstOrDefaultAsync(y => y.Id.Equals(documentTypeId))).Type)
            {
                OverwriteTitleOnExactMatch = true,
                Parent = await GetDepartmentBreadCrumbNodeAsync(yearId, departmentId),
                RouteValues = new
                {
                    yearId,
                    departmentId,
                    documentTypeId
                }

            };
        }
        public async Task<RazorPageBreadcrumbNode> GetDocumentBreadCrumbNodeAsync(Guid yearId,
            Guid departmentId,
            Guid documentTypeId,
            Guid documentTitleId)
        {
            return new RazorPageBreadcrumbNode(ReflectionExtensions
                .ExtractRazorPageKey(typeof(DocumentModel)),
                   (await db.DocumentTitle
                    .FirstOrDefaultAsync(y => y.Id.Equals(documentTitleId))).Name)
            {
                OverwriteTitleOnExactMatch = true,
                Parent = await GetDocumentTypeBreadCrumbNodeAsync(yearId, departmentId, documentTypeId),
                RouteValues = new
                {
                    yearId,
                    departmentId,
                    documentTypeId,
                    documentTitleId

                }

            };
        }
    }
}
