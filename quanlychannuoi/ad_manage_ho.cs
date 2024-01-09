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
    public partial class ad_manage_ho : Form
    {
        public ad_manage_ho()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           ad_manage_chebien form1 = new ad_manage_chebien();
            form1.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ad_manage_tochucchungnhan form1 = new ad_manage_tochucchungnhan();
            form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ad_manage_giayphep form1 = new ad_manage_giayphep();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ad_manage_Hochannuoi form1 = new ad_manage_Hochannuoi();
            form1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ad_manage_vungchannuoi form1 = new ad_manage_vungchannuoi();
            form1.Show();
            this.Hide();
        }
    }
}
