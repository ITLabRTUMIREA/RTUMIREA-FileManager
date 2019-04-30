using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocumentManager.Pages.Login
{
    public class LoginViewModel : PageModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public void OnGet()
        {

        }
    }
}