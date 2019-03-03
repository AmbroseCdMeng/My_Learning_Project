using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyHome.Areas.Manage.Models {

    /// <summary>
    /// 新闻信息（新增）实体类
    /// </summary>
    public class NewsAddInfo {

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        [Required(ErrorMessage = "{0}是必填的")]
        [StringLength(120, ErrorMessage = "{0}最大长度为120个字符")]
        public string TITLE { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = "内容")]
        [Required(ErrorMessage = "{0}是必填的")]
        public string CONTENT { get; set; }
    }
}
