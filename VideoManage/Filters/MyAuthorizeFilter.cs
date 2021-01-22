using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var token = context.HttpContext.Request.Headers["Authorization"];
            var sd = "12";
            //throw new NotImplementedException();
        }
    }
}
