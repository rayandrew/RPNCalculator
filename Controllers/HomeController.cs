using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using xtremax_web.Models;
using xtremax_web.Libs;

namespace xtremax_web.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public IActionResult About()
        {
            ViewData["Message"] = "Ray Andrew Obaja Sinurat";

            return View();
        }

        [HttpPost]
        [Route("Calculate")]
        public IActionResult Calculate([FromBody] Post post) {
            string tokens = RPNConverter.infixToRPN(post.Data.Split(" "));
            return Json(new { tokens = tokens, result = RPNEvaluator.CalculateRPN(tokens) });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
