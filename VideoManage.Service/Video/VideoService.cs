using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoManage.Constants;
using VideoManage.EFCore;
using VideoManage.EFCore.Models;
using VideoManage.Service.Extends;

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
            list.PageByIndex(query.page, query.limit);
            result.count = count;
            result.data = list;
            return result;
        }
    }
}
