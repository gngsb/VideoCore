using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    /// 维修管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[MyAuthorizeFilter]
    public class RepairController : Controller
    {
        private readonly RepairService _repairService;
        private readonly IHttpContextAccessor _httpContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repairService"></param>
        public RepairController(RepairService repairService, IHttpContextAccessor httpContext)
        {
            _repairService = repairService;
            _httpContext = httpContext;
        }

        /// <summary>
        /// 获取分页列表数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="UserName"></param>
        /// <param name="Address"></param>
        /// <param name="RepairName"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public PageApiResult<WRepairinfo> GetPageList(int page, int limit, string UserName, string Address, string RepairName, DateTime? StartTime, DateTime? EndTime)
        {
            return _repairService.GetPageList(page, limit, UserName, Address, RepairName, StartTime, EndTime);
        }

        /// <summary>
        /// 新增维修信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result AddRepairInfo(RepairModel model)
        {
            return _repairService.AddRepairInfo(model);
        }

        /// <summary>
        /// 修改维修信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result UpdateRepairInfo(RepairModel model)
        {
            return _repairService.UpdateRepairInfo(model);
        }

        /// <summary>
        /// 获取维修详情信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public WRepairinfo GetRepairInfo(int id)
        {
            return _repairService.GetRepairInfo(id);
        }

        /// <summary>
        /// 删除维修信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Result DelRepairInfo(int id)
        {
            return _repairService.DelRepairInfo(id);
        }
    }
}
