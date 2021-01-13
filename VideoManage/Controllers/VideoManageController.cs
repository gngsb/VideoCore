﻿using log4net;
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
    }
}
