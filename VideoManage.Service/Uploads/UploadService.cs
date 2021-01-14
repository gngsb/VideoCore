using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using VideoManage.Constants;

namespace VideoManage.Service.Uploads
{
    /// <summary>
    /// 图片上传服务类
    /// </summary>
    public class UploadService
    {
        private IHostingEnvironment hostingEnv;

        public UploadService(IHostingEnvironment env) 
        {
            hostingEnv = env;
        }

        /// <summary>
        /// 视频图片上传
        /// </summary>
        /// <returns></returns>
        public PictureResult AddImage(IFormFile files) 
        {
            PictureResult result = new PictureResult();
            var fileName = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
            //string filePath = hostingEnv.WebRootPath+ $@"\Files\Pictures\";
            string filePath = Directory.GetCurrentDirectory() + $@"\Files\Pictures\";
            if (!Directory.Exists(filePath)) 
            {
                Directory.CreateDirectory(filePath);
            }
            string suffix = fileName.Split('.')[1];
            fileName = Guid.NewGuid() + "." + suffix;
            string fileFullName = filePath + fileName;
            using (FileStream fs = File.Create(fileFullName)) 
            {
                files.CopyTo(fs);
                fs.Flush();
            }
            result.code = "0";
            result.msg = "图片上传成功";
            result.image = $"../Files/Pictures/{fileName}";
            return result;
        }
    }
}
