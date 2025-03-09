using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phoneBook.Entities.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; } // HTTP Status Code

        public List<string> Errors { get; set; }

        public ApiException(string message, int statusCode, List<string> errors = null) : base(message)
        {
            StatusCode = statusCode;
            Errors = errors ?? new List<string>();
        }

    }
}
