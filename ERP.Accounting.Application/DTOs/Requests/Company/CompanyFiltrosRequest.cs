using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.DTOs.Requests.Company
{
    public class CompanyFiltrosRequest
    {
        public string? CompanyName { get; set; } 
        public string? Ruc { get; set; }
        public string? Code { get; set; }
    }

}
