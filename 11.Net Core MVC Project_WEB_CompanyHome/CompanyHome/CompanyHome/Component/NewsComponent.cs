using CompanyHome.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyHome.Component {

    /// <summary>
    /// 添加新闻视图组件类
    /// </summary>
    public class NewsComponent :ViewComponent{

        private MyDBContent myDBContent;
        public NewsComponent(MyDBContent myDBContent) {
            this.myDBContent = myDBContent;
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="num">数量（从视图中传入）</param>
        /// <returns></returns>
        public IViewComponentResult Invoke(int num) {
            var list = myDBContent.News.Take(num).ToList();
            return View(list);
        }
    }
}
