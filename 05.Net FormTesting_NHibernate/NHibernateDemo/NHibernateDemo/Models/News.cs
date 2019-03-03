using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Models {
    public class News {
        public virtual int ID { set; get; }
        public virtual string Title { set; get; }
        public virtual string Content { set; get; }
        public virtual DateTime AddTime { set; get; }
        public virtual bool Status { set; get; }
    }
}
