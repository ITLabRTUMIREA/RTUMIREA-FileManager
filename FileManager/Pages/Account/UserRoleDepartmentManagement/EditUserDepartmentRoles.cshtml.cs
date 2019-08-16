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
        public User currentUser;
        public List<IGrouping<string, UserDepartmentRole>> allUserDepartmentRoles;

        public async Task<IActionResult> OnGetAsync(string userid)
        {
            try
            {
                // TODO Add check for User rights to edit roles certain department
                currentUser = await _userManager.GetUserAsync(HttpContext.User);

                // получаем пользователя
                User user = await _userManager.FindByIdAsync(userid);
                if (user != null)
                {
                    var allRoles = _roleManager.Roles.ToList();
                    allUserDepartmentRoles = await db.UserRoleDepartment
                            .Where(urd => urd.UserId.Equals(user.Id))
                            .GroupBy(urd => urd.Department.Name)
                            .ToListAsync();

                    List<Department> allDepartments = db.Department.ToList();
                    EditUserDepartmentRolesViewModel = new EditUserDepartmentRolesViewModel
                    {
                        UserId = user.Id.ToString(),
                        UserEmail = user.Email,
                        UserDepartmentRoles = new List<UserDepartmentRole>() { },
                        AllRoles = allRoles,
                        AllDepartments = allDepartments

                    };
                    return Page();

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Page();
        }

        public async Task<IActionResult> OnGetGetRolesAsync(string userid, string departmentid)
        {
            try
            {
                currentUser = await _userManager.GetUserAsync(HttpContext.User);

                PickedDepartmentId = departmentid;
                // получаем пользователя
                User user = await _userManager.FindByIdAsync(userid);
                if (user != null)
                {
                    // получем список ролей пользователя
                    var allRoles = _roleManager.Roles.ToList();
                    allUserDepartmentRoles = await db.UserRoleDepartment
                         .Where(urd => urd.UserId.Equals(user.Id))
                         .GroupBy(urd => urd.Department.Name)
                         .ToListAsync();

                    List<Department> allDepartments = db.Department.ToList();

                    EditUserDepartmentRolesViewModel = new EditUserDepartmentRolesViewModel
                    {
                        UserId = user.Id.ToString(),
                        UserEmail = user.Email,
                        UserDepartmentRoles = await db.UserRoleDepartment
                            .Where(urd => urd.UserId.Equals(user.Id) && urd.DepartmentId.ToString() == departmentid)
                            .ToListAsync(),
                        AllRoles = allRoles,
                        AllDepartments = allDepartments,
                        DepartmentId = departmentid

                    };
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUpdateRolesAsync(string userId, string departmentId, List<string> roles)
        {
            try
            {
                if (ModelState.IsValid)
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

                        var allRoles = _roleManager.Roles
                            .Select(r => r.Id.ToString().ToLower())
                            .ToList();

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
                                UserDepartmentRole removingRecord = await db.UserRoleDepartment
                                    .FirstOrDefaultAsync(urd => urd.RoleId.Equals(Guid.Parse(role))
                                        && urd.UserId.Equals(Guid.Parse(userId))
                                        && urd.DepartmentId.Equals(Guid.Parse(departmentId)));

                                db.UserRoleDepartment.Remove(removingRecord);
                            }
                        }

                        await db.SaveChangesAsync();

                        return RedirectToPage("UserList");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Page();
        }
    }
}