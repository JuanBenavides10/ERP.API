using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.DTOs.Requests
{
    public class CreateAnexosRequest
    {
        public string TipoAnexo { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public string RazonSocial { get; set; } = null!;

    }
}
