using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManage.Constants
{
    public class Result
    {
        public string code { get; set; } = "0";
        public string msg { get; set; }
    }

    public class PageApiResult<T> : Result
    {

        public long count { get; set; }
        public IEnumerable<T> data { get; set; }
    }

    public class PictureResult : Result
    {
        public string image { get; set; }
    }
}
