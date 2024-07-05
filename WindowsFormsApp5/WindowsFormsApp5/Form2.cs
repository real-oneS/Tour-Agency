using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

 

        private void button2_Click(object sender, EventArgs e)
        {
            Form f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form f4 = new Form4();
            f4.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form f5 = new Form5();
            f5.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form f6 = new Form6();
            f6.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form f7 = new Form7();
            f7.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form f8 = new Form8();
            f8.Show();
            this.Hide();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
