using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phoneBook.Entities.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public ApiResponse(bool isSuccsess, string message, T data = default, List<string> errors = null)
        {
            IsSuccess = isSuccsess;
            Message = message;
            Data = data;
            Errors = errors ?? new List<string>();
        }

        public static ApiResponse<T> Success(T data, string message)
        {
            return new ApiResponse<T>(isSuccsess: true, message: message, data: data);
        }

        public static ApiResponse<T> NotFound(string message)
        {
            return new ApiResponse<T>(isSuccsess: true, message: message);
        }

        // Internal Server Error Response
        public static ApiResponse<T> Error(string message, List<string> errors = null)
        {
            return new ApiResponse<T>(false, message, default, errors);
        }

        public static ApiResponse<T> ValidationError(ModelStateDictionary modelState, string message)
        {
            var errors = modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return new ApiResponse<T>(isSuccsess: false, message: message, data: default, errors: errors);
        }

        public static ApiResponse<T> Conflict(T data, string message, bool isSuccsess, List<string> errors = null)
        {
            return new ApiResponse<T>(isSuccsess, message, data, errors);
        }


    }
}
