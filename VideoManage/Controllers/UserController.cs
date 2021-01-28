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
    /// 用户管理控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[MyAuthorizeFilter]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserController(UserService userService) 
        {
            _userService = userService;
        }

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PageApiResult<UserInfoModel> GetPageList(int page, int limit,string userName,string phone,DateTime? startTime,DateTime? endTime) 
        {
            return _userService.GetPageList(page, limit, userName, phone, startTime, endTime);
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result AddUserInfo(UserModel model) 
        {
            return _userService.AddUserInfo(model);
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public WUserinfo GetUserInfo(int id) 
        {
            return _userService.GetUserInfo(id);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result UpdateUserInfo(UserModel model) 
        {
            return _userService.UpdateUserInfo(model);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Result DeleteUserInfo(int id) 
        {
            return _userService.DeleteUserInfo(id);
        }
    }
}
