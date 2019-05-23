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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

        public DbSet<User> User { get; set; }
        public DbSet<Departament> Departament { get; set; }
        public DbSet<DepartamentsDocument> DepartamentsDocument { get; set; }
        public DbSet<DepartamentsDocumentsVersion> DepartamentsDocumentsVersion { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<DocumentTitle> DocumentTitle { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<Year> Year { get; set; }
        public DbSet<YearDocumentTitle> YearDocumentTitle { get; set; }
        public DbSet<RoleStatus> RoleStatus { get; set; }
        public DbSet<DocumentStatusHistory> DocumentStatusHistory { get; set; }
        public DbSet<DocumentStatus> DocumentStatus { get; set; }
    }
}
