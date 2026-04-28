using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Identity.Application.DTOs.Responses
{
    public class LoginResponse
    {
        public string token {  get; set; }
        public string user_name { get; set; }
        public string person_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
}
