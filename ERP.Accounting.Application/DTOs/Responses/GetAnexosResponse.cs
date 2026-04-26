using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.DTOs.Responses
{
    public class GetAnexosResponse
    {
        public Guid UuId { get; set; }
        public string TipoAnexo { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public string RazonSocial { get; set; } = null!;

    }
}
