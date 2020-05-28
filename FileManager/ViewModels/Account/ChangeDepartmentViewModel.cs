using System;
using System.Collections.Generic;
using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Models.Database.UserDepartmentRoles;

namespace FileManager.ViewModels.Account
{
    public class ChangeDepartmentViewModel
    {
        public ChangeDepartmentViewModel()
        {
            AllDepartments = new List<Department>();
            UserDepartmentRoles = new List<UserDepartmentRole>();
        }

        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<Department> AllDepartments { get; set; }
        public List<UserDepartmentRole> UserDepartmentRoles { get; set; }
    }
}
