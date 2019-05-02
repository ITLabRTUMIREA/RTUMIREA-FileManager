using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Models
{
    public class UserRight
    {
        public ICollection<User> UserID { get; set; }
        public ICollection<Right> RightID { get; set; }
    }
}
