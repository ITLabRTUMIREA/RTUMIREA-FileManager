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
using FileManager.Models.Database.UserRole;
using FileManager.Models.Database.UserDepartments;

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

        public async Task<IActionResult> OnGetAsync(string userid)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userid);

            if (user != null)
            {
                // получем список кафедр пользователя
<<<<<<< Updated upstream
                var userDepartments = db.UserDepartment.Where(urd => urd.UserId.Equals(user.Id)).ToList();
                var allDepartmemnts = db.Department.ToList();
=======
                var userDepartments = db.UserRole.Where(urd => urd.UserId.Equals(user.Id)).ToList();
                var allDepartmemnts = db.Department.ToList<Department>();
>>>>>>> Stashed changes

                ChangeDepartmentViewModel = new ChangeDepartmentViewModel
                {
                    UserId = user.Id.ToString(),
                    UserEmail = user.Email,
                    UserDepartments = userDepartments,
                    AllDepartments = allDepartmemnts
                };
                return Page();
                
            }

            return NotFound();
        }
        [HttpPost]
<<<<<<< Updated upstream
        public async Task<IActionResult> OnPostAsync(string userId, List<string> departments)
=======
        public async Task<IActionResult> OnPostAsync(string userId, List<string> Departments)
>>>>>>> Stashed changes
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
               
            {
                // получем список кафедр пользователя
                List<string> userDepartmentsIds = db.UserDepartment
                    .Where(urd => urd.UserId.Equals(user.Id))
                    .Select(urd => urd.Department.ID.ToString())
                    .ToList();
                // получаем список кафедр, которые были добавлены
<<<<<<< Updated upstream
                IEnumerable<string> addedDepartmentsIds = departments.Except(userDepartmentsIds);
                // получаем кафедры, которые были удалены
                var removedDepartments = userDepartmentsIds.Except(departments);
=======
                IEnumerable<string> addedDepartmentsIds = Departments.Except(userDepartmentsIds);
                // получаем кафедры, которые были удалены
                var removedDepartments = userDepartmentsIds.Except(Departments);
>>>>>>> Stashed changes

                foreach(string newDepartmentId in addedDepartmentsIds)
                {
                    db.UserDepartment.Add(new UserDepartment()
                    {
                        UserId = Guid.Parse(userId),
<<<<<<< Updated upstream
                        DepartmentId = Guid.Parse(newDepartmentId),
=======
                        DepartmentID = Guid.Parse(newDepartmentId),
>>>>>>> Stashed changes
                        Department = db.Department.FirstOrDefault(d => d.ID.ToString() == newDepartmentId)
                    }) ;
                }

                foreach (string oldDepartmentId in removedDepartments)
                {
<<<<<<< Updated upstream
                    db.UserDepartment.Remove(db.UserDepartment.FirstOrDefault(
                        urd => urd.DepartmentId.ToString() == oldDepartmentId
=======
                    db.UserRole.Remove(db.UserRole.FirstOrDefault(
                        urd => urd.DepartmentID.ToString() == oldDepartmentId
>>>>>>> Stashed changes
                        && urd.UserId.ToString() == userId));

                }
                await db.SaveChangesAsync();

                return RedirectToPage("UserDepartmentList");
            }

            return NotFound();
        }
    }
}