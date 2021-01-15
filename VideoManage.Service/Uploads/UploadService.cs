using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VideoManage.Constants;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

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
            try 
            {
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
            }
            catch (Exception ex) 
            {
                result.code = "1";
            }
            
            return result;
        }

        /// <summary>
        /// 视频上传
        /// </summary>
        /// <returns></returns>
        public async Task<string> UploadVideo(IFormFile data,string lastModified,string total,string fileName,string index) 
        {
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
                    });
                }
                bool mergeOk = false;
                if (total == index)
                {
                    mergeOk = await FileMerge(lastModified, fileName);
                }

                Dictionary<string, object> result = new Dictionary<string, object>();
                result.Add("number", index);
                result.Add("mergeOk", mergeOk);
                return JsonConvert.SerializeObject(result);

            }
            catch (Exception ex)
            {
                Directory.Delete(temporary);//删除文件夹
                throw ex;
            }
        }

        public async Task<bool> FileMerge(string lastModified, string fileName)
        {
            bool ok = false;
            try
            {
                var temporary = Path.Combine(Directory.GetCurrentDirectory() + $@"\Files\Video\", lastModified);//临时文件夹
                //fileName = Request.Form["fileName"];//文件名
                string fileExt = Path.GetExtension(fileName);//获取文件后缀
                var files = Directory.GetFiles(temporary);//获得下面的所有文件
                var finalPath = Path.Combine(Directory.GetCurrentDirectory() + $@"\Files\Video\", DateTime.Now.ToString("yyMMddHHmmss") + fileExt);//最终的文件名（demo中保存的是它上传时候的文件名，实际操作肯定不能这样）
                var fs = new FileStream(finalPath, FileMode.Create);
                foreach (var part in files.OrderBy(x => x.Length).ThenBy(x => x))//排一下序，保证从0-N Write
                {
                    var bytes = System.IO.File.ReadAllBytes(part);
                    await fs.WriteAsync(bytes, 0, bytes.Length);
                    bytes = null;
                    System.IO.File.Delete(part);//删除分块
                }
                fs.Close();
                Directory.Delete(temporary);//删除文件夹
                ok = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ok;
        }
    }
}
