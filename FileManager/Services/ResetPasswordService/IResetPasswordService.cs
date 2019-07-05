using FileManager.Models;
using FileManager.Models.Database.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services.ResetPasswordService
{
    public interface IResetPasswordService
    {
        Task SendResetPasswordConfirmationLinkToEmailAsync(User user, string resetPasswordCallbackLink);
    }
}
