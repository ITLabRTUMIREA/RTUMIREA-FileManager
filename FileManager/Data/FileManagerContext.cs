using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FileManager.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FileManager.Models
{
    public class FileManagerContext : IdentityDbContext<User, Role, Guid>
    {
        public FileManagerContext (DbContextOptions<FileManagerContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
