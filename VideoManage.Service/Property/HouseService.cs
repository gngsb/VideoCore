using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoManage.EFCore;

namespace VideoManage.Service.Property
{
    public class HouseService
    {
        private readonly VideoContext _videoContext;

        public HouseService(VideoContext videoContext) 
        {
            _videoContext = videoContext;
        }

        public List<WHouseinfo> GetHouseList() 
        {
            return _videoContext.WHouseinfo.AsNoTracking().ToList();
        }
    }
}
