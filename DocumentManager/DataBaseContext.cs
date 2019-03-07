using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManager
{
    public class DataBaseContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }

        DbSet<User> Clients { get; set; }
        DbSet<Document> Documents { get; set; }
        DbSet<Division> Divisions { get; set; }


    }
}
