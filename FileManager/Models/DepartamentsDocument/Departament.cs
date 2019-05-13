using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models
{
    public class Departament
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public Guid DepartamentsDocumentID { get; set; }
    }
}
