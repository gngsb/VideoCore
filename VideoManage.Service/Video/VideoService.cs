using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoManage.Constants;
using VideoManage.EFCore;
using VideoManage.EFCore.Models;
using VideoManage.Service.Extends;
using VideoManage.ToolKits.Helper;

namespace VideoManage.Service.Video
{
    public class VideoService
    {
        private readonly VideoContext _videoContext;

        public VideoService(VideoContext videoContext) 
        {
            _videoContext = videoContext;
        }

        public PageApiResult<VVideolist> GetVideo(PageQuery query) 
        {
            var result = new PageApiResult<VVideolist>();
            try 
            {
                var count = _videoContext.VVideolist.Count();
                var list = _videoContext.VVideolist.AsNoTracking();
                if (!string.IsNullOrEmpty(query.Name))
                {
                    list = list.Where(x => x.Name.Contains(query.Name));
                }
                if (query.Cid.HasValue)
                {
                    list = list.Where(x => x.Cid == query.Cid);
                }
                if (query.TypeId.HasValue)
                {
                    list = list.Where(x => x.TypeId == query.TypeId);
                }
                list = list.OrderBy(x => x.CreateTime);
                var lists = list.PageByIndex(query.page, query.limit).ToList();
                result.count = count;
                result.data = lists;
            }
            catch (Exception ex) 
            {
                result.code = "1";
                LoggerHelper.ErrorToFile("获取视频列表出现异常，异常原因：" + ex.Message);
            }
            
            return result;
        }

        /// <summary>
        /// 获取所有国家列表数据
        /// </summary>
        /// <returns></returns>
        public List<VCountries> getList() 
        {
            return _videoContext.VCountries.AsNoTracking().ToList();
        }

        /// <summary>
        /// 获取视频类别列表
        /// </summary>
        /// <returns></returns>
        public List<VType> getType()
        {
            return _videoContext.VType.AsNoTracking().ToList();
        }

        /// <summary>
        /// 获取视频详情
        /// </summary>
        /// <returns></returns>
        public VVideos SelectVideo(int Vid)
        {
            return _videoContext.VVideos.AsNoTracking().Where(x => x.Id == Vid).FirstOrDefault();
        }

        /// <summary>
        /// 添加视频信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result AddVideo(VideoModel model) 
        {
            Result result = new Result();
            try 
            {
                VVideos videos = new VVideos();
                videos.Cid = model.Cid;
                videos.ImgUrl = model.ImgUrl;
                videos.TypeId = model.TypeId;
                videos.Remarks = model.Remarks;
                videos.Name = model.Name;
                videos.CreateTime = DateTime.Now;
                videos.Type = "1";
                var res = _videoContext.VVideos.Add(videos);
                _videoContext.SaveChanges();
                if (res != null)
                {
                    result.code = "0";
                    result.msg = "新增成功";
                }
                else 
                {
                    result.code = "1";
                    result.msg = "新增失败";
                }
                return result;
            }
            catch (Exception ex) 
            {
                LoggerHelper.ErrorToFile("视频新增失败，失败原因：" + ex.Message);
                result.code = "1";
                return result;
            }
        }

        /// <summary>
        /// 修改视频信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result EditVideo(VideoModel model) 
        {
            Result result = new Result();
            try 
            {
                var video = _videoContext.VVideos.Where(x => x.Id == model.Vid).FirstOrDefault();
                if (video == null)
                {
                    result.code = "1";
                    result.msg = "视频信息查找失败！";
                    return result;
                }
                video.Name = model.Name;
                video.Cid = model.Cid;
                video.TypeId = model.TypeId;
                video.ModifyTime = DateTime.Now;
                video.ImgUrl = model.ImgUrl;
                video.Remarks = model.Remarks;
                _videoContext.VVideos.Update(video);
                _videoContext.SaveChanges();
                result.msg = "新增成功";
                return result;
            }
            catch (Exception ex)
            {
                LoggerHelper.ErrorToFile("视频修改失败，失败原因：" + ex.Message);
                result.code = "1";
                return result;
            }
           
        }

    }
}
