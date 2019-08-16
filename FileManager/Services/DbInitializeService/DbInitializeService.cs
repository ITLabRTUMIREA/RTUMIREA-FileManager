using FileManager.Models.Database.UserDepartmentRoles;
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
        private readonly RoleManager<Role> _roleManager;
        private readonly DbInitializeMainUser _mainUserData;

        public DbInitializeService(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<DbInitializeMainUser> mainUserData)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mainUserData = mainUserData.Value;
        }
        public async Task Initialize()
        {
            if (await _roleManager.FindByNameAsync("SystemAdmin") == null)
            {
                await _roleManager.CreateAsync(new Role("SystemAdmin"));
            }
            if (await _userManager.FindByNameAsync(_mainUserData.Email) == null)
            {
                User admin = new User { Email = _mainUserData.Email,
                    UserName = _mainUserData.Email,
                    FistName = _mainUserData.FirstName,
                    LastName = _mainUserData.LastName,
                    EmailConfirmed = true};
                IdentityResult result = await _userManager.CreateAsync(admin, _mainUserData.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "SystemAdmin");
                }
            }
        }
    }
}
