using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManage.EFCore.Models
{
    public class RepairModel
    {
        public int? Id { get; set; }
        /// <summary>
        /// 报修地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 报修人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 维修人
        /// </summary>
        public string RepairName { get; set; }
        /// <summary>
        /// 报修信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }
    }
}
