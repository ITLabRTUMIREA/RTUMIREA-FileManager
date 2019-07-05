using System;
using System.Collections.Generic;
using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Models.Database.UserDepartments;

namespace FileManager.ViewModels.Account
{
    public class ChangeDepartmentViewModel
    {
        public ChangeDepartmentViewModel()
        {
            AllDepartments = new List<Department>();
            UserDepartments = new List<UserDepartment>();
        }

        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<Department> AllDepartments { get; set; }
        public List<UserDepartment> UserDepartments { get; set; }
    }
}
