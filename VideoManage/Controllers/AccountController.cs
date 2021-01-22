using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VideoManage.Constants;
using VideoManage.EFCore.Models;

namespace VideoManage.Hosting.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!model.userName.Equals("admin") || !model.passWord.Equals("123456..."))
            {
                return Ok("登录失败");
            }

            string token = GenerateToken("liu", model.userName);

            return Ok(token);

            #region cookie方式，已废除
            //if (model.userName.Equals("admin") && model.passWord.Equals("123456..."))
            //{
            //    var claims = new List<Claim>()
            //    {
            //        new Claim(ClaimTypes.Name,model.userName),new Claim("password",model.passWord)
            //    };
            //    var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties

            //    {
            //        //cookie 的绝对过期时间，会覆盖ExpireTimeSpan的设置。
            //        ExpiresUtc = DateTime.UtcNow.AddMinutes(2),
            //        //表示 cookie 是否是持久化的以便它在不同的 request 之间传送。设置了ExpireTimeSpan或ExpiresUtc是必须的。
            //        IsPersistent = false,
            //        //应该允许刷新身份验证会话。
            //        AllowRefresh = false

            //    });

            //    //return Redirect("/Home/Index");
            //    return Json(new Result { msg = "登录成功" });
            //}
            #endregion

            //return Json(new Result { code = "1", msg = "用户名或密码错误" });
        }

        private string GenerateToken(string userId, string userName)
        {
            //秘钥，这是生成Token需要的秘钥，就是理论提及到签名的那块秘钥
            string secret = "baichaqinghuanwubieshi";
            //签发者，是由谁颁发的
            string issuer = "issuer";
            //接受者，是给谁用的
            string audience = "videos";
            //指定秘钥
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            //签名凭据，指定对应的签名算法
            var sigingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //自定义payload信息，每一个claim代表一个属性键值对，就类似身份证上的姓名，出生年月一样
            var claims = new Claim[] { new Claim("userId", userId), new Claim("userName", userName) };
            //组装生成Token的数据
            SecurityToken securityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                signingCredentials: sigingCredentials,
                expires: DateTime.Now.AddMinutes(10)
            );
            //生成Token
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoginOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Json(new Result { msg = "退出成功" });
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
