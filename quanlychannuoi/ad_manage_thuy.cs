using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlychannuoi
{
    public partial class ad_manage_thuy : Form
    {
        public ad_manage_thuy()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ad_manage_chicucthuy form1 = new ad_manage_chicucthuy();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ad_manage_gietmo form1 = new ad_manage_gietmo();
            form1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ad_manage_thuoc form1 = new ad_manage_thuoc();
            form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           ad_manage_giamgiu form1 = new ad_manage_giamgiu();
            form1.Show();
            this.Hide();
        }
    }
}
