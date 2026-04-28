using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Identity.Application.DTOs.Requests
{
    public class LoginRequest
    {
        public string? user_name { get; set; }
        public string? password { get; set; }
    }
}
