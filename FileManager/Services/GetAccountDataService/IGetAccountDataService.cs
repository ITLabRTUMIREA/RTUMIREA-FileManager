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

        Task<bool> IsAdminOnAnyDepartment();

        Task<bool> IsAdminOnDepartment(Guid departmentId);

        Task<User> GetCurrentUser();


    }
}
