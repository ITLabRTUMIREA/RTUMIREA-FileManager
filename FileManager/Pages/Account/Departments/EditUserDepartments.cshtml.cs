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

namespace FileManager.Pages.Account.Departments
{
    public class EditUserDepartmentsModel : PageModel
        // TODO Make departaments managing 
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
                var userDepartments = db.UserRole.Where(urd => urd.UserId.Equals(user.Id)).ToList();
                var allDepartmemnts = db.Departament.ToList<Departament>();

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
        public async Task<IActionResult> OnPostAsync(string userId, List<string> departaments)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
               
            {
                // получем список кафедр пользователя
                List<string> userDepartmentsIds = db.UserRole
                    .Where(urd => urd.UserId.Equals(user.Id))
                    .Select(urd => urd.Departament.ID.ToString())
                    .ToList();
                // получаем список кафедр, которые были добавлены
                IEnumerable<string> addedDepartmentsIds = departaments.Except(userDepartmentsIds);
                // получаем кафедры, которые были удалены
                var removedDepartments = userDepartmentsIds.Except(departaments);

                foreach(string newDepartmentId in addedDepartmentsIds)
                {
                    db.UserRole.Add(new UserRole()
                    {
                        UserId = Guid.Parse(userId),
                        DepartamentID = Guid.Parse(newDepartmentId),
                        Departament = db.Departament.FirstOrDefault(d => d.ID.ToString() == newDepartmentId)
                    }) ;
                }

                foreach (string oldDepartmentId in removedDepartments)
                {
                    db.UserRole.Remove(db.UserRole.FirstOrDefault(
                        urd => urd.DepartamentID.ToString() == oldDepartmentId
                        && urd.UserId.ToString() == userId));

                }
                await db.SaveChangesAsync();

                return RedirectToPage("UserDepartmentList");
            }

            return NotFound();
        }
    }
}