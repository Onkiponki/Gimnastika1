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
    public partial class Raspodela : Form
    {
        SqlConnection veza = Konekcija.zakonektuj();
        DataTable raspodela;
        int br = 0;
        string komanda;
        public Raspodela()
        {
            InitializeComponent();
        }
        private void Zaucitaj()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select * from raspodela", veza);
            raspodela = new DataTable();
            adapter.Fill(raspodela);
        }

        private void FeelingTheCombos()
        {
            SqlDataAdapter adapter;
            DataTable godina, osoba, nivo;

            godina = new DataTable();
            osoba = new DataTable();
            nivo = new  DataTable();

            komanda = "select id, naziv from godina";
            adapter = new SqlDataAdapter(komanda, veza);
            
            adapter.Fill(godina);

            komanda = "select id, ime + ' ' + prezime as Ime from osoba";
            adapter = new SqlDataAdapter(komanda, veza);

            adapter.Fill(osoba);

            komanda = "select id, naziv from nivoTakmicenja";
            adapter = new SqlDataAdapter(komanda, veza);

            adapter.Fill(nivo);

            label1.Text = raspodela.Rows[br]["id"].ToString();

            cmbGodina.DataSource = godina;
            cmbGodina.ValueMember = "id";
            cmbGodina.DisplayMember = "naziv";

            cmbOsoba.DataSource = osoba;
            cmbOsoba.ValueMember = "id";
            cmbOsoba.DisplayMember = "Ime";

            cmbNivo.DataSource = nivo;
            cmbNivo.ValueMember = "id";
            cmbNivo.DisplayMember = "naziv";

            if (raspodela.Rows.Count == 0)
            {
                cmbGodina.SelectedValue = -1;
                cmbOsoba.SelectedValue = -1;
                cmbNivo.SelectedValue = -1;
                foreach (Control c in Controls)
                {
                    Button b = c as Button;
                    if (b != null)
                    {
                        b.Enabled = false;
                    }
                }
                insert.Enabled = true;

            }
            else
            {
                cmbGodina.SelectedValue = raspodela.Rows[br]["idGodine"];
                cmbOsoba.SelectedValue = raspodela.Rows[br]["idOsobe"];
                cmbNivo.SelectedValue = raspodela.Rows[br]["idNivoa"];

                levlje.Enabled = !(br == 0);
                levo.Enabled = !(br == 0);
                desnje.Enabled = !(br == raspodela.Rows.Count - 1);
                desno.Enabled = !(br == raspodela.Rows.Count - 1);
                komanda = $"select ime + prezime as Ime, brojBodova as Skor, ocena as Ocena from rezultat join raspodela on raspodela.id = rezultat.idRaspodele join osoba on osoba.id = raspodela.idOsobe join godina on godina.id = raspodela.idGodine join nivoTakmicenja on nivoTakmicenja.id = raspodela.idNivoa where raspodela.id = {raspodela.Rows[br]["id"].ToString()}";
                adapter = new SqlDataAdapter(komanda, veza);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }
        private void Raspodela_Load(object sender, EventArgs e)
        {
            Zaucitaj();
            FeelingTheCombos();
            
        }

        private void levlje_Click(object sender, EventArgs e)
        {
            br = 0;
            FeelingTheCombos();
        }

        private void levo_Click(object sender, EventArgs e)
        {
            br--;
            FeelingTheCombos();
        }

        private void desno_Click(object sender, EventArgs e)
        {
            br++;
            FeelingTheCombos();
        }

        private void desnje_Click(object sender, EventArgs e)
        {
            br=raspodela.Rows.Count - 1;
            FeelingTheCombos();
        }

        private void update_Click(object sender, EventArgs e)
        {
            komanda = $"update raspodela set idOsobe= {cmbOsoba.SelectedValue.ToString()}, idNivoa= {cmbNivo.SelectedValue.ToString()}, idGodine={cmbGodina.SelectedValue.ToString()} where id = {raspodela.Rows[br]["id"].ToString()}";
            SqlCommand comm = new SqlCommand(komanda, veza);
            try
            {
                veza.Open();
                comm.ExecuteNonQuery();
                veza.Close();
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message);
                throw;
            }
            Zaucitaj();
            FeelingTheCombos();
        }

        private void insert_Click(object sender, EventArgs e)
        {
            komanda = $"insert into raspodela (idOsobe, idNivoa, idGodine) values ({cmbOsoba.SelectedValue.ToString()}, {cmbNivo.SelectedValue.ToString()}, {cmbGodina.SelectedValue.ToString()})";
            SqlCommand comm = new SqlCommand(komanda, veza);
            try
            {
                veza.Open();
                comm.ExecuteNonQuery();
                veza.Close();
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message);
                throw;
            }
            Zaucitaj();
            FeelingTheCombos();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            komanda = $"delete from raspodela where id = {raspodela.Rows[br]["id"].ToString()}";
            SqlCommand comm = new SqlCommand(komanda, veza);
            bool brisano = false;
            try
            {
                veza.Open();
                comm.ExecuteNonQuery();
                veza.Close();
                brisano = true;
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message);
                throw;
            }
            if (brisano)
            {
                br--;
            }
            Zaucitaj();
            FeelingTheCombos();
        }
    }
}
