using SmartBreadcrumbs.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services.SmartBreadcrumbService
{
    public interface ISmartBreadcrumbService
    {
        RazorPageBreadcrumbNode GetReportingYearBreadCrumbNode(Guid yearId);
        RazorPageBreadcrumbNode GetDepartmentBreadCrumbNode(Guid departmentId, RazorPageBreadcrumbNode parentBreadcrumbNode);
    }
}
