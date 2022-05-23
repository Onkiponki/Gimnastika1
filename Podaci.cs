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
        DataTable dt;
        SqlDataAdapter adapter;
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
            adapter = new SqlDataAdapter(komanda,conn);
             dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["id"].ReadOnly = true;
            dataGridView1.AllowUserToAddRows = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable = dt.GetChanges();
            adapter.UpdateCommand = new SqlCommandBuilder(adapter).GetUpdateCommand();
            if (dataTable != null)
            {
                adapter.Update(dataTable);
            }
            this.Close();

        }
    }
}
