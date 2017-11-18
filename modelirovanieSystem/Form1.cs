using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace modelirovanieSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double ll = Convert.ToDouble(l.Text);
            int tt1 = Convert.ToInt32(t1.Text);
            int tt2 = Convert.ToInt32(t2.Text);
            double pp1 = Convert.ToDouble(p1.Text);
            int KK = Convert.ToInt32(K.Text);
            OOS oos = new OOS(ll,tt1,tt2,pp1, KK);
            oos.start();
            dlina.Text = oos.getOcherdCount()+"";
            int time = oos.time;
            vremya.Text = time + "";
            maxDlina.Text = oos.max + "";
            SrDlina.Text = (double)((double)oos.sum / (double)time) + "";
        }

        private void l_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
