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

namespace FileManager.Pages.Account.Departments
{
    public class EditUserDepartmentsModel : PageModel
        // TODO Make Departments managing 
    {
        private readonly FileManagerContext db;
        private readonly UserManager<User> _userManager;

        public EditUserDepartmentsModel(FileManagerContext context, UserManager<User> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        public ChangeDepartmentViewModel ChangeDepartmentViewModel = null;

/*        public async Task<IActionResult> OnGetAsync(string userid)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userid);

            if (user != null)
            {
                // получем список кафедр пользователя
                var userDepartments = db.UserDepartmentRoles.Where(urd => urd.UserId.Equals(user.Id)).ToList();
                var allDepartmemnts = db.Department.ToList();

                ChangeDepartmentViewModel = new ChangeDepartmentViewModel
                {
                    UserId = user.Id.ToString(),
                    UserEmail = user.Email,
                    UserDepartmentRoles = userDepartments,
                    AllDepartments = allDepartmemnts
                };
                return Page();
                
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync(string userId, List<string> Departments)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
               
            {
                // получем список кафедр пользователя
                List<string> userDepartmentsIds = db.UserDepartmentRoles
                    .Where(urd => urd.UserId.Equals(user.Id))
                    .Select(urd => urd.Department.Id.ToString())
                    .ToList();
                // получаем список кафедр, которые были добавлены
                IEnumerable<string> addedDepartmentsIds = Departments.Except(userDepartmentsIds);
                // получаем кафедры, которые были удалены
                var removedDepartments = userDepartmentsIds.Except(Departments);

                foreach(string newDepartmentId in addedDepartmentsIds)
                {
                    db.UserDepartment.Add(new Models.Database.UserDepartmentRoles.UserDepartmentRole()
                    {
                        UserId = Guid.Parse(userId),
                        DepartmentId = Guid.Parse(newDepartmentId),
                        Department = db.Department.FirstOrDefault(d => d.Id.ToString() == newDepartmentId)
                    }) ;
                }

                foreach (string oldDepartmentId in removedDepartments)
                {
                    db.UserRole.Remove(db.UserRole.FirstOrDefault(
                        urd => urd.DepartmentId.ToString() == oldDepartmentId
                        && urd.UserId.ToString() == userId));

                }
                await db.SaveChangesAsync();

                return RedirectToPage("UserDepartmentList");
            }

            return NotFound();
        }*/
    }
}