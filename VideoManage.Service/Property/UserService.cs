using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoManage.Constants;
using VideoManage.EFCore;
using VideoManage.EFCore.Models;
using VideoManage.Service.Extends;

namespace VideoManage.Service.Property
{
    public class UserService : BaseService
    {
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="videoContext"></param>
        public UserService(VideoContext videoContext, IMapper mapper) : base(videoContext, mapper) 
        {
           
        }

        public PageApiResult<UserInfoModel> GetPageList(int page, int limit, string userName, string phone, DateTime? startTime, DateTime? endTime)
        {
            var count = _videoContext.WUserinfo.Count();
            var query = _videoContext.WUserinfo.AsNoTracking().Where(x => x.IsDel == 0);
            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(x => x.UserName.Contains(userName));
            }
            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(x => x.Phone.Contains(phone));
            }
            if (startTime.HasValue)
            {
                query = query.Where(x => x.ComeTime >= startTime);
            }
            if (endTime.HasValue)
            {
                query = query.Where(x => x.ComeTime <= endTime);
            }
            var list = query.PageByIndex(page, limit).OrderByDescending(x => x.ComeTime).ToList().Select(x => new UserInfoModel
            {
                Id = x.Id,
                UserName = x.UserName,
                NumberId = x.NumberId,
                ComeTime = x.ComeTime,
                Phone = x.Phone,
                Sex = x.Sex,
                HouseId = GetHouseName(x.HouseId)
            }).ToList();
            return new PageApiResult<UserInfoModel>(count, list);
        }

        public string GetHouseName(int? houseId)
        {
            return _videoContext.WHouseinfo.AsNoTracking().Where(x => x.Id == houseId).FirstOrDefault().Address;
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result AddUserInfo(UserModel model)
        {
            //WUserinfo info = new WUserinfo();
            //info.UserName = model.UserName;
            //info.Phone = model.Phone;
            //info.NumberId = model.NumberId;
            //info.HouseId = Convert.ToInt32(model.HouseId);
            //info.Sex = Convert.ToInt32(model.Sex);
            //info.IsDel = 0;
            //info.ComeTime = DateTime.Now;

            var info = _mapper.Map<WUserinfo>(model);
            info.IsDel = 0;
            info.ComeTime = DateTime.Now;

            _videoContext.Add(info);
            _videoContext.SaveChanges();
            return new Result() { msg = "添加成功" };
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WUserinfo GetUserInfo(int id) 
        {
            return _videoContext.WUserinfo.AsNoTracking().Where(a => a.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result UpdateUserInfo(UserModel model) 
        {
            var user = _videoContext.WUserinfo.Where(x => x.Id == model.Id && x.IsDel == 0).FirstOrDefault();
            user.UserName = model.UserName;
            user.Phone = model.Phone;
            user.Sex = Convert.ToInt32(model.Sex);
            user.NumberId = model.NumberId;
            user.HouseId = Convert.ToInt32(model.HouseId);
            _videoContext.Update(user);
            _videoContext.SaveChanges();
            return new Result() { msg = "修改成功" };
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result DeleteUserInfo(int id) 
        {
            var user = _videoContext.WUserinfo.Where(x => x.Id == id && x.IsDel == 0).FirstOrDefault();
            user.IsDel = 1;
            _videoContext.Update(user);
            _videoContext.SaveChanges();
            return new Result() { msg = "修改成功" };
        }

        /// <summary>
        /// 获取用户列表信息
        /// </summary>
        /// <returns></returns>
        public List<WUserinfo> GetUserList() 
        {
            return _videoContext.WUserinfo.Where(x => x.IsDel == 0).ToList();
        }
    }
}
