using System;
using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.ViewModels.Account
{
    public class ChangeDepartmentViewModel
    {
        public ChangeDepartmentViewModel()
        {
            AllDepartments = new List<Departament>();
            UserDepartments = new List<UserRole>();
        }

        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<Departament> AllDepartments { get; set; }
        public List<UserRole> UserDepartments { get; set; }
    }
}
