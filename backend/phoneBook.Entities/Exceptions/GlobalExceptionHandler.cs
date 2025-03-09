using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using phoneBook.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace phoneBook.Entities.Exceptions
{
    public class GlobalExceptionHandler: ExceptionFilterAttribute
    {
        // Called when aciton throw an exception
        public override void OnException(ExceptionContext context)
        {
            // here i will handle the excpetion and i want to fill the 'ApiResponse' and return it 
            // Because i dont want any errors or exceptions to done at the clinet 

            // we want to format the 'ApiResponse' Object and return it so we need [status code,...]
            var StatusCode = context.Exception is ApiException apiException
                ? apiException.StatusCode : (int)HttpStatusCode.InternalServerError;

            // i return the internal server error cause it the default status code when the exception in unhandled from API


            var apiResponse = new ApiResponse<string>(isSuccsess: false, message: context.Exception.Message, data: null, errors: context.Exception is ApiException apiException1
                ? apiException1.Errors : new List<string> { context.Exception.Message });



            // then we must format the exceptionResult into the apiResponse to be thrown

            context.Result = new ObjectResult(apiResponse)
            {
                StatusCode = StatusCode,
            };
        }
    }
}
