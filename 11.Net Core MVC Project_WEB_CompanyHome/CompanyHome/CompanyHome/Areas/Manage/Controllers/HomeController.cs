using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CompanyHome.Core;
using CompanyHome.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyHome.Areas.Manage.Controllers {
    [Area("Manage")]
    public class HomeController : Controller {

        private MyDBContent myDBContent;

        public HomeController(MyDBContent myDBContent) {
            this.myDBContent = myDBContent;
        }

        #region 页面
        [Authorize(Roles = "admin")]
        public IActionResult Index() {
            return View();
        }

        public IActionResult Login() {
            return View();
        }
        #endregion

        #region 登录(含验证码的异步校验)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginViewModel model, string ReturnUrl) {

            if (!ModelState.IsValid) {
                return null;
            }
            /* 采用异步验证 故屏蔽以下登录验证 */
            //if (!model.VERIFICATION.Equals(TempData["Code"])) {
            //    throw new Exception("验证码输入错误");
            //}

            /* 查询用户信息 核对密码 */
            var user = myDBContent.Admins.Where(p => p.USERNAME == model.USERNAME).FirstOrDefault();
            if (user != null && user.PASSWORD == model.PASSWORD) {
                
                //登录成功 调用授权服务
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, model.USERNAME));
                /* 测试：默认使用 admin 角色 */
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                if (!string.IsNullOrEmpty(ReturnUrl)) {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            //登录失败
            ModelState.AddModelError("USERNAME", "用户名或密码错误");
            return View();
        } 
        
        /// <summary>
        /// 异步校验验证码
        /// </summary>
        /// <param name="verification"></param>
        /// <returns></returns>
        public IActionResult VerifyCode(string verification) {
            if (!verification.Equals(TempData["Code"])) {
                /* $符号 用于替代以前的string.format */
                return Json(data: $"验证码错误");
            }
            return Json(data: true);
        }
        #endregion

        #region 新增用户（非注册功能 仅供测试使用）
        public bool AddAdmin() {
            /* ID 作为主键 可以不指定 默认会自增 */
            myDBContent.Add(new Admin { USERNAME = "ADMIN", PASSWORD = "ADMIN", CREATETIME = DateTime.Now, LASTLOGINTIME = DateTime.Now});
            myDBContent.SaveChanges();
            return true;
        }
        #endregion

    }
}