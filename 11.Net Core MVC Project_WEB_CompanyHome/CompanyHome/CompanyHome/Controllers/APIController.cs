using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Util;

namespace CompanyHome.Controllers
{
    public class APIController : Controller
    {
        /// <summary>
        /// 生成随机验证码图片
        /// </summary>
        /// <returns>图片文件</returns>
        public IActionResult GetValidateCode()
        {
            byte[] data = null;
            string code = ValidateCodeHelper.RandomCode(5);
            TempData["Code"] = code;
            /* 定义一个画板 */
            MemoryStream ms = new MemoryStream();
            using (Bitmap map = new Bitmap(80, 28)) {
                /* 画笔 */
                //g.Dispose();
                using (Graphics g = Graphics.FromImage(map)) {
                    g.Clear(Color.White);
                    g.DrawString(code, new Font("黑体", 18.0F), Brushes.Blue, new Point(4, 3));
                    /* 干扰线条 */
                    ValidateCodeHelper.PaintInterLine(g, 6, map.Width, map.Height);
                }
                map.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            data = ms.GetBuffer();
            return File(data, "Image/jpeg");
        }
    }
}