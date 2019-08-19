using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FileManager.Pages.Account
{
    public class InfoModel : PageModel
    {
        [TempData] public string Message { get; set; }

        private readonly ILogger<InfoModel> _logger;

        public InfoModel(ILogger<InfoModel> logger)
        {
            _logger = logger;
        }


        public IActionResult OnGetGetInfoMessage(string message)
        {
            try
            {
                Message = message;
                return Page();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error while getting page In");
                return NotFound();
            }
        }
    }
}