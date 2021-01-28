using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VideoManage.EFCore;

namespace VideoManage.Service
{
    public class BaseService
    {
        protected VideoContext _videoContext;

        /// <summary>
        /// 自动映射
        /// </summary>
        protected IMapper _mapper;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="videoContext"></param>
        /// <param name="mapper"></param>
        public BaseService(VideoContext videoContext, IMapper mapper) 
        {
            _videoContext = videoContext;
            _mapper = mapper;
        }
    }
}
