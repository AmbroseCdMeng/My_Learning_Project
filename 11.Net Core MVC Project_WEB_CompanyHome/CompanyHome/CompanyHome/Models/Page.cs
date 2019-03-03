using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyHome.Models {
    public class Page {

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 标识符
        /// </summary>
        [StringLength(20)]
        public string IDENTITY { get; set; }

        [StringLength(200)]
        public string TITLE { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string CONTENT { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime ADDTIME { get; set; }
    }
}
