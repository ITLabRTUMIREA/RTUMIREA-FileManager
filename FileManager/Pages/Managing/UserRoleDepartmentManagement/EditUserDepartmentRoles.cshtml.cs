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
using FileManager.Services.GetAccountDataService;
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Managing.UserRoleDepartmentManagement
{
    public class EditUserDepartmentRolesModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly FileManagerContext db;
        private readonly IGetAccountDataService _getAccountDataService;
        private readonly ILogger<EditUserDepartmentRolesModel> _logger;

        public EditUserDepartmentRolesViewModel EditUserDepartmentRolesViewModel = null;
        public string PickedDepartmentId = "";
        public bool IsSystemAdmin = false;

        public List<IGrouping<string, UserDepartmentRole>> allUserDepartmentRoles;

        public EditUserDepartmentRolesModel(UserManager<User> userManager,
            FileManagerContext context,
            IGetAccountDataService getAccountDataService,
            ILogger<EditUserDepartmentRolesModel> logger)
        {
            _userManager = userManager;
            db = context;
            _getAccountDataService = getAccountDataService;
            _logger = logger;
        }


        public async Task<IActionResult> OnGetAsync(string userid)
        {
            try
            {
                IsSystemAdmin = _getAccountDataService.IsSystemAdmin();

                // получаем пользователя
                User user = await _userManager.FindByIdAsync(userid);
                if (user != null && (await _getAccountDataService.IsAdminOnAnyDepartment() || IsSystemAdmin))
                {
                    List<Role> allRoles = await db.Role.ToListAsync();
                    allUserDepartmentRoles = await db.UserRoleDepartment
                            .Where(urd => urd.UserId.Equals(user.Id))
                            .Include(udr => udr.Role)
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
                _logger.LogError(e, "Error while getting page EditUserDepartmentRoles");
                return NotFound();
            }
        }

        public async Task<IActionResult> OnGetGetRolesAsync(string userid, string departmentid)
        {
            try
            {
                IsSystemAdmin = _getAccountDataService.IsSystemAdmin();

                PickedDepartmentId = departmentid;
                if (await _getAccountDataService.IsAdminOnAnyDepartment() || IsSystemAdmin)
                {
                    // получаем пользователя
                    User user = await _userManager.FindByIdAsync(userid);
                    if (user != null)
                    {
                        // получем список ролей пользователя
                        List<Role> allRoles = await db.Role.ToListAsync();
                        allUserDepartmentRoles = await db.UserRoleDepartment
                             .Where(urd => urd.UserId.Equals(user.Id))
                             .Include(udr => udr.Role)
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
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting Department Data for this User");
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUpdateRolesAsync(string userId, string departmentId, List<string> roles)
        {
            try
            {
                if (await _getAccountDataService.IsAdminOnAnyDepartment() || _getAccountDataService.IsSystemAdmin())
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

                            var allRoles = db.Role
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

                            return RedirectToPage("UserDepartmentList");
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return Page();
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while saving Department Data for this User");
                return NotFound();
            }
        }
    }
}