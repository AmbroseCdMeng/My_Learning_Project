using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyHome.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyHome {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            /* 添添加授权支持 */
            /* 并添加使用cookie的方式 配置登录成功页面和没有权限时的跳转页面 */
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                o => {
                    o.LoginPath = new PathString("/manage/home/login");
                    o.AccessDeniedPath = new PathString("/Error/Forbidden");
                });

            /* 注册 SQLServer 数据库服务 */
            var sqlConnection = Configuration.GetConnectionString("SqlServerConnection");
            services.AddDbContext<MyDBContent>(option => option.UseSqlServer(sqlConnection));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            /* 使用授权 */
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes => {
                /* 手动新增区域路由 MapAreaRoute */
                routes.MapAreaRoute(
                    name: "areas_manage",
                    areaName: "Manage",
                    template: "manage/{controller=Home}/{action=Index}/{id?}");

                /* 对单页配置特殊路由 */
                routes.MapRoute(
                    name: "page",
                    template: "page/{identity}/",
                    defaults: new { Controller = "Page", action = "Index" });

                /* 对新闻页配置特殊路由 */
                routes.MapRoute(
                    name: "news",
                    template: "news/index/{page}/",
                    defaults: new { Controller = "News", action = "Index" });

                /* 默认路由：非区域路由使用 */
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
