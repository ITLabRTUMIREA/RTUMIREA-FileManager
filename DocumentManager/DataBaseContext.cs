using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManager
{
    public class DataBaseContext : IdentityDbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
