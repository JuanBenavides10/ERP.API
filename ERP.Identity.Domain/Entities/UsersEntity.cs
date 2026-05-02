using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Identity.Domain.Entities
{
    public class UsersEntity
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public int PersonId { get; set; }
        public string? UserName { get; set; }
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public int Attempts { get; set; }
        public DateTime? LockedUntil { get; set; }
        public bool Active { get; set; } //bool - > bit
        public DateTime? CreatedAt { get; set; }
        public PersonEntity? Person { get; set; }
    }
}
