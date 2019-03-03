using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernateDemo.Models;

namespace NHibernateDemo.Dals {
    public class NewsDal {

        private ISession session;

        public NewsDal(ISession session) {
            this.session = session;
        }


        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public News GetNewsByID(int id) {
            return session.Get<News>(id);
        }
    }
}
