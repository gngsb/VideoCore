﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace VideoManage.EFCore
{
    public partial class WUserinfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 房屋编号
        /// </summary>
        public int? HouseId { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public int? Sex { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 用户入住时间
        /// </summary>
        public DateTime? ComeTime { get; set; }
        /// <summary>
        /// 用户身份证号
        /// </summary>
        public string NumberId { get; set; }
    }
}