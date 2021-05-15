using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace _20MCA121.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute //CustomExceptionFilter File extand  ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext) // objcte 
        {
            base.OnException(actionExecutedContext);
            if (actionExecutedContext.Exception is DbUpdateException)
            {

                HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponse.Content = new StringContent("Provide Prpper Data!!!"); // Database Error
                httpResponse.ReasonPhrase = "Database Error!!";
                actionExecutedContext.Response = httpResponse;

            }
        }  
    }
}