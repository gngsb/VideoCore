using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManage.EFCore.Models
{
    public class CostModel
    {
        public int? Id { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 房屋编号
        /// </summary>
        public int? HouseId { get; set; }
        /// <summary>
        /// 房屋地址
        /// </summary>
        public string HouseAddress { get; set; }
        /// <summary>
        /// 缴费类型 1：水费  2：电费  3：物业费  4：停车费  5：煤气费
        /// </summary>
        public string CostType { get; set; }
        /// <summary>
        /// 缴费金额
        /// </summary>
        public string CostMoney { get; set; }
        /// <summary>
        /// 缴费时间
        /// </summary>
        public DateTime? PayTime { get; set; }
    }
}
