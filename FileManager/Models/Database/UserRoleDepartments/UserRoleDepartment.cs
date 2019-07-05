using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FileManager.Models.Database.UserRole
{
    public class UserRoleDepartment : IdentityUserRole<Guid>
    {
<<<<<<< Updated upstream:FileManager/Models/Database/UserRole/UserRole.cs
=======
        public Guid DepartmentID { get; set; }
        public Department Department { get; set; }
>>>>>>> Stashed changes:FileManager/Models/Database/UserRoleDepartments/UserRoleDepartment.cs
    }
}
