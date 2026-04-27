using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.DTOs.Responses.Configuration
{
    public class GetCompanyResponse
     {
        public Guid uuid { get; set; }
        public string code { get; set; }
        public string company_name { get; set; }
        public string address { get; set; }
        public string ruc { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public byte[]? logo { get; set; }
    }
}
