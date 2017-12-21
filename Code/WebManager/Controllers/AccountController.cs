//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity;
//using DataAccess;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using WebManager.Services;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Microsoft.AspNetCore.Authorization;
//using WebManager.Models.AccountViewModels;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace WebManager.Controllers
//{
//    public class AccountController : Controller
//    {
//        private const int CODE_LENGTH = 8;
//        private readonly UserManager<Member> _userManager;
//        private RoleManager<IdentityRole> _roleManager;
//        private readonly ApplicationDbContext _context;
//        private readonly SignInManager<Member> _signInManager;
//        private readonly IEmailSender _emailSender;
//        private readonly ISmsSender _smsSender;
//        private readonly ILogger _logger;
//        private readonly string _externalCookieScheme;

//        public AccountController(
//            RoleManager<IdentityRole> roleMgr,
//            ApplicationDbContext context,
//            UserManager<Member> userManager,
//            SignInManager<Member> signInManager,
//            IOptions<IdentityCookieOptions> identityCookieOptions,
//            IEmailSender emailSender,
//            ISmsSender smsSender,
//            ILoggerFactory loggerFactory)
//        {
//            _roleManager = roleMgr;
//            _context = context;
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
//            _emailSender = emailSender;
//            _smsSender = smsSender;
//            _logger = loggerFactory.CreateLogger<AccountController>();
//        }

//        //
//        // GET: /Account/Login
//        [HttpGet]
//        [AllowAnonymous]
//        [Route("/dang-nhap")]
//        public async Task<IActionResult> Login(string returnUrl = null)
//        {
//            // Clear the existing external cookie to ensure a clean login process
//            await HttpContext.Authentication.SignOutAsync(_externalCookieScheme);

//            ViewData["ReturnUrl"] = returnUrl;
//            return View();
//        }

//        //
//        // POST: /Account/Login
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        [Route("/dang-nhap")]
//        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
//        {
//            ViewData["ReturnUrl"] = returnUrl;
//            if (ModelState.IsValid)
//            {
//                // This doesn't count login failures towards account lockout
//                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
//                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
//                if (result.Succeeded)
//                {
//                    _logger.LogInformation(1, "User logged in.");
//                    string userID = (await GetCurrentUserAsync()).Id;
//                    if (User.IsInRole(RoleName.ROLE_ADMIN) || User.IsInRole(RoleName.ROLE_MANAGER) || User.IsInRole(RoleName.ROLE_MEMBER))
//                        return RedirectToLocal(returnUrl);
//                    else
//                        return null;
//                }
//                if (result.RequiresTwoFactor)
//                {
//                    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, ReMemberMe = model.RememberMe });
//                }
//                if (result.IsLockedOut)
//                {
//                    _logger.LogWarning(2, "User account locked out.");
//                    return View("Lockout");
//                }
//                else
//                {
//                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//                    return View(model);
//                }
//            }

//            // If we got this far, something failed, redisplay form
//            return View(model);
//        }

//        //
//        // GET: /Account/Register
        
//        //
//        // POST: /Account/Logout
//        [HttpPost]
//        [Route("/quan-ly-web/dang-xuat")]
//        public async Task<IActionResult> Logout()
//        {
//            await _signInManager.SignOutAsync();
//            _logger.LogInformation(4, "User logged out.");
//            return RedirectToAction("Index", "Dashboard");
//        }
        
//        //
//        // GET: /Account/ForgotPassword
//        [HttpGet]
//        [AllowAnonymous]
//        public IActionResult ForgotPassword()
//        {
//            return View();
//        }

//        //
//        // POST: /Account/ForgotPassword
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await _userManager.FindByEmailAsync(model.Email);
//                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
//                {
//                    // Don't reveal that the user does not exist or is not confirmed
//                    return View("ForgotPasswordConfirmation");
//                }

//                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
//                // Send an email with this link
//                //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
//                //var callbackUrl = Url.Action(nameof(ResetPassword), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
//                //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
//                //   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
//                //return View("ForgotPasswordConfirmation");
//            }

//            // If we got this far, something failed, redisplay form
//            return View(model);
//        }

//        //
//        // GET: /Account/ForgotPasswordConfirmation
//        [HttpGet]
//        [AllowAnonymous]
//        public IActionResult ForgotPasswordConfirmation()
//        {
//            return View();
//        }

//        //
//        // GET: /Account/ResetPassword
//        [HttpGet]
//        [AllowAnonymous]
//        public IActionResult ResetPassword(string code = null)
//        {
//            return code == null ? View("Error") : View();
//        }

//        //
//        // POST: /Account/ResetPassword
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }
//            var user = await _userManager.FindByEmailAsync(model.Email);
//            if (user == null)
//            {
//                // Don't reveal that the user does not exist
//                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
//            }
//            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
//            if (result.Succeeded)
//            {
//                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
//            }
//            AddErrors(result);
//            return View();
//        }

//        //
//        // GET: /Account/ResetPasswordConfirmation
//        [HttpGet]
//        [AllowAnonymous]
//        public IActionResult ResetPasswordConfirmation()
//        {
//            return View();
//        }

//        //
//        // GET: /Account/SendCode
//        [HttpGet]
//        [AllowAnonymous]
//        public async Task<ActionResult> SendCode(string returnUrl = null, bool reMemberMe = false)
//        {
//            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
//            if (user == null)
//            {
//                return View("Error");
//            }
//            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
//            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
//            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl });
//        }

//        //
//        // POST: /Account/SendCode
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> SendCode(SendCodeViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View();
//            }

//            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
//            if (user == null)
//            {
//                return View("Error");
//            }

//            // Generate the token and send it
//            var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
//            if (string.IsNullOrWhiteSpace(code))
//            {
//                return View("Error");
//            }

//            var message = "Your security code is: " + code;
//            if (model.SelectedProvider == "Email")
//            {
//                await _emailSender.SendEmailAsync(await _userManager.GetEmailAsync(user), "Security Code", message);
//            }
//            else if (model.SelectedProvider == "Phone")
//            {
//                await _smsSender.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), message);
//            }

//            return RedirectToAction(nameof(VerifyCode), new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl });
//        }

//        //
//        // GET: /Account/VerifyCode
//        [HttpGet]
//        [AllowAnonymous]
//        public async Task<IActionResult> VerifyCode(string provider, bool reMemberMe, string returnUrl = null)
//        {
//            // Require that the user has already logged in via username/password or external login
//            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
//            if (user == null)
//            {
//                return View("Error");
//            }
//            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl });
//        }

//        //
//        // POST: /Account/VerifyCode
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }

//            // The following code protects for brute force attacks against the two factor codes.
//            // If a user enters incorrect codes for a specified amount of time then the user account
//            // will be locked out for a specified amount of time.
//            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, false, false);
//            if (result.Succeeded)
//            {
//                return RedirectToLocal(model.ReturnUrl);
//            }
//            if (result.IsLockedOut)
//            {
//                _logger.LogWarning(7, "User account locked out.");
//                return View("Lockout");
//            }
//            else
//            {
//                ModelState.AddModelError(string.Empty, "Invalid code.");
//                return View(model);
//            }
//        }

//        //
//        // GET /Account/AccessDenied
//        [HttpGet]
//        public IActionResult AccessDenied()
//        {
//            return View();
//        }
//        [HttpGet]
//        [Route("dang-ky")]
//        public IActionResult Register()
//        {
//            if (!_context.Users.Any())
//                return RedirectToAction("RegisterAdmin");
//            return RedirectToAction("RegisterSimple");
//        }
//        [HttpGet]
//        [Route("dang-ky/buoc-1")]
//        public IActionResult RegisterSimple()
//        {
//            return View();
//        }
//        [HttpPost]
//        [Route("quan-ly-web/dang-ky/buoc-1")]
//        public async Task<IActionResult> RegisterSimple(RegisterSimpleViewModel model, string returnUrl = null)
//        {
//            ViewData["ReturnUrl"] = returnUrl;
//            if (ModelState.IsValid)
//            {
//                string code = StringExtensions.RandomString(CODE_LENGTH);
//                while (_context.Users.Any(p => p.Code == code))
//                {
//                    code = StringExtensions.RandomString(CODE_LENGTH);
//                }
//                var user = new Member { UserName = model.UserName, Code = code };
//                var result = await _userManager.CreateAsync(user);

//                if (result.Succeeded)
//                {
//                    return RedirectToAction("CompleteRegisterSimple", new CompleteRegisterSimple { ID = model.UserName, Code = code });
//                }
//                AddErrors(result);
//            }

//            // If we got this far, something failed, redisplay form
//            return View(model);
//        }
//        [Route("quan-ly-web/dang-ky/hoan-tat")]
//        public IActionResult CompleteRegisterSimple(CompleteRegisterSimple model)
//        {
//            return View(model);
//        }
//        #region Helpers
//        private Task<Member> GetCurrentUserAsync()
//        {
//            return _userManager.GetUserAsync(HttpContext.User);
//        }

//        private void AddErrors(IdentityResult result)
//        {
//            foreach (var error in result.Errors)
//            {
//                ModelState.AddModelError(string.Empty, error.Description);
//            }
//        }

//        private IActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//            {
//                return Redirect(returnUrl);
//            }
//            else
//            {
//                return RedirectToAction("Index", "Dashboard");
//            }
//        }

//        #endregion
//    }
//}
