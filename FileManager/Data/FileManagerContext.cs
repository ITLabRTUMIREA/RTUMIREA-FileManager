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
        public FileManagerContext (DbContextOptions<FileManagerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureDepartamentsDocument(builder);
            ConfigureDocumentStatus(builder);
            ConfigureYearDocumentTitle(builder);
            ConfigureUserRole(builder);

        }

        private static void ConfigureDepartamentsDocument(ModelBuilder builder)
        {
            builder.Entity<Departament>(b => {
                b.HasKey(d => d.ID);
            });

            builder.Entity<DepartamentsDocument>(b => {

                b.HasKey(dd=>dd.ID);

                b.HasAlternateKey(dd => new {dd.DepartamentID,dd.YearDocumentTitleID });

                b.HasOne(dd => dd.YearDocumentTitle)
                    .WithMany(ydt => ydt.DepartamentsDocuments)
                    .HasForeignKey(dd => dd.YearDocumentTitleID);

                b.HasOne(dd => dd.Departament)
                    .WithMany(d => d.DepartamentsDocuments)
                    .HasForeignKey(dd => dd.DepartamentID);
            });

            builder.Entity<DepartamentsDocumentsVersion>(b => {

                b.HasKey(ddv => ddv.ID);

                b.HasOne(ddv => ddv.DepartamentsDocument)
                    .WithMany(dd => dd.DepartamentsDocumentsVersions)
                    .HasForeignKey(ddv => ddv.DepartamentDocumentID);
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

                b.HasKey(dsh =>new {dsh.DocumentStatusID,dsh.DepartamentsDocumentID });

                b.HasOne(dsh => dsh.DocumentStatus)
                   .WithMany(ds => ds.DocumentStatusHistories)
                   .HasForeignKey(dsh => dsh.DocumentStatusID);

                b.HasOne(dsh => dsh.DepartamentsDocument)
                    .WithMany(ds => ds.DocumentStatusHistories)
                    .HasForeignKey(dsh => dsh.DepartamentsDocumentID);
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
            builder.Entity<UserRole>(b=> {
              //  b.HasKey(ur=>new { ur.DepartamentID,ur.RoleId,ur.UserId });

                b.HasOne(ur => ur.Departament)
                    .WithMany(d => d.UserRoles)
                    .HasForeignKey(ur => ur.DepartamentID);
                b.ToTable("AspNetUserRoles");
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
