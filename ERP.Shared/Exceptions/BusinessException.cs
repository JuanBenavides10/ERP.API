using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Exceptions
{
    public class BusinessException : Exception
    {
        public int _status_code { get; }
        public BusinessException(string message, int status_code = 422) : base(message)
        {
            _status_code = status_code;
        }
    }
}
