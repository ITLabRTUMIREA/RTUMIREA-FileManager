using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
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

        public async Task<bool> UserIsAdminOnAnyDepartment()
        {
            if (Guid.TryParse(_userManager.GetUserId(_signInManager.Context.User), out Guid userId))
            {

                UserDepartmentRole currentUserWithAdminRole = await db.UserRoleDepartment
                .FirstOrDefaultAsync(urd => urd.UserId == userId && urd.Role.Name == "Admin");

                return currentUserWithAdminRole != null;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UserIsHaveAnyRoleOnDepartment(Guid departmentId)
        {
            if (Guid.TryParse(_userManager.GetUserId(_signInManager.Context.User), out Guid userId))
            {

                UserDepartmentRole currentUserWithAnyRole = await db.UserRoleDepartment
                    .FirstOrDefaultAsync(urd => urd.UserId == userId && urd.DepartmentId == departmentId);

                return currentUserWithAnyRole != null;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UserIsAdminOnDepartment(Guid departmentId)
        {
            if (Guid.TryParse(_userManager.GetUserId(_signInManager.Context.User), out Guid userId))
            {

                UserDepartmentRole currentUserWithAdminRoleOnDepartment = await db.UserRoleDepartment
                    .FirstOrDefaultAsync(urd => urd.UserId.Equals(userId)
                        && urd.Role.Name == "Admin"
                        && urd.DepartmentId == departmentId);
                return currentUserWithAdminRoleOnDepartment != null;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<Department> SelectDepartmentsWhereUserHaveAnyRole(IQueryable<Department> departments)
        {
            if (Guid.TryParse(_userManager.GetUserId(_signInManager.Context.User), out Guid userId))
            {
                return departments.Where(d => d.UserDepartmentRoles.Any(urd => urd.UserId == userId));
            }
            else
            {
                return default;
            }

        }
        public async Task<User> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(_signInManager.Context.User.Clone());
        }
    }
}
