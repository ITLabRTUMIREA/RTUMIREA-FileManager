using FileManager.Models;
using FileManager.Models.Database.DepartmentsDocuments;
using FileManager.Models.Database.DocumentStatuses;
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
        public async Task<bool> UserIsCheckerOnDepartment(Guid departmentId)
        {
            if (Guid.TryParse(_userManager.GetUserId(_signInManager.Context.User), out Guid userId))
            {

                UserDepartmentRole currentUserWithCheckerRole = await db.UserRoleDepartment
                    .FirstOrDefaultAsync(urd => urd.UserId == userId && urd.DepartmentId == departmentId && urd.Role.Name == "Checker");

                return currentUserWithCheckerRole != null;
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

        public int GetCountOfSetDocWithCertainStatus(Guid userId, string status)
        {
            _logger.LogWarning("using danger code");

            var c = db.DocumentStatusHistory
               .Include(dsh => dsh.DocumentStatus)
               .Where(dsh => dsh.UserId == userId 
               && dsh.DocumentStatus.Id == (db.DepartmentsDocument.FirstOrDefault(d=> d.Id == dsh.DepartmentsDocumentId)).LastSetDocumentStatusId
               && dsh.DocumentStatus.Status == status)
               .GroupBy(dsh => dsh.DepartmentsDocumentId)
               .Count();
            return c;
            //return db.DocumentStatusHistory
            //   .Include(dsh => dsh.DocumentStatuses)
            //   .Where(dsh => dsh.UserId == userId && dsh.DocumentStatuses.Status == status)
            //   .ToList()
            //   .Distinct(new DocumentStatusComparer())
            //   .Count();
        }
    }
}
public class DocumentStatusComparer : IEqualityComparer<DocumentStatusHistory>
{
    // DocumentStatuses are equal if their names and product numbers are equal.
    public bool Equals(DocumentStatusHistory x, DocumentStatusHistory y)
    {

        //Check whether the compared objects reference the same data.
        if (Object.ReferenceEquals(x, y)) return true;

        //Check whether any of the compared objects is null.
        if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
            return false;

        //Check whether the products' properties are equal.
        return (x.DepartmentsDocumentId == y.DepartmentsDocumentId && x.DocumentStatusId == y.DocumentStatusId) || x.DepartmentsDocumentId == y.DepartmentsDocumentId;
    }

    // If Equals() returns true for a pair of objects 
    // then GetHashCode() must return the same value for these objects.

    public int GetHashCode(DocumentStatusHistory docStatusHistory)
    {
        //Check whether the object is null
        if (Object.ReferenceEquals(docStatusHistory, null)) return 0;

        //Get hash code for the Name field if it is not null.
        int hashProductName = docStatusHistory.DepartmentsDocumentId == null ? 0 : docStatusHistory.DepartmentsDocumentId.GetHashCode();

        //Get hash code for the Code field.
        int hashProductCode = docStatusHistory.DocumentStatusId.GetHashCode();

        //Calculate the hash code for the product.
        return hashProductName ^ hashProductCode;
    }
}