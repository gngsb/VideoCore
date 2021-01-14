using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManage.EFCore.Models
{
    public class VideoModel
    {
        public int? Vid { get; set; }

        public string Name { get; set; }

        public int? Cid { get; set; }

        public int? TypeId { get; set; }

        public string ImgUrl { get; set; }

        public string Remarks { get; set; }
    }
}
