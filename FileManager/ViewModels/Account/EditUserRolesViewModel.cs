using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.Models.Database.UserSystemRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.ViewModels.Account
{
    public class EditUserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<SystemRole> AllSystemRoles { get; set; }
        public IList<string> UserSystemRoles { get; set; }
        public EditUserRolesViewModel()
        {
            AllSystemRoles = new List<SystemRole>();
            UserSystemRoles = new List<string>();
        }
    }
}
