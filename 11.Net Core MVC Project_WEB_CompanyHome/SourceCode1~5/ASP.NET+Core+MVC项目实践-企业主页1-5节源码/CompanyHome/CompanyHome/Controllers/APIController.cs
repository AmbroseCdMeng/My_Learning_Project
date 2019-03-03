using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CompanyHome.Core;
using Microsoft.AspNetCore.Mvc;

namespace CompanyHome.Controllers
{
    public class APIController : Controller
    {
        public IActionResult GetValidateCode()
        {
            byte[] data = null;
            string code = CommonHelper.RandomCode(5);
            TempData["code"] = code;
            //定义一个画板
            MemoryStream ms = new MemoryStream();
            using (Bitmap map = new Bitmap(80, 28))
            {
                //画笔,在指定画板画板上画图
                //g.Dispose();
                using (Graphics g = Graphics.FromImage(map))
                {
                    g.Clear(Color.White);
                    g.DrawString(code, new Font("黑体", 18.0F), Brushes.Blue, new Point(4, 3));
                    //绘制干扰线
                    CommonHelper.PaintInterLine(g, 6, map.Width, map.Height);
                }
                map.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            data = ms.GetBuffer();
            return File(data, "image/jpeg");
        }
    }
}