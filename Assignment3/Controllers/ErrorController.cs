using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Text;

namespace Assignment3.Controllers
{
    /// <summary>
    /// Exception handler for logging and formatting error messages
    /// </summary>
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly Assignment3Context _context;

        public ErrorController(Assignment3Context context)
        {
            _context = context;
        }

        [Route("error")]
        async public Task<ActionResult<ErrorResponse>> Error()
        {
            IExceptionHandlerFeature errorContext = HttpContext.Features.Get<IExceptionHandlerFeature>();
            Exception exception = errorContext.Error;

            ErrorResponse error;

            if (exception is StatusException statusException)
            {
                error = new ErrorResponse()
                {
                    Id = Guid.NewGuid(),
                    StatusCode = statusException.StatusCode,
                    Message = statusException.Message,
                };
            } else {
                error = new ErrorResponse()
                {
                    Id = Guid.NewGuid(),
                    StatusCode = 500,
                    Message = "An internal server error occured.",
                };
            }

            Response.StatusCode = error.StatusCode;
            ValueTask responseBodyTask = Response.Body.WriteAsync(Encoding.ASCII.GetBytes(error.Message));

            _context.ErrorResponse.Add(error);
            Task<int> saveDatabaseTask = _context.SaveChangesAsync();

            await responseBodyTask;
            await saveDatabaseTask;

            return error;
        }
    }
}
