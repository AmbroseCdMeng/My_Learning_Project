using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyHome.Models {

    /// <summary>
    /// 评论
    /// </summary>
    public class Comment {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = "内容")]
        [Required(ErrorMessage = "{0}是必填的")]
        [StringLength(400, ErrorMessage = "{0}最大长度为400")]
        public string CONTENT { set; get; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { set; get; }

        /// <summary>
        /// 发表人
        /// </summary>
        public string AUTHOR { set; get; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime ADDTIME { set; get; }

        /// <summary>
        /// 楼层
        /// </summary>
        public int LEVEL { get; set; }

        #region 一个评论对应一篇新闻
        /// <summary>
        /// 新闻ID
        /// </summary>
        public int NID { get; set; }

        /// <summary>
        /// 绑定新闻 外键
        /// </summary>
        [ForeignKey("NID")]
        public virtual News News { get; set; }
        #endregion

    }
}
