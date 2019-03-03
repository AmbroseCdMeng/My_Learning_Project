using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyHome.Areas.Manage.Models;
using CompanyHome.Core;
using CompanyHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyHome.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class NewsController : Controller
    {
        /// <summary>
        /// 依赖注入的方式获取上下文
        /// </summary>
        private MyDBContent myDBContent;
        public NewsController(MyDBContent myDBContent) {
            this.myDBContent = myDBContent;
        }

        public IActionResult Index() {

            /* 读取数据 */
            var list = myDBContent.News.ToList();
            return View(list);
        }

        public IActionResult Add() {
            return View();
        }

        /// <summary>
        /// 新增新闻内容
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(NewsAddInfo info) {
            if (ModelState.IsValid) {

                /* 构造实体 */
                var newsModel = new News {
                    ADDTIME = DateTime.Now,
                    CONTENT = info.CONTENT,
                    TITLE = info.TITLE,
                    CID = 1,
                    AUTHOR = "Meng"
                };

                /* 调用上下文对象 添加数据到数据库 */
                myDBContent.Add(newsModel);

                /* 添加成功 跳转主页 */
                if (myDBContent.SaveChanges() > 0) {
                    return Redirect("/manage/news/index");
                }

                /* 添加失败 返回错误提示 */
                ModelState.AddModelError("TITLE", "保存失败");
            }
            return View(info);
        }
    }
}