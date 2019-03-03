using CompanyHome.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyHome.Core {
    public class MyDBContent : DbContext {
        public MyDBContent(DbContextOptions<MyDBContent> options) : base(options) { }

        /// <summary>
        /// 上下文中绑定管理员实体
        /// </summary>
        public DbSet<Admin> Admins { get; set; }

        /// <summary>
        /// 上下文绑定分类实体
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// 上下文绑定新闻实体
        /// </summary>
        public DbSet<News> News { get; set; }

        /// <summary>
        /// 上下文绑定评论实体
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// 页面
        /// </summary>
        public DbSet<Page> Pages { get; set; }
    }
}
