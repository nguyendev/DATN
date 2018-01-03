using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebManager.Models.ToeicExamViewModels;
using Extension;

namespace WebManager.Controllers
{
    public class ToeicExamController : Controller
    {
        [Route("de-thi/doi")]
        [HttpGet]
        public IActionResult Watting()
        {
            DateTime dateTime = DateTime.Now;
            DateTime timeTo = dateTime.AddSeconds(30);
            WattingViewModel watingViewModel = new WattingViewModel();
            watingViewModel.WaitingTime = timeTo;
            watingViewModel.ShowTime = DateTimeExtension.WatingDay(watingViewModel.WaitingTime);
            return View(watingViewModel);
        }
        [Route("de-thi/doi-1")]
        [HttpGet]
        public IActionResult ChangeWatting(WattingViewModel watingViewModel)
        {
            watingViewModel.WaitingTime.AddSeconds(-1);
            watingViewModel.ShowTime = DateTimeExtension.WatingDay(watingViewModel.WaitingTime);
            var ts = new TimeSpan(DateTime.Now.Ticks - watingViewModel.WaitingTime.Ticks);
            if (ts.Seconds > 0)
                return RedirectToAction("DeThi");
            return View("Watting", watingViewModel);
        }
        public IActionResult DeThi()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}