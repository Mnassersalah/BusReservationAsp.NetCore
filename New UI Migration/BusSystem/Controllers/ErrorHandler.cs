using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Controllers
{
    public class ErrorHandler : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult StatusError(int statusCode)
        {
            if (statusCode == 404)
            {
                ViewBag.Message = "THE PAGE YOU ARE LOOKING FOR DOESN'T EXISTS";
            }
            else
            {
                ViewBag.Message = "SOMETHING WENT WRONG DURING YOUR REQUEST";
            }

            ViewBag.StatusCode = statusCode;

            return View();
        }



        [AllowAnonymous]
        [Route("Error")]
        public IActionResult ExceptionError()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;

            return View();
        }
    }
}
