using FileManager.Models;
using FileManager.Models.Database.UserDepartmentRoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileManager.Services.GetAccountDataService
{
    public class GetAccountDataService : IGetAccountDataService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly FileManagerContext db;
        private readonly ILogger<GetAccountDataService> _logger;

        public GetAccountDataService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            FileManagerContext context,
            ILogger<GetAccountDataService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
            _logger = logger;
        }
        public bool IsSystemAdmin()
        {
            return _signInManager.Context.User.IsInRole("SystemAdmin");
        }

        public async Task<bool> IsAdminOnAnyDepartment()
        {
            Guid userId = (await _userManager.GetUserAsync(_signInManager.Context.User.Clone())).Id;

            UserDepartmentRole currentUserWithAdminRole = await db.UserRoleDepartment
                .FirstOrDefaultAsync(urd => urd.UserId.Equals(userId) && urd.Role.Name == "Admin");

            if (currentUserWithAdminRole != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> IsAdminOnDepartment(Guid departmentId)
        {
            Guid userId = (await _userManager.GetUserAsync(_signInManager.Context.User.Clone())).Id;

            UserDepartmentRole currentUserWithAdminRoleOnDepartment = await db.UserRoleDepartment
                .FirstOrDefaultAsync(urd => urd.UserId.Equals(userId)
                    && urd.Role.Name == "Admin"
                    && urd.DepartmentId.Equals(departmentId));

            if (currentUserWithAdminRoleOnDepartment != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<User> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(_signInManager.Context.User.Clone());
        }
    }
}
