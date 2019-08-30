using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.Models.Database.UserSystemRoles;
using FileManager.Models.Database.ReportingYearDocumentTitles;
using FileManager.Models.DbInitialize;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services.DbInitializeService
{
    public class DbInitializeService: IDbInitializeService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<SystemRole> _roleManager;
        private readonly DbInitializeMainUser _mainUserData;
        private readonly FileManagerContext db;

        public DbInitializeService(UserManager<User> userManager,
            RoleManager<SystemRole> roleManager,
            IOptions<DbInitializeMainUser> mainUserData,
            FileManagerContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mainUserData = mainUserData.Value;
            db = context;
        }
        public async Task Initialize()
        {
            await InitializeSystemAdmin();

            await InitializeReportingYearsList();
        }

        private async Task InitializeSystemAdmin()
        {
            if (await _roleManager.FindByNameAsync("SystemAdmin") == null)
            {
                await _roleManager.CreateAsync(new SystemRole("SystemAdmin"));
            }
            if (await _userManager.FindByNameAsync(_mainUserData.Email) == null)
            {
                User admin = new User
                {
                    Email = _mainUserData.Email,
                    UserName = _mainUserData.Email,
                    FistName = _mainUserData.FirstName,
                    LastName = _mainUserData.LastName,
                    EmailConfirmed = true
                };
                IdentityResult result = await _userManager.CreateAsync(admin, _mainUserData.Password);
                if (result.Succeeded)
                {
                    IdentityResult res = await _userManager.AddToRoleAsync(admin, "SystemAdmin");
                }
            }
        }

        private async Task InitializeReportingYearsList()
        {
            if(db.ReportingYear.FirstOrDefault(y => y.Number == DateTime.Now.ReportingYear) == null)
            {
                await db.ReportingYear.AddAsync(new ReportingYear(DateTime.Now.ReportingYear));

                await db.SaveChangesAsync();
            }
        }
    }
}
