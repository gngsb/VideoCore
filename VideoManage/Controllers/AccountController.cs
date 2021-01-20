using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VideoManage.Hosting.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string passWord) 
        {

            if (userName.Equals("admin") && passWord.Equals("123456...")) 
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,userName),new Claim("password",passWord)
                };
                var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties

                {
                    //cookie 的绝对过期时间，会覆盖ExpireTimeSpan的设置。
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(2),
                    //表示 cookie 是否是持久化的以便它在不同的 request 之间传送。设置了ExpireTimeSpan或ExpiresUtc是必须的。
                    IsPersistent = false,
                    //应该允许刷新身份验证会话。
                    AllowRefresh = false

                });

                return Redirect("/Home/Index");
            }
            return Json(new { result = false, msg = "用户名密码错误!" });
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoginOut() 
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
            //var prop = new AuthenticationProperties()
            //{
            //    RedirectUri = "/Account/Login"
            //};
            //// after signout this will redirect to your provided target
            //await HttpContext.SignOutAsync("Cookies", prop);
            //return new EmptyResult();
        }
    }
}
