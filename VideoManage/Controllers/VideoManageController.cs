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
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public PageApiResult<VVideolist> GetVideo(PageQuery query) 
        {
            return _service.GetVideo(query);
        }
    }
}
