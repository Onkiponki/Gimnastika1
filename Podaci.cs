using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Gimnastika1
{
    public partial class Podaci : Form
    {
        string tabela;
        SqlConnection conn = Konekcija.zakonektuj();
        string komanda;
        public Podaci()
        {
            InitializeComponent();
        }
        public Podaci(string tabela)
        {
            this.tabela = tabela;
            InitializeComponent();
        }
        private void Podaci_Load(object sender, EventArgs e)
        {
            komanda = $"select * from {tabela}";
            SqlDataAdapter adapter = new SqlDataAdapter(komanda,conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["id"].ReadOnly = true;
        }
    }
}
