using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Models.Database.UserDepartmentRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.ViewModels.Account
{
    public class EditUserDepartmentRolesViewModel
    {
        public EditUserDepartmentRolesViewModel()
        {
            AllRoles = new List<Role>();
            UserDepartmentRoles = new List<UserDepartmentRole>();
        }

        public string UserId { get; set; }
        public string UserEmail { get; set; }

        public List<Role> AllRoles { get; set; }

        public List<Department> AllDepartments { get; set; }
        public string DepartmentId { get; set; }
        public List<UserDepartmentRole> UserDepartmentRoles { get; set; }
    }
}
