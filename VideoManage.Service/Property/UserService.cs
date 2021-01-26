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
    public class UserService
    {
        private readonly VideoContext _videoContext;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="videoContext"></param>
        public UserService(VideoContext videoContext)
        {
            _videoContext = videoContext;
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
            WUserinfo info = new WUserinfo();
            info.UserName = model.UserName;
            info.Phone = model.Phone;
            info.NumberId = model.NumberId;
            info.HouseId = Convert.ToInt32(model.HouseId);
            info.Sex = Convert.ToInt32(model.Sex);
            info.IsDel = 0;
            info.ComeTime = DateTime.Now;
            _videoContext.Add(info);
            _videoContext.SaveChanges();
            return new Result() { msg = "添加成功" };
        }
    }
}
