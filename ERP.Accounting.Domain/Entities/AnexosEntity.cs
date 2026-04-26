using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Domain.Entities
{
    public class AnexosEntity
    {
        public int Id { get; set; }
        public Guid UuId { get; set; }
        public string TipoAnexo { get; set; }
        public string Codigo { get; set; }
        public string RazonSocial { get; set; }

    }
}
