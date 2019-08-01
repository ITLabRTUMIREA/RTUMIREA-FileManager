using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileManager.ViewModels;
using FileManager.ViewModels.Account;
using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.Models.Database.DepartmentsDocuments;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Pages.Account.Roles
{
    public class EditModel : PageModel
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly FileManagerContext db;
        public EditModel(RoleManager<Role> roleManager, UserManager<User> userManager, FileManagerContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            db = context;
        }

        public EditUserDepartmentRolesViewModel EditUserDepartmentRolesViewModel = null;
        public string PickedDepartmentId = "";

        public async Task<IActionResult> OnGetAsync(string userid)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userid);
            if (user != null)
            {
                var allRoles = _roleManager.Roles.ToList();
                List<Department> allDepartments = db.Department.ToList();
                EditUserDepartmentRolesViewModel = new EditUserDepartmentRolesViewModel
                {
                    UserId = user.Id.ToString(),
                    UserEmail = user.Email,
                    UserDepartmentRoles = new List<string>() { },
                    AllRoles = allRoles,
                    AllDepartments = allDepartments

                };
                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnGetGetRolesAsync(string userid, string departmentid)
        {
            PickedDepartmentId = departmentid;
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userid);
            if (user != null)
            {
                // получем список ролей пользователя
                var allRoles = _roleManager.Roles.ToList();
                List<Department> allDepartments = db.Department.ToList();
                EditUserDepartmentRolesViewModel = new EditUserDepartmentRolesViewModel
                {
                    UserId = user.Id.ToString(),
                    UserEmail = user.Email,
                    UserDepartmentRoles = await db.UserRoleDepartment
                        .Where(urd => urd.UserId.Equals(user.Id) && urd.DepartmentId.ToString() == departmentid)
                        .Select(urd => urd.Role.Id.ToString().ToLower())
                        .ToListAsync(),
                    AllRoles = allRoles,
                    AllDepartments = allDepartments,
                    DepartmentId = departmentid

                };
                return Page();
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUpdateRolesAsync(string userId, string departmentId, List<string> roles)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var UserDepartmentRoles = await db.UserRoleDepartment
                    .Where(urd => urd.UserId.Equals(Guid.Parse(userId)) && urd.DepartmentId.ToString() == departmentId)
                    .Select(urd => urd.Role.Id.ToString().ToLower())
                    .ToListAsync();

                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(UserDepartmentRoles);
                // получаем роли, которые были удалены
                var removedRoles = UserDepartmentRoles.Except(roles);

                foreach (var role in addedRoles)
                {
                    if (role != null)
                    {
                        db.UserRoleDepartment.Add(new UserDepartmentRole()
                        {
                            UserId = Guid.Parse(userId),
                            DepartmentId = Guid.Parse(departmentId),
                            RoleId = Guid.Parse(role)
                        });
                    }

                }

                foreach (var role in removedRoles)
                {
                    if (role != null)
                    {
                        db.UserRoleDepartment.Remove(new UserDepartmentRole()
                        {
                            UserId = Guid.Parse(userId),
                            DepartmentId = Guid.Parse(departmentId),
                            RoleId = Guid.Parse(role)
                        });
                    }
                }
                // TODO Make possible creating record with the same userid and roleid and different departamentid,
                //  problem is that database creating waste key CONSTRAINT [AK_AspNetUserDepartmentRole_UserId_RoleId] UNIQUE NONCLUSTERED ([UserId] ASC, [RoleId] ASC),

                await db.SaveChangesAsync();

                return RedirectToPage("UserList");
            }

            return NotFound();
        }
    }
}