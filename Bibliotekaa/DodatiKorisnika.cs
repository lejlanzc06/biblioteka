using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bibliotekaa
{
    public partial class DodatiKorisnika : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KTQ5MSB;Initial Catalog=Školska_biblioteka;Persist Security Info=True;User ID=Lejla;Password=neziclejla3101");

        public DodatiKorisnika()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string brojClana = textBox1.Text;
            string prezimeIme = textBox2.Text;

            if (string.IsNullOrEmpty(brojClana) || string.IsNullOrEmpty(prezimeIme))
            {
                MessageBox.Show("Obavezno je unjeti broj člana i prezime i ime.");
                return;
            }

            int brojClanaInt;
            if (!int.TryParse(brojClana, out brojClanaInt))
            {
                MessageBox.Show("Broj člana mora biti broj.");
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Dodati_Korisnika", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Broj_člana", SqlDbType.Int).Value = brojClanaInt;
                cmd.Parameters.Add("@Prezime_i_Ime", SqlDbType.NVarChar).Value = prezimeIme;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Korisnik je uspješno dodat");

                textBox1.Text = "";
                textBox2.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}

