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
                // получем список ролей пользователя
                var UserDepartmentRoles = await db.UserRoleDepartment.Where(urd => urd.User.Equals(user)).ToListAsync();
                var allRoles = _roleManager.Roles.ToList();
                List<Department> allDepartments = db.Department.ToList();
                EditUserDepartmentRolesViewModel = new EditUserDepartmentRolesViewModel
                {
                    UserId = user.Id.ToString(),
                    UserEmail = user.Email,
                    UserDepartmentRoles = UserDepartmentRoles,
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
                var UserDepartmentRoles = await db.UserRoleDepartment
                    .Where(urd => urd.User.Equals(user) && urd.DepartmentID.ToString() == PickedDepartmentId)
                    .ToListAsync();
                var allRoles = _roleManager.Roles.ToList();
                List<Department> allDepartments = db.Department.ToList();
                EditUserDepartmentRolesViewModel = new EditUserDepartmentRolesViewModel
                {
                    UserId = user.Id.ToString(),
                    UserEmail = user.Email,
                    UserDepartmentRoles = UserDepartmentRoles,
                    AllRoles = allRoles,
                    AllDepartments = allDepartments

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
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToPage("UserList");
            }

            return NotFound();
        }
    }
}