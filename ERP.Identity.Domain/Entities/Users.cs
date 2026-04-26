using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Identity.Domain.Entities
{
    public class Users
    {
        public int id { get; set; }
        public int person_id { get; set; }
        public string? user_name { get; set; }
        public byte[] password_hash { get; set; } = Array.Empty<byte>();
        public byte[] password_salt { get; set; } = Array.Empty<byte>();
        public int attempts { get; set; }
        public DateTime? locked_until { get; set; }
        public bool active { get; set; }
        public DateTime? created_at { get; set; }
        public Person? person { get; set; }
    }
}
