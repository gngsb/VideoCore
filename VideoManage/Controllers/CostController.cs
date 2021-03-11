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
    /// 缴费管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[MyAuthorizeFilter]
    public class CostController : Controller
    {
        private readonly CostService _costService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="costService"></param>
        public CostController(CostService costService) 
        {
            _costService = costService;
        }

        /// <summary>
        /// 获取缴费分页信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="UserName"></param>
        /// <param name="CostType"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        [HttpGet]
        public PageApiResult<WCostinfo> GetPageList(int page, int limit, string UserName, string CostType, DateTime? StartTime, DateTime? EndTime) 
        {
            return _costService.GetPageList(page, limit, UserName, CostType, StartTime, EndTime);
        }

        /// <summary>
        /// 新增缴费信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result AddCostInfo(CostModel model) 
        {
            return _costService.AddCostInfo(model);
        }

        /// <summary>
        /// 修改缴费信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result UpdateCostInfo(CostModel model) 
        {
            return _costService.UpdateCostInfo(model);
        }

        /// <summary>
        /// 删除缴费信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Result DeleteCostInfo(int id) 
        {
            return _costService.DeleteCostInfo(id);
        }

        /// <summary>
        /// 获取缴费详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public WCostinfo GetCostInfo(int id) 
        {
            return _costService.GetCostInfo(id);
        }

    }
}
