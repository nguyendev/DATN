using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DataAccess;
using WebManager.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebManager.Models.ManageViewModels;
using Microsoft.EntityFrameworkCore;
using Extension;

namespace WebManager.Controllers
{
    public class ManageController : Controller
    {
        private readonly UserManager<Member> _userManager;
        private readonly SignInManager<Member> _signInManager;
        private readonly string _externalCookieScheme;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public ManageController(
            UserManager<Member> userManager,
          SignInManager<Member> signInManager,
          IOptions<IdentityCookieOptions> identityCookieOptions,
          IEmailSender emailSender,
          ISmsSender smsSender,
          ApplicationDbContext context,
          ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
            _context = context;
        }

        [HttpGet]
        [Route("quan-ly/dat-mat-khau/{code}")]
        public IActionResult SetPassword(string code)
        {
            ViewData["Code"] = code;
            return View();
        }

        [HttpGet]
        [Route("quan-ly/dat-mat-khau/thanh-cong/{code}")]
        public async Task<IActionResult> SuccessPassword(string code)
        {
            string userName = (await _context.Users.SingleOrDefaultAsync(p => p.Code == code)).UserName;
            ViewData["userName"] = userName;
            return View();
        }
        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [Route("quan-ly/dat-mat-khau/{code}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model, string code)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Users.SingleOrDefaultAsync(p => p.Code  == code);
            if (user != null)
            {
                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Members");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("SuccessPassword",new {code = code });
                }
                return View(model);
            }
            return RedirectToAction("Index","Home");
        }
    }
}