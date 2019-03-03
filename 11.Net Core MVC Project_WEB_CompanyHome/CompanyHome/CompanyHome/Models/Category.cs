using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyHome.Models {
    
    /// <summary>
    /// 分类实体
    /// </summary>
    public class Category {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [StringLength(15)]
        public string NAME { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        public int ORDER { set; get; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime ADDTIME { set; get; }

        #region 一个分类下对应多篇新闻
        public virtual List<News> News { get; set; }
        #endregion

        /// <summary>
        /// 构造函数初始化文章集合
        /// </summary>
        public Category() {
            News = new List<News>();
        }
    }
}
