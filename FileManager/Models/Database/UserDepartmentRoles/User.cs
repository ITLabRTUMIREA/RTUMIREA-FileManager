﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models.Database.UserSystemRoles;
using FileManager.Models.Database.UserDepartmentRoles;
using Microsoft.AspNetCore.Identity;
using FileManager.Models.Database.DocumentStatuses;
using FileManager.Models.Database.DepartmentsDocuments;

namespace FileManager.Models.Database.UserDepartmentRoles
{
    public class User : IdentityUser<Guid>
    {
        public string FistName { get; set; }
        public string LastName { get; set; }

        public List<UserDepartmentRole> UserDepartmentRoles { get; set; }

        public List<UserSystemRole> UserSystemRoles { get; set; }

        public List<DocumentStatusHistory> DocumentStatusHistories { get; set; }
        public List<DepartmentsDocumentsVersion> DepartmentsDocumentsVersions { get; set; }
    }
}
