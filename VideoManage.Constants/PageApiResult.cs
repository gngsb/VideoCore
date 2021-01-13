using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManage.Constants
{
    public class PageApiResult<T>
    {
        public string code { get; set; } = "0";
        public string msg { get; set; }
        public long count { get; set; }
        public IEnumerable<T> data { get; set; }
    }
}
