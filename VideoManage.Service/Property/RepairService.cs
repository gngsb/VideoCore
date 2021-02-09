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

    /// <summary>
    /// 维修管理
    /// </summary>
    public class RepairService : BaseService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="videoContext"></param>
        /// <param name="mapper"></param>
        public RepairService(VideoContext videoContext, IMapper mapper) : base(videoContext, mapper)
        {

        }

        /// <summary>
        /// 获取分页列表数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="UserName"></param>
        /// <param name="Address"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public PageApiResult<WRepairinfo> GetPageList(int page, int limit, string UserName, string Address,string RepairName, DateTime? StartTime, DateTime? EndTime)
        {
            var query = _videoContext.WRepairinfo.AsNoTracking().Where(x => x.IsDel == 0);
            if (!string.IsNullOrEmpty(UserName))
            {
                query = query.Where(x => x.UserName.Contains(UserName));
            }

            if (!string.IsNullOrEmpty(Address))
            {
                query = query.Where(x => x.Address.Contains(Address));
            }

            if (!string.IsNullOrEmpty(RepairName))
            {
                query = query.Where(x => x.RepairName.Contains(RepairName));
            }

            if (StartTime.HasValue)
            {
                query = query.Where(x => x.RepairTime >= StartTime);
            }

            if (EndTime.HasValue)
            {
                query = query.Where(x => x.RepairTime <= EndTime);
            }
            int count = query.Count();
            var result = query.OrderByDescending(x => x.RepairTime).PageByIndex(page, limit).ToList();
            return new PageApiResult<WRepairinfo>(count, result);
        }

        /// <summary>
        /// 新增维修信息
        /// </summary>
        /// <returns></returns>
        public Result AddRepairInfo(RepairModel model)
        {
            var repair = _mapper.Map<WRepairinfo>(model);
            repair.CreateTime = DateTime.Now;
            repair.IsDel = 0;
            _videoContext.Add(repair);
            _videoContext.SaveChanges();
            return new Result { msg = "新增成功" };
        }

        /// <summary>
        /// 修改维修信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result UpdateRepairInfo(RepairModel model)
        {
            var repair = _videoContext.WRepairinfo.Where(x => x.Id == model.Id && x.IsDel == 0).FirstOrDefault();
            repair.RepairName = model.RepairName;
            repair.RepairTime = model.RepairTime;
            repair.UserName = model.UserName;
            repair.Address = model.Address;
            _videoContext.Update(repair);
            _videoContext.SaveChanges();
            return new Result { msg = "修改成功" };
        }

        /// <summary>
        /// 获取维修详情信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WRepairinfo GetRepairInfo(int id)
        {
            return _videoContext.WRepairinfo.Where(x => x.Id == id && x.IsDel == 0).FirstOrDefault();
        }

        /// <summary>
        /// 删除维修信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result DelRepairInfo(int id)
        {
            var repair = _videoContext.WRepairinfo.Where(x => x.Id == id && x.IsDel == 0).FirstOrDefault();
            repair.IsDel = 1;
            _videoContext.Update(repair);
            _videoContext.SaveChanges();
            return new Result { msg = "删除成功" };
        }
    }
}
