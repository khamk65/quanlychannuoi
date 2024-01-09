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
    public partial class ad_manage_sanpham : Form
    {
        public ad_manage_sanpham()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ad_manage_sanpham_coso form1 = new ad_manage_sanpham_coso();
            form1.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ad_sanpham form1 = new ad_sanpham();
            form1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ad_manage_khaonghiem form1 = new ad_manage_khaonghiem();
            form1.Show();
        }
    }
}
