using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util {
    /// <summary>
    /// 验证码的辅助类
    /// </summary>
    public static class ValidateCodeHelper {

        /// <summary>
        /// 随机生成指定长度的验证码字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns>随机字符串</returns>
        public static string RandomCode(int length) {
            string s = "0123456789abcdefghijklmnopqrstuvwxyz";
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            int index;
            for (int i = 0; i < length; i++) {
                index = rand.Next(0, s.Length);
                sb.Append(s[index]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成验证码干扰线条
        /// </summary>
        /// <param name="g"></param>
        /// <param name="num"></param>
        /// <param name="width"></param>
        /// <param name="heigh"></param>
        public static void PaintInterLine(Graphics g, int num, int width, int heigh) {
            Random r = new Random();
            int startX, startY, endX, endY;
            for (int i = 0; i < num; i++) {
                startX = r.Next(0, width);
                startY = r.Next(0, heigh);
                endX = r.Next(0, width);
                endY = r.Next(0, heigh);
                g.DrawLine(new Pen(Brushes.Red), startX, startY, endX, endY);
            }
        }
    }
}

