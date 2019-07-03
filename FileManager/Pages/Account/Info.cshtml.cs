using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileManager.Pages.Account
{
    public class InfoModel : PageModel
    {
        [TempData] public string Message { get; set; }

        public void OnGetGetInfoMessage(string message)
        {
            Message = message;
        }
    }
}