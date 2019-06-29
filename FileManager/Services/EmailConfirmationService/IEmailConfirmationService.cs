using FileManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Services.EmailConfirmationService
{
    public interface IEmailConfirmationService
    {
        Task SendEmailConfirmationAsync(User user, string confirmationLink);
    }
}
