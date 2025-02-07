using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bibliotekaa
{
    public partial class PosudbaKnjige : Form
    {
        public PosudbaKnjige()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KTQ5MSB;Initial Catalog=Školska_biblioteka;Persist Security Info=True;User ID=Lejla;Password=neziclejla3101");

        private void PosudbaKnjige_Load(object sender, EventArgs e)
        {
            // Ovdje možeš učitati naslove iz baze u comboBox1
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Naslov FROM Knjiga", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["Naslov"].ToString());
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom učitavanja naslova: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string brojClana = textBox3.Text;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Prezime_i_Ime, Broj_Člana FROM Korisnik WHERE Broj_Člana = @Broj_Člana", con);
                cmd.Parameters.AddWithValue("@Broj_Člana", brojClana);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    textBox1.Text = dr["Prezime_i_Ime"].ToString();
                    textBox2.Text = dr["Broj_Člana"].ToString();
                }
                else
                {
                    MessageBox.Show("Korisnik sa ovim brojem člana nije pronađen.");
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom pretrage korisnika: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string brojClana = textBox3.Text;
            string inventarniBroj = textBox5.Text;
            DateTime datumPosudbe = dateTimePicker1.Value;
            DateTime rokVracanja = dateTimePicker2.Value;
            string prezimeIme = textBox1.Text;
            string naslov = comboBox1.SelectedItem.ToString();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Dodaj_knjigu_za_posudbu", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Broj_Člana", brojClana);
                cmd.Parameters.AddWithValue("@Inventarni_broj", inventarniBroj);
                cmd.Parameters.AddWithValue("@Datum_Posudbe", datumPosudbe);
                cmd.Parameters.AddWithValue("@Datum_Vraćanja", rokVracanja);

                SqlParameter paramPrezimeIme = new SqlParameter("@Prezime_i_Ime", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramPrezimeIme);

                SqlParameter paramNaslov = new SqlParameter("@Naslov", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramNaslov);

                cmd.ExecuteNonQuery();

                string prezimeImeOutput = paramPrezimeIme.Value.ToString();
                string naslovOutput = paramNaslov.Value.ToString();

                MessageBox.Show("Knjiga uspješno posuđena.\n" +
                                "Korisnik: " + prezimeImeOutput + "\n" +
                                "Naslov: " + naslovOutput);

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom posudbe knjige: " + ex.Message);
            }
        }
    }
}
