using ERP.Accounting.Infrastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Infrastructure.Persistence.Repositories
{
    public class CompanyRepository
    {
        private readonly AccountingDbContext _db;
        public CompanyRepository(AccountingDbContext db)
        {
            _db = db;
        }
    }
}
