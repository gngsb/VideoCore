using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManage.EFCore.Models
{
    public class PageQuery
    {
        public int page { get; set; }

        public int limit { get; set; }

        public string Name { get; set; }

        public int? Cid { get; set; }

        public int? TypeId { get; set; }

    }
}
