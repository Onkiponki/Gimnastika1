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
    public partial class Menu: Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void osobeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Podaci form1 = new Podaci("osoba");
            form1.ShowDialog();

        }

        private void takmicenjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Podaci form1 = new Podaci("nivoTakmicenja");
            form1.ShowDialog();
        }

        private void godineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Podaci form1 = new Podaci("godina");
            form1.ShowDialog();
        }

        private void raspodelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Raspodela form1 = new Raspodela();
            form1.Show();

        }
    }
}
