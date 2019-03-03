using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyHome.Areas.Manage.Models {
    /// <summary>
    /// 单页信息 (新增) 实体
    /// </summary>
    public class PageAddInfo {

        /// <summary>
        /// URL标识符
        /// </summary>
        [Display(Name = "标识符")]
        [Required(ErrorMessage = "{0}是必填的")]
        [StringLength(20, ErrorMessage = "{0}最大长度为20")]
        [RegularExpression(@"^\w+$", ErrorMessage = "{0}必须是英文字母或数字")]
        [Remote("CheckIdentity", "Page", "Manage", ErrorMessage = "{0}已经被占用")]
        public string IDENTIFY { get; set; }

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
