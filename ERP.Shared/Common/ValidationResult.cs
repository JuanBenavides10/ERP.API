using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Shared.Common
{
    public class ValidationResult
    {
        public bool IsValid { get; init; }
        public string Message { get; init; }
        public static ValidationResult Failure(string message) => new() { IsValid = false, Message = message };
        public static ValidationResult Success(string message) => new() { IsValid = true, Message = message };
    }

    public class ValidationResult<T> : ValidationResult
    {
        public T? Data { get; init; }
        public static ValidationResult<T> Failure(string message) => new() { IsValid = false, Message = message, Data = default };
        public static ValidationResult<T> Success(T data, string message) => new() { IsValid = true, Message = message, Data = data };
    }

}
