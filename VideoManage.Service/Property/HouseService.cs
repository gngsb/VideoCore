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
    public class HouseService : BaseService
    {

        public HouseService(VideoContext videoContext, IMapper mapper) : base(videoContext, mapper)
        {

        }


        public PageApiResult<WHouseinfo> GetPageList(int page, int limit, string Address, string HouseType) 
        {
            var query = _videoContext.WHouseinfo.AsNoTracking();
            if (!string.IsNullOrEmpty(Address)) 
            {
                query = query.Where(x => x.Address.Contains(Address));
            }
            if (!string.IsNullOrEmpty(HouseType) && HouseType != "-1") 
            {
                query = query.Where(x => x.HouseType == Convert.ToInt32(HouseType));
            }
            var count = query.Count();
            var result = query.PageByIndex(page, limit).ToList();
            return new PageApiResult<WHouseinfo>(count, result);
        }

        public List<WHouseinfo> GetHouseList()
        {
            return _videoContext.WHouseinfo.AsNoTracking().ToList();
        }

        /// <summary>
        /// 获取当前房屋信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WHouseinfo GetHouseInfo(int id) 
        {
            return _videoContext.WHouseinfo.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// 新增房屋信息
        /// </summary>
        /// <returns></returns>
        public Result AddHouseInfo(HouseModel model) 
        {
            var info = _mapper.Map<WHouseinfo>(model);
            _videoContext.Add(info);
            _videoContext.SaveChanges();
            return new Result() { msg = "添加成功" };
        }

        /// <summary>
        /// 修改房屋信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result UpdateHouseInfo(HouseModel model) 
        {
            var info = _videoContext.WHouseinfo.AsNoTracking().Where(x => x.Id == model.Id).FirstOrDefault();
            info.Address = model.Address;
            info.HouseArea = model.HouseArea;
            info.HouseType = model.HouseType;
            _videoContext.Update(info);
            _videoContext.SaveChanges();
            return new Result() { msg = "修改成功" };
        }
    }
}
