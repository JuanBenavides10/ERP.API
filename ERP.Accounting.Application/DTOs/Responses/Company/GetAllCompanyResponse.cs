using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.DTOs.Responses.Company
{
    public class GetAllCompanyResponse
    {
        public Guid Uuid { get; set; }
        public string Code { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Ruc { get; set; }
       
    }
}
