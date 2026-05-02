using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Identity.Application.DTOs.Responses
{
    public class LoginResponse
    {
        public string? Token {  get; set; }
        public string? UserName { get; set; }
        public string? PersonId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
