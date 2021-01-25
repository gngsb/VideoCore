﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VideoManage.EFCore
{
    public partial class VideoContext : DbContext
    {
        public VideoContext()
        {
        }

        public VideoContext(DbContextOptions<VideoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<VCountries> VCountries { get; set; }
        public virtual DbSet<VType> VType { get; set; }
        public virtual DbSet<VVideolist> VVideolist { get; set; }
        public virtual DbSet<VVideos> VVideos { get; set; }
        public virtual DbSet<WAdmininfo> WAdmininfo { get; set; }
        public virtual DbSet<WCostinfo> WCostinfo { get; set; }
        public virtual DbSet<WHouseinfo> WHouseinfo { get; set; }
        public virtual DbSet<WRepairinfo> WRepairinfo { get; set; }
        public virtual DbSet<WUserinfo> WUserinfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VCountries>(entity =>
            {
                entity.ToTable("v_countries");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Title)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<VType>(entity =>
            {
                entity.ToTable("v_type");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Remarks)
                    .HasColumnType("varchar(500)")
                    .HasComment("评论")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TypeName)
                    .HasColumnType("varchar(255)")
                    .HasComment("类型名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<VVideolist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_videolist");

                entity.Property(e => e.Cid).HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ImgUrl)
                    .HasColumnType("varchar(500)")
                    .HasComment("图片路径")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasComment("视频名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Remarks)
                    .HasColumnType("varchar(1000)")
                    .HasComment("说明")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Title)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasComment("是否删除 0：是   1：否")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TypeId)
                    .HasColumnType("int(11)")
                    .HasComment("类型id");

                entity.Property(e => e.TypeName)
                    .HasColumnType("varchar(255)")
                    .HasComment("类型名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<VVideos>(entity =>
            {
                entity.ToTable("v_videos");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Cid).HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.ImgUrl)
                    .HasColumnType("varchar(500)")
                    .HasComment("图片路径")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasComment("视频名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Remarks)
                    .HasColumnType("varchar(1000)")
                    .HasComment("说明")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasComment("是否删除 0：是   1：否")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TypeId)
                    .HasColumnType("int(11)")
                    .HasComment("类型id");
            });

            modelBuilder.Entity<WAdmininfo>(entity =>
            {
                entity.ToTable("w_admininfo");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasComment("用户名")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PassWord)
                    .HasColumnType("varchar(255)")
                    .HasComment("用户密码")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<WCostinfo>(entity =>
            {
                entity.ToTable("w_costinfo");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CoalMoney)
                    .HasColumnType("varchar(255)")
                    .HasComment("煤气费")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("缴费时间");

                entity.Property(e => e.ElectricMoney)
                    .HasColumnType("varchar(255)")
                    .HasComment("电费")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SerMoney)
                    .HasColumnType("varchar(255)")
                    .HasComment("物业费")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.StoMoney)
                    .HasColumnType("varchar(255)")
                    .HasComment("停车费")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasComment("用户编号");

                entity.Property(e => e.WaterMoney)
                    .HasColumnType("varchar(255)")
                    .HasComment("水费")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<WHouseinfo>(entity =>
            {
                entity.ToTable("w_houseinfo");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnType("varchar(255)")
                    .HasComment("房屋地址")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.HouseArea)
                    .HasColumnType("varchar(255)")
                    .HasComment("房屋面积")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.HouseType)
                    .HasColumnType("int(11)")
                    .HasComment("房屋户型");

                entity.Property(e => e.Number)
                    .HasColumnType("int(11)")
                    .HasComment("入住人数");
            });

            modelBuilder.Entity<WRepairinfo>(entity =>
            {
                entity.ToTable("w_repairinfo");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnType("varchar(255)")
                    .HasComment("保修地址")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("提交时间");

                entity.Property(e => e.RepairTime)
                    .HasColumnType("datetime")
                    .HasComment("维修时间");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasComment("业主编号");
            });

            modelBuilder.Entity<WUserinfo>(entity =>
            {
                entity.ToTable("w_userinfo");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ComeTime)
                    .HasColumnType("datetime")
                    .HasComment("用户入住时间");

                entity.Property(e => e.HouseId)
                    .HasColumnType("int(11)")
                    .HasComment("房屋编号");

                entity.Property(e => e.NumberId)
                    .HasColumnType("varchar(255)")
                    .HasComment("用户身份证号")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Phone)
                    .HasColumnType("varchar(255)")
                    .HasComment("用户手机号")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Sex)
                    .HasColumnType("int(11)")
                    .HasComment("用户性别");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}