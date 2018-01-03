using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using WebManager.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;

namespace WebManager.Controllers
{
    [Route("api/AccountAPI")]
    public class AccountAPIController : Controller
    {
        private readonly UserManager<Member> _userManager;
        private readonly SignInManager<Member> _signInManager;

        public AccountAPIController(
            UserManager<Member> userManager,
            SignInManager<Member> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName,model.Password, model.RememberMe, lockoutOnFailure: false);
            bool loginSuccess = result.Succeeded == true ? loginSuccess = true : loginSuccess = false;

            return loginSuccess;
        }

        //[HttpPost()]
        //public async Task<bool> UpdateContactAsync(string code)
        //{
        //    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
        //    bool loginSuccess = result.Succeeded == true ? loginSuccess = true : loginSuccess = false;

        //    return loginSuccess;
        //}
    }
}