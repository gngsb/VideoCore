using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManage.EFCore.Models
{
    public class HouseModel
    {
        public int? Id { get; set; }
        /// <summary>
        /// 房屋地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 房屋户型 0：两室一厅一厨一卫  1：三室一厅一厨一卫  2：三室两厅一厨一卫  3：三室两厅一厨两卫
        /// </summary>
        public int? HouseType { get; set; }
        /// <summary>
        /// 房屋面积
        /// </summary>
        public string HouseArea { get; set; }
    }
}
