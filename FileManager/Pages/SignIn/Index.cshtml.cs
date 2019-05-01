using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FileManager.Models;
using FileManager.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FileManager.Pages.SignIn
{
    public class IndexModel : PageModel
    {
        private readonly FileManager.Models.FileManagerContext _context;

        public IndexModel(FileManager.Models.FileManagerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SignInViewModel SignInViewModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await _context.User.AnyAsync(u => u.Email == SignInViewModel.Email))
            { 
                User DbUser = await _context.User.FirstOrDefaultAsync(u => u.Email == SignInViewModel.Email);
                var hasher = new PasswordHasher<User>();
                var VerifyPassword = hasher.VerifyHashedPassword(DbUser, DbUser.Password, SignInViewModel.Password);
                if (VerifyPassword == PasswordVerificationResult.Success)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    return RedirectToPage("/SignIn/Index");
                }
            }
            else
            {
                return RedirectToPage("/SignIn/Index");
            }

        }
    }
}