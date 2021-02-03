using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoManage.Hosting.Controllers
{
    /// <summary>
    /// 缴费管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[MyAuthorizeFilter]
    public class CostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
