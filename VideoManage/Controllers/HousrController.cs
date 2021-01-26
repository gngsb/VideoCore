using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoManage.EFCore;
using VideoManage.Hosting.Filters;
using VideoManage.Service.Property;

namespace VideoManage.Hosting.Controllers
{
    /// <summary>
    /// 房屋管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[MyAuthorizeFilter]
    public class HousrController : Controller
    {
        private readonly HouseService _houseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HousrController(HouseService houseService) 
        {
            _houseService = houseService;
        }

        /// <summary>
        /// 获取所有房屋集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<WHouseinfo> GetHouseList() 
        {
            return _houseService.GetHouseList();
        }
    }
}
