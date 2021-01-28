using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoManage.Constants;
using VideoManage.EFCore;
using VideoManage.EFCore.Models;
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

        /// <summary>
        /// 获取当前房屋信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public WHouseinfo GetHouseInfo(int id) 
        {
            return _houseService.GetHouseInfo(id);
        }

        /// <summary>
        /// 新增房屋信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result AddHouseInfo(HouseModel model) 
        {
            return _houseService.AddHouseInfo(model);
        }

        /// <summary>
        /// 修改房屋信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result UpdateHouseInfo(HouseModel model) 
        {
            return _houseService.UpdateHouseInfo(model);
        }
    }
}
