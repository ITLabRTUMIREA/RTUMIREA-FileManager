using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FileManager.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Models.Database.YearDocumentTitles;
using FileManager.Models.Database.DocumentStatus;

namespace FileManager.Models
{
    public class FileManagerContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<User> User { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<DepartmentsDocument> DepartmentsDocument { get; set; }
        public DbSet<DepartmentsDocumentsVersion> DepartmentsDocumentsVersion { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserDepartmentRole> UserRoleDepartment { get; set; }
        public DbSet<DocumentTitle> DocumentTitle { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<Year> Year { get; set; }
        public DbSet<YearDocumentTitle> YearDocumentTitle { get; set; }
        public DbSet<RoleStatus> RoleStatus { get; set; }
        public DbSet<DocumentStatusHistory> DocumentStatusHistory { get; set; }
        public DbSet<DocumentStatus> DocumentStatus { get; set; }
        public FileManagerContext (DbContextOptions<FileManagerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureDepartmentsDocument(builder);
            ConfigureDocumentStatus(builder);
            ConfigureYearDocumentTitle(builder);
            ConfigureUserRole(builder);

        }

        private static void ConfigureDepartmentsDocument(ModelBuilder builder)
        {
            builder.Entity<Department>(b => {
                b.HasKey(d => d.ID);
            });

            builder.Entity<DepartmentsDocument>(b => {

                b.HasKey(dd=>dd.ID);

                b.HasAlternateKey(dd => new {dd.DepartmentID,dd.YearDocumentTitleID });

                b.HasOne(dd => dd.YearDocumentTitle)
                    .WithMany(ydt => ydt.DepartmentsDocuments)
                    .HasForeignKey(dd => dd.YearDocumentTitleID);

                b.HasOne(dd => dd.Department)
                    .WithMany(d => d.DepartmentsDocuments)
                    .HasForeignKey(dd => dd.DepartmentID);
            });

            builder.Entity<DepartmentsDocumentsVersion>(b => {

                b.HasKey(ddv => ddv.ID);

                b.HasOne(ddv => ddv.DepartmentsDocument)
                    .WithMany(dd => dd.DepartmentsDocumentsVersions)
                    .HasForeignKey(ddv => ddv.DepartmentDocumentID);
            });
        }

        private static void ConfigureDocumentStatus(ModelBuilder builder)
        {
            builder.Entity<DocumentStatus>(b => {
                b.HasKey(ds => ds.ID);
            });

            builder.Entity<RoleStatus>(b => {

                b.HasKey(rs => new { rs.RoleID,rs.DocumentStatusID });

                b.HasOne(rs => rs.Role)
                   .WithMany(r => r.RoleStatuses)
                   .HasForeignKey(rs => rs.RoleID);

                b.HasOne(rs => rs.DocumentStatus)
                    .WithMany(ds => ds.RoleStatuses)
                    .HasForeignKey(rs => rs.DocumentStatusID);
            });

            builder.Entity<DocumentStatusHistory>(b => {

                b.HasKey(dsh =>new {dsh.DocumentStatusID,dsh.DepartmentsDocumentID });

                b.HasOne(dsh => dsh.DocumentStatus)
                   .WithMany(ds => ds.DocumentStatusHistories)
                   .HasForeignKey(dsh => dsh.DocumentStatusID);

                b.HasOne(dsh => dsh.DepartmentsDocument)
                    .WithMany(ds => ds.DocumentStatusHistories)
                    .HasForeignKey(dsh => dsh.DepartmentsDocumentID);
            });
        }

        private static void ConfigureYearDocumentTitle(ModelBuilder builder)
        {
            builder.Entity<Year>(b => {
                b.HasKey(y => y.ID);
            });

            builder.Entity<DocumentType>(b => {
                b.HasKey(dtp => dtp.ID);
            });

            builder.Entity<DocumentTitle>(b => {

                b.HasKey(dt => dt.ID);

                b.HasOne(dtp => dtp.DocumentType)
                   .WithMany(dt => dt.DocumentTitles)
                   .HasForeignKey(dtp => dtp.DocumentTypeID);
            });

            builder.Entity<YearDocumentTitle>(b => {

                b.HasKey(ydt=>ydt.ID);

                b.HasAlternateKey(ydt => new { ydt.DocumentTitleID, ydt.YearID });

                b.HasOne(ydt => ydt.DocumentTitle)
                    .WithMany(dt => dt.YearDocumentTitles)
                    .HasForeignKey(ydt => ydt.DocumentTitleID);

                b.HasOne(ydt => ydt.Year)
                    .WithMany(y => y.YearDocumentTitles)
                    .HasForeignKey(ydt => ydt.YearID);
            });
        }

        private static void ConfigureUserRole(ModelBuilder builder)
        {
            builder.Entity<UserDepartmentRole>(b => {

                b.HasOne(udt => udt.User)
                    .WithMany(u => u.UserRoleDepartments)
                    .HasForeignKey(udt => udt.UserId);

                b.HasOne(udt => udt.Department)
                    .WithMany(d => d.UserDepartmentRoles)
                    .HasForeignKey(udt => udt.DepartmentID);


                b.HasOne(udt => udt.Role)
                    .WithMany(r => r.UserRoleDepartments)
                    .HasForeignKey(udt => udt.RoleId);

                b.ToTable("AspNetUserDepartmentRoles");
            });

            builder.Entity<Role>(b => {
                b.ToTable("AspNetRoles");
            });

            builder.Entity<User>(b => {
                b.ToTable("AspNetUsers");
            });
        }

    }
}
