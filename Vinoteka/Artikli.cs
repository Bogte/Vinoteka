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

namespace Vinoteka
{
    public partial class Artikli : Form
    {
        DataTable podaci;
        SqlCommand menjanja;

        public Artikli()
        {
            InitializeComponent();
        }

        private void Artikli_Load(object sender, EventArgs e)
        {
            Osvezi();
        }

        private void Osvezi()
        {
            podaci = new DataTable();
            podaci = Konekcija.Unos("SELECT id, naziv, kolicina, cena, convert(varchar(10), godina_proizvodnje) AS 'Godina proizvodnje' FROM Artikli");
            dataGridView1.DataSource = podaci;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int indeks = dataGridView1.CurrentRow.Index;

                textBox1.Text = Convert.ToString(dataGridView1.Rows[indeks].Cells["id"].Value);
                textBox2.Text = Convert.ToString(dataGridView1.Rows[indeks].Cells["naziv"].Value);
                textBox3.Text = Convert.ToString(dataGridView1.Rows[indeks].Cells["kolicina"].Value);
                textBox4.Text = Convert.ToString(dataGridView1.Rows[indeks].Cells["cena"].Value);
                textBox5.Text = Convert.ToString(dataGridView1.Rows[indeks].Cells["Godina proizvodnje"].Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Da li ste sigurni da zelite da obrisete ove podatake?", "Vinoteka", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    menjanja = new SqlCommand();
                    menjanja.CommandText = ("DELETE FROM Artikli WHERE id = " + textBox1.Text);

                    SqlConnection con = new SqlConnection(Konekcija.Veza());
                    con.Open();
                    menjanja.Connection = con;
                    menjanja.ExecuteNonQuery();
                    con.Close();

                    Osvezi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ne mozete da obrisete ove podatake, druge tabele zahtevaju ove podatake! - " + ex.Source, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SqlConnection con = new SqlConnection(Konekcija.Veza());
                con.Close();
                Osvezi();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Da li ste sigurni da zelite da izmenite ove podatke?", "Vinoteka", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                    {
                        MessageBox.Show("Sva polja moraju biti popunjena - Vinoteka", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Osvezi();
                        return;
                    }

                    podaci = new DataTable();
                    podaci = Konekcija.Unos("SELECT * FROM Artikli WHERE naziv = '" + textBox2.Text + "' AND kolicina = '" + textBox3.Text + "' AND cena = " + textBox4.Text + " AND godina_proizvodnje = '" + textBox5.Text + "'");
                    if (podaci.Rows.Count >= 1) throw new Exception();

                    menjanja = new SqlCommand();
                    menjanja.CommandText = ("UPDATE Artikli SET naziv = '" + textBox2.Text + "' WHERE id = " + textBox1.Text +
                        " UPDATE Artikli SET kolicina = '" + textBox3.Text + "' WHERE id = " + textBox1.Text +
                        " UPDATE Artikli SET cena = " + textBox4.Text + " WHERE id = " + textBox1.Text +
                        " UPDATE Artikli SET godina_proizvodnje = '" + textBox5.Text + "' WHERE id = " + textBox1.Text);

                    SqlConnection con = new SqlConnection(Konekcija.Veza());
                    con.Open();
                    menjanja.Connection = con;
                    menjanja.ExecuteNonQuery();
                    con.Close();

                    Osvezi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Podatak vec postoji u tabeli - " + ex.Source, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SqlConnection con = new SqlConnection(Konekcija.Veza());
                con.Close();
                Osvezi();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Da li ste sigurni da zelite da dodate ove podatke?", "Vinoteka", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                    {
                        MessageBox.Show("Sva polja moraju biti popunjena - Vinoteka", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Osvezi();
                        return;
                    }

                    podaci = new DataTable();
                    podaci = Konekcija.Unos("SELECT * FROM Artikli WHERE naziv = '" + textBox2.Text + "' AND kolicina = '" + textBox3.Text + "' AND cena = " + textBox4.Text + " AND godina_proizvodnje = '" + textBox5.Text + "'");
                    if (podaci.Rows.Count >= 1) throw new Exception();


                    menjanja = new SqlCommand();
                    menjanja.CommandText = ("INSERT INTO Artikli VALUES ('" + textBox2.Text + "', '" + textBox3.Text + "', " + textBox4.Text + ", '" + textBox5.Text + "')");
                    SqlConnection con = new SqlConnection(Konekcija.Veza());
                    con.Open();
                    menjanja.Connection = con;
                    menjanja.ExecuteNonQuery();
                    con.Close();

                    Osvezi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ne mozete da dodate vec postojece podatke! - " + ex.Source, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SqlConnection con = new SqlConnection(Konekcija.Veza());
                con.Close();
                Osvezi();
            }
        }
    }
}
