using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebManager.Areas.WebManager.Controllers
{
    [Area("webmanager")]
    [Authorize(Roles = "Admin, Manager")]
    public class DashboardController : Controller
    {
        [Route("quan-ly-web")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
