using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyHome.Areas.Manage.Models;
using CompanyHome.Core;
using CompanyHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyHome.Areas.Manage.Controllers {
    [Area("Manage")]
    public class PageController : Controller {

        /// <summary>
        /// 依赖注入的方式获取上下文
        /// </summary>
        private MyDBContent myDBContent;
        public PageController(MyDBContent myDBContent) {
            this.myDBContent = myDBContent;
        }

        public IActionResult Index() {

            /* 读取数据 */
            var list = myDBContent.Pages.ToList();
            return View(list);
        }

        public IActionResult Add() {
            return View();
        }

        /// <summary>
        /// 新增单页内容
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(PageAddInfo info) {
            if (ModelState.IsValid) {

                /* 构造实体 */
                var pageModel = new Page {
                    ADDTIME = DateTime.Now,
                    CONTENT = info.CONTENT,
                    IDENTITY = info.IDENTIFY,
                    TITLE = info.TITLE
                };

                /* 调用上下文对象 添加数据到数据库 */
                myDBContent.Add(pageModel);

                /* 添加成功 跳转主页 */
                if(myDBContent.SaveChanges() > 0) {
                    return Redirect("/manage/page/index");
                }

                /* 添加失败 返回错误提示 */
                ModelState.AddModelError("IDENTITY", "保存失败");
            }
            return View(info);
        }
        

        /// <summary>
        /// 验证 IDENTIFY 的唯一性 在实体标记中使用 Remote 调用
        /// </summary>
        /// <param name="Identify"></param>
        /// <returns>是否通过验证 existPage 为 null 说明 ID 不重复</returns>
        public IActionResult CheckIdentity(string Identify) {

            /* 验证传入的 ID 是否已经存在 */
            var existPage = myDBContent.Pages.FirstOrDefault(m => m.IDENTITY == Identify);
            return Json(existPage == null);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int id) {
            myDBContent.Pages.Remove(myDBContent.Pages.FirstOrDefault(m => m.ID == id));
            var res = myDBContent.SaveChanges();
            return Json(new { status = res > 0 });
        }
    }
}