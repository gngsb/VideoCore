using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VideoManage.Constants;
using VideoManage.Service.Uploads;

namespace VideoManage.Hosting.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        string[] pictureFormatArray = { "png", "jpg", "jpeg" };

        private readonly UploadService _uploadService;

        public UploadController(UploadService uploadService) 
        {
            _uploadService = uploadService;
        }

        /// <summary>
        /// 视频图片上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PictureResult AddImage() //IFormFileCollection formCollection
        {
            PictureResult result = new PictureResult();
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);
            if (size > 10485760) 
            {
                result.code = "1";
                result.msg = "图片最大允许上传10M！";
                return result;
            }
            var fileName = ContentDispositionHeaderValue.Parse(files[0].ContentDisposition).FileName.Trim('"');
            string suffix = fileName.Split('.')[1];
            if (!pictureFormatArray.Contains(suffix)) 
            {
                result.code = "1";
                result.msg = "图片只允许上传png,jpg,jpeg类型的图片！";
                return result;
            }
            return _uploadService.AddImage(files[0]);
        }
    }
}
