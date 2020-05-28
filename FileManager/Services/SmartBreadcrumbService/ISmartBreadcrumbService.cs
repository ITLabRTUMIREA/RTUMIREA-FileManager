using SmartBreadcrumbs.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services.SmartBreadcrumbService
{
    public interface ISmartBreadcrumbService
    {
        Task<RazorPageBreadcrumbNode> GetReportingYearBreadCrumbNodeAsync(Guid yearId);
        Task<RazorPageBreadcrumbNode> GetDepartmentBreadCrumbNodeAsync(Guid yearId,
            Guid departmentId);
        Task<RazorPageBreadcrumbNode> GetDocumentTypeBreadCrumbNodeAsync(Guid yearId,
            Guid departmentId,
            Guid documentTypeId);
        Task<RazorPageBreadcrumbNode> GetDocumentBreadCrumbNodeAsync(Guid yearId,
            Guid departmentId,
            Guid documentTypeId,
            Guid documentTitleId);
    }
}
