using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyHome.Models {

    /// <summary>
    /// 新闻
    /// </summary>
    public class News {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(100)]
        public string TITLE { set; get; }

        /// <summary>
        /// 内容
        /// </summary>
        public string CONTENT { set; get; }

        /// <summary>
        /// 作者
        /// </summary>
        public string AUTHOR { set; get; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime ADDTIME { set; get; }

        /// <summary>
        /// 楼层（最大）
        /// </summary>
        public int MAXLEVEL { get; set; }

        #region 一篇新闻对应一个分类
        /// <summary>
        /// 类别ID
        /// </summary>
        public int CID { get; set; }

        /// <summary>
        /// 绑定类别 外键
        /// </summary>
        [ForeignKey("CID")]
        public virtual Category Category { get; set; }
        #endregion
    }
}
