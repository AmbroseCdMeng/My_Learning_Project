using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyHome.Areas.Manage.Models;
using CompanyHome.Core;
using CompanyHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyHome.Controllers
{
    public class NewsController : Controller
    {
        private MyDBContent myDBContent;
        public NewsController(MyDBContent myDBContent) {
            this.myDBContent = myDBContent;
        }
        /// <summary>
        /// 主页 列表显示 带分页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(int page = 1, int pagesize = 10)
        {
            var list = myDBContent.News.ToList();

            var total = list.Count();   //总数
            var totalPage = total / pagesize + (total % pagesize == 0 ? 0 : 1);
            if (page > totalPage) {
                page = totalPage;
            }

            ViewBag.Page = page;
            ViewBag.Total = total;
            ViewBag.TotalPage = totalPage;
            /* 跳过 (page - 1) * pageSize 条记录 取 pageSize 条 */
            return View(list.Skip((page - 1) * pagesize).Take(pagesize).ToList());
        }

        /// <summary>
        /// 新闻详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Detail(int id) {
            
            /* 获取并返回新闻实体 */
            var model = myDBContent.News.FirstOrDefault(m => m.ID == id);
            ViewBag.NewsModel = model;

            /* 获取评论列表 */
            ViewBag.Comments = myDBContent.Comments.OrderBy(m => m.LEVEL).Where(m => m.NID == id).ToList();

            return View();
        }

        /// <summary>
        /// 新闻新增评论
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Detail(CommentAddInfo info) {

            /* 根据 NID 查找对应的新闻实体 */
            var newsModel = myDBContent.News.FirstOrDefault(m => m.ID == info.NID);
            /* 计算当前楼层 */
            int level = newsModel.MAXLEVEL + 1;
            /* 当前最大楼层自增 */
            newsModel.MAXLEVEL++;
            /* 构造评论实体类 */
            var commenModel = new Comment {
                IP = HttpContext.Request.Host.ToString(),       //
                LEVEL = level,
                NID = info.NID,
                CONTENT = info.CONTENT,
                ADDTIME = DateTime.Now,
                AUTHOR = "ADMIN"        //
            };


            myDBContent.Comments.Add(commenModel);
            if (myDBContent.SaveChanges() > 0) {
                return Redirect("/news/detail/" + info.NID);
            }
            return Content("提交失败");
        }
    }
}