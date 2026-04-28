using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Identity.Domain.Entities
{
    public class Person
    {
        public int id { get; set; }
        public Guid uuid { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? document_type { get; set; }
        public string? email { get; set; }
        public DateTime? birth_date { get; set; }
        public bool active { get; set; }
        public DateTime? created_at { get; set; }
        public ICollection<Users> users { get; set; } = new List<Users>();
    }
}
