using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyHome.Models {
    /// <summary>
    /// 后台管理员实体
    /// </summary>
    [Serializable]
    public class Admin {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(15)]
        public string USERNAME { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string PASSWORD { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CREATETIME { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LASTLOGINTIME { get; set; }
    }
}
