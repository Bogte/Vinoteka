using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Vinoteka
{
    public partial class Lager : Form
    {
        DataTable podaci;
        SqlCommand menjanja;
        int indekss = 0;
        decimal cena = 0;

        public Lager()
        {
            InitializeComponent();
        }

        private void Lager_Load(object sender, EventArgs e)
        {
            podaci = new DataTable();//Dodavanje vina
            podaci = Konekcija.Unos("SELECT DISTINCT naziv AS 'Autor' FROM Artikli");
            string[] pomocna = new string[podaci.Rows.Count];
            for (int i = 0; i < podaci.Rows.Count; i++)
            {
                pomocna[i] = Convert.ToString(podaci.Rows[i]["Autor"]);
                comboBox3.Items.Add(pomocna[i]);
            }

            podaci = new DataTable();//Dodavanje vina
            podaci = Konekcija.Unos("SELECT DISTINCT naziv AS 'Autor' FROM Artikli");
            pomocna = new string[podaci.Rows.Count];
            for (int i = 0; i < podaci.Rows.Count; i++)
            {
                pomocna[i] = Convert.ToString(podaci.Rows[i]["Autor"]);
                comboBox4.Items.Add(pomocna[i]);
            }

            Osvezi();
        }

        private void Osvezi()
        {
            podaci = new DataTable();
            podaci = Konekcija.Unos("SELECT Lager.id, Artikli.naziv, Artikli.kolicina, Artikli.cena, convert(varchar(10), godina_proizvodnje) AS 'Godina proizvodnje', opis FROM Lager JOIN Artikli ON Artikli.id = Lager.id_artikla");
            dataGridView1.DataSource = podaci;

            podaci = Konekcija.Unos("SELECT COUNT(id) FROM Lager WHERE Opis = 'Nije ostecena'");
            textBox6.Text = Convert.ToString(podaci.Rows[0][0]);

            podaci = Konekcija.Unos("SELECT COUNT(id) FROM Lager WHERE Opis = 'Ostecena'");
            textBox7.Text = Convert.ToString(podaci.Rows[0][0]);

            podaci = Konekcija.Unos("SELECT COUNT(id) FROM Lager WHERE Opis = 'Ukradena'");
            textBox8.Text = Convert.ToString(podaci.Rows[0][0]);
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int indeks = dataGridView1.CurrentRow.Index;

                textBox1.Text = Convert.ToString(dataGridView1.Rows[indeks].Cells["id"].Value);
                comboBox3.Text = Convert.ToString(dataGridView1.Rows[indeks].Cells["naziv"].Value);
                textBox3.Text = Convert.ToString(dataGridView1.Rows[indeks].Cells["kolicina"].Value);
                textBox4.Text = Convert.ToString(dataGridView1.Rows[indeks].Cells["cena"].Value);
                textBox5.Text = Convert.ToString(dataGridView1.Rows[indeks].Cells["Godina proizvodnje"].Value);
                comboBox1.Text = Convert.ToString(dataGridView1.Rows[indeks].Cells["opis"].Value);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Da li ste sigurni da zelite da izmenite ove podatke?", "Vinoteka", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (comboBox3.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "")
                    {
                        MessageBox.Show("Sva polja moraju biti popunjena - Vinoteka", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Osvezi();
                        return;
                    }

                    //Trazenje id-a za artikal
                    podaci = new DataTable();
                    podaci = Konekcija.Unos("SELECT id FROM Artikli WHERE naziv = '" + comboBox3.Text + "' AND kolicina = '" + textBox3.Text + "' AND cena = " + textBox4.Text + " AND godina_proizvodnje = '" + textBox5.Text + "'");
                    int id_artikla = (int)podaci.Rows[0][0];

                    menjanja = new SqlCommand();
                    menjanja.CommandText = ("UPDATE Lager SET id_artikla = '" + id_artikla + "' WHERE id = " + textBox1.Text +
                        " UPDATE Lager SET opis = '" + comboBox1.Text + "' WHERE id = " + textBox1.Text);

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Da li ste sigurni da zelite da obrisete ove podatake?", "Vinoteka", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    menjanja = new SqlCommand();
                    menjanja.CommandText = ("DELETE FROM Lager WHERE id = " + textBox1.Text);

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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == 1) 
            {
                podaci = new DataTable();
                podaci = Konekcija.Unos("SELECT Lager.id, Artikli.naziv, Artikli.kolicina, Artikli.cena, convert(varchar(10), godina_proizvodnje) AS 'Godina proizvodnje', opis FROM Lager JOIN Artikli ON Artikli.id = Lager.id_artikla WHERE Opis = 'Nije ostecena'");
                dataGridView3.DataSource = podaci;
            }
            else if (comboBox2.SelectedIndex == 0) 
            {
                podaci = new DataTable();
                podaci = Konekcija.Unos("SELECT Lager.id, Artikli.naziv, Artikli.kolicina, Artikli.cena, convert(varchar(10), godina_proizvodnje) AS 'Godina proizvodnje', opis FROM Lager JOIN Artikli ON Artikli.id = Lager.id_artikla WHERE Opis = 'Ostecena'");
                dataGridView3.DataSource = podaci;
            }
            else
            {
                podaci = new DataTable();
                podaci = Konekcija.Unos("SELECT Lager.id, Artikli.naziv, Artikli.kolicina, Artikli.cena, convert(varchar(10), godina_proizvodnje) AS 'Godina proizvodnje', opis FROM Lager JOIN Artikli ON Artikli.id = Lager.id_artikla WHERE Opis = 'Ukradena'");
                dataGridView3.DataSource = podaci;
            }
        }

        private void dataGridView3_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow != null)
            {
                int indeks = dataGridView3.CurrentRow.Index;

                textBox13.Text = Convert.ToString(dataGridView3.Rows[indeks].Cells["id"].Value);
                textBox12.Text = Convert.ToString(dataGridView3.Rows[indeks].Cells["naziv"].Value);
                textBox11.Text = Convert.ToString(dataGridView3.Rows[indeks].Cells["kolicina"].Value);
                textBox10.Text = Convert.ToString(dataGridView3.Rows[indeks].Cells["cena"].Value);
                textBox9.Text = Convert.ToString(dataGridView3.Rows[indeks].Cells["Godina proizvodnje"].Value);
                comboBox2.Text = Convert.ToString(dataGridView3.Rows[indeks].Cells["opis"].Value);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Da li ste sigurni da zelite da dodate ove podatke?", "Vinoteka", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (comboBox3.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "")
                    {
                        MessageBox.Show("Sva polja moraju biti popunjena - Vinoteka", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Osvezi();
                        return;
                    }

                    //Trazenje id-a za artikal
                    podaci = new DataTable();
                    podaci = Konekcija.Unos("SELECT id FROM Artikli WHERE naziv = '" + comboBox3.Text + "' AND kolicina = '" + textBox3.Text + "' AND cena = " + textBox4.Text + " AND godina_proizvodnje = '" + textBox5.Text + "'");
                    int id_artikla = (int)podaci.Rows[0][0];


                    menjanja = new SqlCommand();
                    menjanja.CommandText = ("INSERT INTO Lager VALUES (" + id_artikla + ", '" + comboBox1.Text + "')");
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pom = comboBox3.Text;
            podaci = new DataTable();
            podaci = Konekcija.Unos("SELECT kolicina, cena, convert(varchar(10), godina_proizvodnje) FROM Artikli WHERE Naziv = '" + pom + "'");
            textBox3.Text = Convert.ToString(podaci.Rows[0][0]);
            textBox4.Text = Convert.ToString(podaci.Rows[0][1]);
            textBox5.Text = Convert.ToString(podaci.Rows[0][2]);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pom = comboBox4.Text;
            podaci = new DataTable();
            podaci = Konekcija.Unos("SELECT kolicina, cena, convert(varchar(10), godina_proizvodnje) FROM Artikli WHERE Naziv = '" + pom + "'");
            textBox15.Text = Convert.ToString(podaci.Rows[0][0]);
            textBox14.Text = Convert.ToString(podaci.Rows[0][1]);
            textBox2.Text = Convert.ToString(podaci.Rows[0][2]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox4.Text == "")
            {
                MessageBox.Show("Polja moraju biti popunjena - Vinoteka", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Osvezi();
                return;
            }

            dataGridView2.Rows.Add();
            dataGridView2.Rows[indekss].Cells["Ime_vina"].Value = comboBox4.Text;
            dataGridView2.Rows[indekss].Cells["Kolicina"].Value = textBox15.Text;
            dataGridView2.Rows[indekss].Cells["Cena"].Value = textBox14.Text;
            dataGridView2.Rows[indekss].Cells["Godina_proizvodnje"].Value = textBox2.Text;

            cena += Convert.ToDecimal(dataGridView2.Rows[indekss].Cells["Cena"].Value);
            textBox16.Text = Convert.ToString(cena);

            indekss++;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Skloni")
            {
                if (MessageBox.Show("Da li ste sigurni da zelite da sklonite ove podatake?", "Vinoteka", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(dataGridView1.Rows.Count == 0)
                    {
                        MessageBox.Show("Tabela je prazna - Vinoteka", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Osvezi();
                        return;
                    }

                    cena -= Convert.ToDecimal(dataGridView2.Rows[e.RowIndex].Cells["Cena"].Value);
                    textBox16.Text = Convert.ToString(cena);
                    dataGridView2.Rows.RemoveAt(e.RowIndex);
                    indekss--;

                    Osvezi();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
                if (MessageBox.Show("Da li ste sigurni da zelite da prodate ove podatake?", "Vinoteka", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 1; i < dataGridView2.Rows.Count; i++)
                    {
                        //Trazenje id-a za artikal
                        podaci = new DataTable();
                        podaci = Konekcija.Unos("SELECT id FROM Artikli WHERE naziv = '" + dataGridView2.Rows[i].Cells["Ime_vina"].Value + "' AND kolicina = '" + dataGridView2.Rows[i].Cells["Kolicina"].Value + "' AND cena = " + dataGridView2.Rows[i].Cells["Cena"].Value + " AND godina_proizvodnje = '" + dataGridView2.Rows[i].Cells["Godina_proizvodnje"].Value + "'");
                        int id_artikla = (int)podaci.Rows[0][0];

                        menjanja = new SqlCommand();
                        menjanja.CommandText = ("DELETE FROM Lager WHERE id_artikla = " + id_artikla + " AND opis = 'Nije ostecena'");

                        SqlConnection con = new SqlConnection(Konekcija.Veza());
                        con.Open();
                        menjanja.Connection = con;
                        menjanja.ExecuteNonQuery();
                        con.Close();

                        
                    }
                    Osvezi();
                    dataGridView2.Rows.Clear();
                    textBox16.Text = "";
                    cena = 0;
                    indekss = 0;
                }
            
        }
    }
}
