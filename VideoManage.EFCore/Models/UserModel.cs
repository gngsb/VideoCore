using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManage.EFCore.Models
{
    public class UserModel
    {
        public int? Id { get; set; }

        public string UserName { get; set; }
        /// <summary>
        /// 房屋编号
        /// </summary>
        public string HouseId { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 用户身份证号
        /// </summary>
        public string NumberId { get; set; }
    }
}
