using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyHome.Models {

    /// <summary>
    /// 评论信息（新增）实体类
    /// </summary>
    public class CommentAddInfo {

        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = "内容")]
        [Required(ErrorMessage = "{0}是必填的")]
        [StringLength(400, ErrorMessage = "{0}最大长度为400")]
        public string CONTENT { set; get; }

        /// <summary>
        /// 新闻ID
        /// </summary>
        public int NID { get; set; }
    }
}
