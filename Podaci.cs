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
            if(tabela != "rezultat")
            {
                komanda = $"select * from {tabela}";
                adapter = new SqlDataAdapter(komanda, conn);
                dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["id"].Visible = false;
                dataGridView1.AllowUserToAddRows = true;
            }
            else
            {
                komanda = $"select ime + prezime as Ime, brojBodova as Skor, ocena as Ocena from rezultat join raspodela on raspodela.id = rezultat.idRaspodele join osoba on osoba.id = raspodela.idOsobe join godina on godina.id = raspodela.idGodine join nivoTakmicenja on nivoTakmicenja.id = raspodela.idNivoa";
                MessageBox.Show(komanda);
                adapter = new SqlDataAdapter(komanda, conn);
                dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AllowUserToAddRows = true;
            }
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
