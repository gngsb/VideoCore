using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VideoManage.Constants.Configurations
{
    /// <summary>
    /// appsettings.json配置文件数据读取类
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 配置文件的根节点
        /// </summary>
        private static readonly IConfigurationRoot _config;

        static AppSettings() 
        {
            // 加载appsettings.json，并构建IConfigurationRoot
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json", true, true);
            _config = builder.Build();
        }

        /// <summary>
        /// JWT配置文件
        /// </summary>
        public static class JWT 
        {
            public static string secret => _config["Jwt:Secret"];

            public static string issuer => _config["Jwt:Issuer"];

            public static string audience => _config["Jwt:Audience"];
        }

        /// <summary>
        /// Session配置文件
        /// </summary>
        public static class Session 
        {
            public static string Name => _config["Session:Name"];

            public static string TimeOut => _config["Session:TimeOut"];
        }

        /// <summary>
        /// Redis配置文件
        /// </summary>
        public static class Redis 
        {
            public static string RedisConnectionString => _config["Redis:RedisConnectionString"];

            public static string InstanceName => _config["Redis:InstanceName"];

            public static string DefaultDB => _config["Redis:DefaultDB"];
        }
    }
}
