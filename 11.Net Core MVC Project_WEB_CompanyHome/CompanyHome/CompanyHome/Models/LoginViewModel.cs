using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyHome.Models {
    public class LoginViewModel {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "用户名")]
        [StringLength(maximumLength: 16, MinimumLength = 5, ErrorMessage = "{0}长度应为6~16位")]
        public string USERNAME { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [Display(Name = "密码")]
        [StringLength(maximumLength: 16, MinimumLength = 5, ErrorMessage = "{0}长度应为6~16位")]
        public string PASSWORD { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        [Display(Name = "验证码")]
        [StringLength(maximumLength: 5, MinimumLength = 5)]
        [Remote("VerifyCode", "Home", "Manage", ErrorMessage = "{0}验证码不正确")]
        public string VERIFICATION { get; set; }
    }
}
