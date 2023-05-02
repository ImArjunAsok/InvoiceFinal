using InvoiceTrack.Core.Application.Interfaces;
using InvoiceTrack.Core.Domain.Dto;
using InvoiceTrack.Core.Domain.Models;
using InvoiceTrack.Core.Domain.Type;
using InvoiceTrack.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Security.Claims;

namespace InvoiceTrack.WebAPI.Areas.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountsController(ILoginService loginService,
                                  SignInManager<ApplicationUser> signInManager)
        {
            _loginService = loginService;
            _signInManager = signInManager;
        }

        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin(LoginDto dto)
        {
            var res = await _loginService.AdminLoginAsync(dto);
            return Ok(res);
        }

        [HttpPost("ExternalLogin")]
        public async Task<IActionResult> ExternalAuthentication(ExternalAuthDto dto)
        {
            var res = await _loginService.ExternalAuthenticationAsync(dto.Token, dto.Provider);
            return Ok(res);
        }

        //[HttpGet("ExternalLogin")]
        //public IActionResult ExternalLogin()
        //{
        //    //var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account");
        //    var redirectUrl = "/api/Accounts/MicrosoftLogin";
        //    var properties = _signInManager.ConfigureExternalAuthenticationProperties(MicrosoftAccountDefaults.AuthenticationScheme, redirectUrl);
        //    return Challenge(properties, MicrosoftAccountDefaults.AuthenticationScheme);
        //}

        //[HttpGet("MicrosoftLogin")]
        //public async Task<IActionResult> ExternalLoginCallback()
        //{
        //    var res = new ServiceResponse<string>();
        //    var info = await _signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        res.AddError("", "No account found");
        //        return Ok(res);
        //    }
        //    var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        //    //if (signInResult.Succeeded)
        //    //{
        //    //    Console.WriteLine(signInResult);
        //    //    res.Result = "true";
        //    //    return Ok;
        //    //}
        //    //else
        //    //{
        //    //    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //    //    res.Result = email;
        //    //    return res;
        //    //}
        //    return Ok();
        //}
    }
}
