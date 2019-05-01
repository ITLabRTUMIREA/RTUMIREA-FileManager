using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
