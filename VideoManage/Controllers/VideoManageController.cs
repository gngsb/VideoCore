using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoManage.Constants;
using VideoManage.EFCore;
using VideoManage.EFCore.Models;
using VideoManage.Service.Video;

namespace VideoManage.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class VideoManageController : ControllerBase
    {
        private readonly VideoService _service;

        public VideoManageController(VideoService service) 
        {
            _service = service;
        }

        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="Name"></param>
        /// <param name="CId"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        [HttpGet]
        public PageApiResult<VVideolist> GetVideo(int page,int limit,string Name,int? CId,int? TypeId) 
        {
            PageQuery query = new PageQuery
            {
                page = page,
                limit = limit,
                Name = Name,
                Cid = CId,
                TypeId = TypeId
            };
            return _service.GetVideo(query);
        }

        /// <summary>
        /// 获取所有国家列表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<VCountries> getList() 
        {
            return _service.getList();
        }

        /// <summary>
        /// 获取视频类别列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<VType> getType() 
        {
            return _service.getType();
        }

        /// <summary>
        /// 获取视频详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public VVideos SelectVideo(int Vid) 
        {
            return _service.SelectVideo(Vid);
        }

        /// <summary>
        /// 添加视频信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result AddVideo(VideoModel model) 
        {
            return _service.AddVideo(model);
        }

        /// <summary>
        /// 修改视频信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Result EditVideo(VideoModel model) 
        {
            return _service.EditVideo(model);
        }
    }
}
