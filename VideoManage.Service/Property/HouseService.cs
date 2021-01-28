using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoManage.EFCore;

namespace VideoManage.Service.Property
{
    public class HouseService : BaseService
    {

        public HouseService(VideoContext videoContext, IMapper mapper) : base(videoContext, mapper)
        {

        }

        public List<WHouseinfo> GetHouseList() 
        {
            return _videoContext.WHouseinfo.AsNoTracking().ToList();
        }
    }
}
