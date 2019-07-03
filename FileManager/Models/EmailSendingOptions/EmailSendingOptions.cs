using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace FileManager.Models.EmailSendingOptions
{
    public class EmailSendingOptions
    { 
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
