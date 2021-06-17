using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MISA.Core.Entities.Base;
using MISA.Core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Exceptions
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is GuardException exception)
                {
                    var res = new ActionServiceResult
                    {
                        StatusCode = 400,
                        UserMsg = "Dữ liệu không hợp lệ",
                        DevMsg = "Dữ liệu không hợp lệ",
                        Data = exception.Data
                    };
                    context.Result = new ObjectResult(res) {
                        StatusCode = 400,
                    };
                    context.ExceptionHandled = true;
                }
                else
                {
                    var res = new
                    {
                        devMsg = context.Exception.Message,
                        userMsg = Resources.error_system,
                        Data = context.Exception.Data,
                        traceInfor = context.Exception.StackTrace
                    };
                    context.Result = new ObjectResult(res)
                    {
                        StatusCode = 500
                    };
                    context.ExceptionHandled = true;
                }
            }
        }
    }
}
