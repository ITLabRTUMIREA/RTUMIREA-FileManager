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
<<<<<<< Updated upstream
            UserDepartments = new List<UserDepartment>();
=======
            UserDepartments = new List<UserRoleDepartment>();
>>>>>>> Stashed changes
        }

        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<Department> AllDepartments { get; set; }
<<<<<<< Updated upstream
        public List<UserDepartment> UserDepartments { get; set; }
=======
        public List<UserRoleDepartment> UserDepartments { get; set; }
>>>>>>> Stashed changes
    }
}
