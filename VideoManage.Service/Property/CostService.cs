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
    public class CostService : BaseService
    {

        public CostService(VideoContext videoContext, IMapper mapper) : base(videoContext, mapper) 
        {
        
        }

        public PageApiResult<WCostinfo> GetPageList(int page, int limit, string UserName, string CostType,DateTime? StartTime,DateTime? EndTime) 
        {
            var query = _videoContext.WCostinfo.AsNoTracking();
            if (!string.IsNullOrEmpty(UserName)) 
            {
                query = query.Where(x => x.UserName.Contains(UserName));
            }
            if (!string.IsNullOrEmpty(CostType)) 
            {
                query = query.Where(x => x.CostType == CostType);
            }
            if (StartTime.HasValue) 
            {
                query = query.Where(x => x.PayTime >= StartTime);
            }
            if (EndTime.HasValue) 
            {
                query = query.Where(x => x.PayTime <= EndTime);
            }
            var count = query.Count();
            var result = query.OrderByDescending(x => x.PayTime).PageByIndex(page,limit).ToList();
            return new PageApiResult<WCostinfo>(count, result);
        }

        /// <summary>
        /// 新增缴费信息
        /// </summary>
        /// <returns></returns>
        public Result AddCostInfo(CostModel model) 
        {
            var cost = _mapper.Map<WCostinfo>(model);
            cost.IsDel = 0;
            _videoContext.Add(cost);
            _videoContext.SaveChanges();
            return new Result { msg = "新增成功" };
        }

        /// <summary>
        /// 修改缴费信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result UpdateCostInfo(CostModel model) 
        {
            var cost = _videoContext.WCostinfo.Where(x => x.Id == model.Id).FirstOrDefault();
            cost.UserId = model.UserId;
            cost.UserName = model.UserName;
            cost.HouseId = model.HouseId;
            cost.HouseAddress = model.HouseAddress;
            cost.PayTime = model.PayTime;
            cost.CostType = model.CostType;
            cost.CostMoney = model.CostMoney;
            _videoContext.Update(cost);
            _videoContext.SaveChanges();
            return new Result { msg = "修改成功" };
        }

        /// <summary>
        /// 删除缴费信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result DeleteCostInfo(int id) 
        {
            var cost = _videoContext.WCostinfo.Where(x => x.Id == id).FirstOrDefault();
            cost.IsDel = 1;
            _videoContext.WCostinfo.Update(cost);
            _videoContext.SaveChanges();
            return new Result { msg = "删除成功" };
        }

        /// <summary>
        /// 获取缴费详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WCostinfo GetCostInfo(int id) 
        {
            return _videoContext.WCostinfo.Where(x => x.Id == id).FirstOrDefault();
        }

    }
}
