using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VideoManage.Constants;
using VideoManage.ToolKits.Helper;

namespace VideoManage.Hosting.Filters
{
    /// <summary>
    /// 自定义授权过滤器
    /// </summary>
    public class MyAuthorizeFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //验证是否登录
            //var token = context.HttpContext.Request.Headers["Authorization"];
            var token = context.HttpContext.Request.Headers["token"];
            var value = context.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(value)) 
            {
                //结果转为自定义消息格式
                context.Result = new JsonResult(new Result { code = "401", msg = "用户未登录" });
                return;
            }
            if (!token.Equals(value)) 
            {
                //结果转为自定义消息格式
                context.Result = new JsonResult(new Result { code = "402", msg = "用户认证失败" });
                return;
            }
            
        }
    }
}
