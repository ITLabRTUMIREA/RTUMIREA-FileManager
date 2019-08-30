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
using FileManager.Models.Database.ReportingYearDocumentTitles;
using FileManager.Models.Database.DocumentStatus;
using FileManager.Models.Database.UserSystemRoles;

namespace FileManager.Models
{
    public class FileManagerContext : IdentityDbContext<User, SystemRole, Guid, IdentityUserClaim<Guid>, UserSystemRole,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DbSet<User> User { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<DepartmentsDocument> DepartmentsDocument { get; set; }
        public DbSet<DepartmentsDocumentsVersion> DepartmentsDocumentsVersion { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<SystemRole> SystemRole { get; set; }
        public DbSet<UserDepartmentRole> UserRoleDepartment { get; set; }
        public DbSet<DocumentTitle> DocumentTitle { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<ReportingYear> ReportingYear { get; set; }
        public DbSet<UserSystemRole> UserSystemRole { get; set; }
        public DbSet<ReportingYearDocumentTitle> ReportingYearDocumentTitle { get; set; }
        public DbSet<RoleStatus> RoleStatus { get; set; }
        public DbSet<DocumentStatusHistory> DocumentStatusHistory { get; set; }
        public DbSet<DocumentStatus> DocumentStatus { get; set; }
        public FileManagerContext(DbContextOptions<FileManagerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureDepartmentsDocument(builder);
            ConfigureDocumentStatus(builder);
            ConfigureReportingYearDocumentTitle(builder);
            ConfigureUserDepartmentRoles(builder);
            ConfigureUserSystemRoles(builder);

        }

        private static void ConfigureDepartmentsDocument(ModelBuilder builder)
        {

            builder.Entity<DepartmentsDocument>(b =>
            {

                b.HasKey(dd => dd.Id);

                b.HasAlternateKey(dd => new { dd.DepartmentId, dd.ReportingYearDocumentTitleId });

                b.HasOne(dd => dd.ReportingYearDocumentTitle)
                    .WithMany(ydt => ydt.DepartmentsDocuments)
                    .HasForeignKey(dd => dd.ReportingYearDocumentTitleId);

                b.HasOne(dd => dd.Department)
                    .WithMany(d => d.DepartmentsDocuments)
                    .HasForeignKey(dd => dd.DepartmentId);
            });

            builder.Entity<DepartmentsDocumentsVersion>(b =>
            {

                b.HasKey(ddv => ddv.Id);

                b.HasOne(ddv => ddv.DepartmentsDocument)
                    .WithMany(dd => dd.DepartmentsDocumentsVersions)
                    .HasForeignKey(ddv => ddv.DepartmentDocumentId);
            });
        }

        private static void ConfigureDocumentStatus(ModelBuilder builder)
        {
            builder.Entity<DocumentStatus>(b =>
            {
                b.HasKey(ds => ds.Id);
            });

            builder.Entity<RoleStatus>(b =>
            {

                b.HasKey(rs => new { rs.RoleId, rs.DocumentStatusId });

                b.HasOne(rs => rs.Role)
                   .WithMany(r => r.RoleStatuses)
                   .HasForeignKey(rs => rs.RoleId);

                b.HasOne(rs => rs.DocumentStatus)
                    .WithMany(ds => ds.RoleStatuses)
                    .HasForeignKey(rs => rs.DocumentStatusId);
            });

            builder.Entity<DocumentStatusHistory>(b =>
            {

                b.HasKey(dsh => new { dsh.DocumentStatusId, dsh.DepartmentsDocumentId });

                b.HasOne(dsh => dsh.DocumentStatus)
                   .WithMany(ds => ds.DocumentStatusHistories)
                   .HasForeignKey(dsh => dsh.DocumentStatusId);

                b.HasOne(dsh => dsh.DepartmentsDocument)
                    .WithMany(ds => ds.DocumentStatusHistories)
                    .HasForeignKey(dsh => dsh.DepartmentsDocumentId);
            });
        }

        private static void ConfigureReportingYearDocumentTitle(ModelBuilder builder)
        {
            builder.Entity<ReportingYear>(b =>
            {
                b.HasKey(y => y.Id);
            });

            builder.Entity<DocumentType>(b =>
            {
                b.HasKey(dtp => dtp.Id);
            });

            builder.Entity<DocumentTitle>(b =>
            {

                b.HasKey(dt => dt.Id);

                b.HasOne(dtp => dtp.DocumentType)
                   .WithMany(dt => dt.DocumentTitles)
                   .HasForeignKey(dtp => dtp.DocumentTypeId);
            });

            builder.Entity<ReportingYearDocumentTitle>(b =>
            {

                b.HasKey(ydt => ydt.Id);

                b.HasAlternateKey(ydt => new { ydt.DocumentTitleId, ydt.ReportingYearId });

                b.HasOne(ydt => ydt.DocumentTitle)
                    .WithMany(dt => dt.ReportingYearDocumentTitles)
                    .HasForeignKey(ydt => ydt.DocumentTitleId);

                b.HasOne(ydt => ydt.ReportingYear)
                    .WithMany(y => y.ReportingYearDocumentTitles)
                    .HasForeignKey(ydt => ydt.ReportingYearId);
            });
        }

        private static void ConfigureUserDepartmentRoles(ModelBuilder builder)

        {
            builder.Entity<UserDepartmentRole>(b =>
            {
                b.HasKey(udr => new { udr.UserId, udr.RoleId, udr.DepartmentId });


                b.HasOne(udt => udt.User)
                    .WithMany(u => u.UserDepartmentRoles)
                    .HasForeignKey(udt => udt.UserId)
                    .IsRequired();

                b.HasOne(udt => udt.Department)
                    .WithMany(d => d.UserDepartmentRoles)
                    .HasForeignKey(udt => udt.DepartmentId)
                    .IsRequired();


                b.HasOne(udt => udt.Role)
                    .WithMany(r => r.UserDepartmentRoles)
                    .HasForeignKey(udt => udt.RoleId)
                    .IsRequired();

                b.ToTable("AspNetUserDepartmentRoles");
            });


            builder.Entity<Department>(b =>
            {
                b.HasKey(d => d.Id);
            });

            builder.Entity<Role>(b =>
            {
                b.HasKey(r => r.Id);

                b.ToTable("AspNetRoles");
            });

            builder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);

                b.ToTable("AspNetUsers");
            });
        }
        private static void ConfigureUserSystemRoles(ModelBuilder builder)
        {
            builder.Entity<SystemRole>(b =>
            {
                b.HasKey(r => r.Id);

                b.ToTable("AspNetSystemRoles");
            });
            builder.Entity<UserSystemRole>(b =>
            {
                b.HasKey(udr => new { udr.UserId, udr.RoleId });


                b.HasOne(udt => udt.User)
                    .WithMany(u => u.UserSystemRoles)
                    .HasForeignKey(udt => udt.UserId)
                    .IsRequired();

                b.HasOne(udt => udt.SystemRole)
                    .WithMany(r => r.UserSystemRoles)
                    .HasForeignKey(udt => udt.RoleId)
                    .IsRequired();

                b.ToTable("AspNetUserSystemRoles");
            });
        }

    }
}