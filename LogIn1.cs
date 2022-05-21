using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gimnastika1
{
    public partial class LogIn1 : Form
    {
        public LogIn1()
        {
            InitializeComponent();
        }

        private void LogIn1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains("@") && textBox2.Text != "")
            {
                Menu form1 = new Menu();
                this.Hide();
                form1.ShowDialog();
                Application.Exit();
            }
        }
    }
}
