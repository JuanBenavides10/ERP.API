using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.DTOs.Requests.Configuration
{
    public class UpdateCompanyRequest
    {
        public string Code { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Ruc { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public byte[]? Logo { get; set; }
    }
}
