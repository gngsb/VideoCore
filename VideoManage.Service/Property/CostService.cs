using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VideoManage.EFCore;

namespace VideoManage.Service.Property
{
    public class CostService : BaseService
    {

        public CostService(VideoContext videoContext, IMapper mapper) : base(videoContext, mapper) 
        {
        
        }


    }
}
