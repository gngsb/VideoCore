﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VideoManage.Constants;
using VideoManage.Service.Uploads;

namespace VideoManage.Hosting.Controllers
{
    [Authorize]
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

        /// <summary>
        /// 视频上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UploadVideo() 
        {
            var data = Request.Form.Files["data"];
            string lastModified = Request.Form["lastModified"].ToString();
            var total = Request.Form["total"];
            var fileName = Request.Form["fileName"];
            var index = Request.Form["index"];
            string temporary = Path.Combine(Directory.GetCurrentDirectory() + $@"\Files\Video\", lastModified);//临时保存分块的目录
            try
            {
                if (!Directory.Exists(temporary))
                    Directory.CreateDirectory(temporary);
                string filePath = Path.Combine(temporary, index.ToString());
                if (!Convert.IsDBNull(data))
                {
                    await Task.Run(() => {
                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        data.CopyTo(fs);
                        fs.Close();
                    });
                }
                bool mergeOk = false;
                if (total == index)
                {
                    mergeOk = await _uploadService.FileMerge(lastModified, fileName);
                }

                Dictionary<string, object> result = new Dictionary<string, object>();
                result.Add("number", Convert.ToInt32(index.ToString()));
                result.Add("mergeOk", mergeOk);
                return Ok(result);

            }
            catch (Exception ex)
            {
                Directory.Delete(temporary);//删除文件夹
                throw ex;
            }

        }

        [HttpGet]
        public bool UploadOss() 
        { 
            string Thumbnails = "https://file.cnnho-vu.cn//ADUploadFiles/2020/09/29/e347e304-6296-44b6-ae4c-06a41bf2175c.jpg";
            Thumbnails = Thumbnails.Split('?')[0];
            string path = "";
            try
            {
                #region 图片上传阿里oss
                string tempPath = "/Upload/VideoCover/";
                string saveDoc = System.AppDomain.CurrentDomain.BaseDirectory;
                saveDoc = System.IO.Directory.GetParent(saveDoc).FullName + tempPath.Replace("/", "\\");
                if (!Directory.Exists(saveDoc))
                {
                    Directory.CreateDirectory(saveDoc);
                }

                var bol = _uploadService.RemoteFileExists(Thumbnails);
                if (bol)
                {
                    string FileName = System.IO.Path.GetFileName(Thumbnails);//文件名
                    string savePath = saveDoc + "\\" + FileName;
                    //先下载到本地，再上传oss
                    WebClient wClient = new WebClient();
                    wClient.DownloadFile(Thumbnails, savePath);
                    //上传阿里云oss
                    //var client = AliOssTools._OssClient;
                    //上传文件
                    //var result = client.PutObject(AliOssTools.ImgBucket, "LZVideoCover/" + FileName, savePath);
                    //path = ImgSite + "LZVideoCover/" + FileName;
                    return true;
                }
                else
                {
                    //LogService.WriteErrorLog("视频封面保存OSS失败，该封面地址不存在,路径：" + Thumbnails);
                    return false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                //LogService.WriteErrorLog($"视频封面保存OSS异常，图片地址：{Thumbnails}异常原因：" + ex.Message);
                return false;
            }
        }



    }
}
