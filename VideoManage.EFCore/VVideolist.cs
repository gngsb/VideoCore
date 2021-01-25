﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace VideoManage.EFCore
{
    public partial class VVideolist
    {
        public int Id { get; set; }
        /// <summary>
        /// 视频名称
        /// </summary>
        public string Name { get; set; }
        public int? Cid { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 类型id
        /// </summary>
        public int? TypeId { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 是否删除 0：是   1：否
        /// </summary>
        public string Type { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
    }
}