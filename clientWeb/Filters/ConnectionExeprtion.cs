﻿using Microsoft.AspNetCore.Mvc.Filters;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace ClientWeb.Filters
{
    public class ConnectionExeprtion: ExceptionFilterAttribute,  IExceptionFilter
    {
 
        public void OnException(ExceptionContext context)
    {
            string actionName = context.ActionDescriptor.DisplayName;
            string exceptionStack = context.Exception.StackTrace;
            string exceptionMessage = context.Exception.Message;
            context.Result = new ContentResult
            {
                Content = $"В методе {actionName} возникло исключение: \n {exceptionMessage} \n {exceptionStack}"
            };
            context.ExceptionHandled = true;
            /*if (!exceptionContext.ExceptionHandled && exce    ptionContext.Exception is IndexOutOfRangeException)
            {
                exceptionContext.Result = new RedirectResult("/Content/ExceptionFound.html");
                exceptionContext.ExceptionHandled = true;
            }*/
        }
    }
}
