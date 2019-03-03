using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NHibernate;
using NHibernateDemo.Dals;
using NHibernateDemo.Models;

namespace NHibernateDemo {
    public partial class Form1 : Form {

        private ISession session;
        private NewsDal newsDal;
        private SessionFactory sessionFactory = new SessionFactory();

        public Form1() {
            InitializeComponent();
            session = sessionFactory.GetSession();
            newsDal = new NewsDal(session);
        }

        private void button1_Click(object sender, EventArgs e) {

            News newsInfo = newsDal.GetNewsByID(Convert.ToInt32(textBox1.Text));
            if (newsInfo != null) {
                MessageBox.Show(newsInfo.ToString());
            }
        }
    }
}
