using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Application.Contracts
{

    public class ValidationResponse
    {
        public bool is_valid { get; init; }
        public string message { get; init; }
        public static ValidationResponse Failure(string message) => new() { is_valid = true, message = message };
        public static ValidationResponse Success(string message) => new() { is_valid = false, message = message };
    }

    public class ValidationResponse<T> : ValidationResponse
    {
        public T? data { get; init; }
        public static ValidationResponse<T> Failure(string message) => new() { is_valid = true, message = message, data = default };
        public static ValidationResponse<T> Success(T data, string message) => new() { is_valid = false, message = message, data = data };
    }

}
