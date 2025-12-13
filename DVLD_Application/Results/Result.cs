using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; } 
        public string Message { get; set; }
        public string Code { get; set; } 
        public T Data { get; set; }

        public static Result<T> Success(T data, string message = "Operation Successful")
        {
            return new Result<T> { IsSuccess = true, Data = data, Message = message, Code = "OK" };
        }

        public static Result<T> Failure(string message, string code = "ERROR")
        {
            return new Result<T> { IsSuccess = false, Message = message, Code = code };
        }

    }
}
