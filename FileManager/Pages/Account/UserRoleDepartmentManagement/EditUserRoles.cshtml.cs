using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.Models.Database.UserSystemRoles;
using FileManager.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Account.UserRoleDepartmentManagement
{
    public class EditUserRolesModel : PageModel
    {
        private readonly RoleManager<SystemRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<EditUserRolesModel> _logger;

        public EditUserRolesViewModel EditUserRolesViewModel;
        public bool IsSystemAdmin = false;

        public EditUserRolesModel(RoleManager<SystemRole> roleManager,
            UserManager<User> userManager,
            ILogger<EditUserRolesModel> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(string userid)
        {
            try
            {

                // получаем пользователя
                User user = await _userManager.FindByIdAsync(userid);
                IsSystemAdmin = await _userManager.IsInRoleAsync(user, "SystemAdmin");

                if (user != null && IsSystemAdmin)
                {
                    // получем список ролей пользователя
                    var userSystemRoles = await _userManager.GetRolesAsync(user);
                    var allSystemRoles = _roleManager.Roles.ToList();
                    EditUserRolesViewModel = new EditUserRolesViewModel
                    {
                        UserId = user.Id.ToString(),
                        UserEmail = user.Email,
                        UserSystemRoles = userSystemRoles,
                        AllSystemRoles = allSystemRoles
                    };
                    return Page();
                }
                return NotFound();
            }catch(Exception e)
            {
                _logger.LogError(e, "Error while loading EditUserRoles page");
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> OnPostUpdateUserRolesAsync(string userid, List<string> roles)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userid);
            IsSystemAdmin = await _userManager.IsInRoleAsync(user, "SystemAdmin");

            if (user != null && IsSystemAdmin)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);

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