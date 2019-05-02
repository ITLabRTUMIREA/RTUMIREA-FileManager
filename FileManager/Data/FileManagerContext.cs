using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FileManager.Models.ViewModels;

namespace FileManager.Models
{
    public class FileManagerContext : DbContext
    {
        public FileManagerContext (DbContextOptions<FileManagerContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<Right> Right { get; set; }

        public DbSet<UserRight> UserRight { get; set; }
    }
}
