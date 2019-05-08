using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FileManager.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FistName { get; set; }
        public string LastName { get; set; }
    }
}
