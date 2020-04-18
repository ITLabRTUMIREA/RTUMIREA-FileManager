using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Models.Database.UserDepartmentRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services.GetAccountDataService
{
   public interface IGetAccountDataService
    {
        bool IsSystemAdmin();

        Task<bool> UserIsAdminOnAnyDepartment();

        Task<bool> UserIsHaveAnyRoleOnDepartment(Guid departmentId);

        Task<bool> UserIsAdminOnDepartment(Guid departmentId);

        Task<User> GetCurrentUser();

        IQueryable<Department> SelectDepartmentsWhereUserHaveAnyRole(IQueryable<Department> departments);
        Task<bool> UserIsCheckerOnDepartment(Guid departmentId);

    }
}
