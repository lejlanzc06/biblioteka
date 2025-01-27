using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bibliotekaa
{
    public partial class PregledKorisnika : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KTQ5MSB;Initial Catalog=Školska_biblioteka;Persist Security Info=True;User ID=Lejla;Password=neziclejla3101");

        public PregledKorisnika()
        {
            InitializeComponent();
        }

        private void PregledKorisnika_Load(object sender, EventArgs e)
        {
            PrikaziSveKorisnike();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrikaziKorisnikaPoBroju();
        }

        private void PrikaziSveKorisnike()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand("Pregled_Korisnika", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Broj_Člana", SqlDbType.Int).Value = DBNull.Value;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void PrikaziKorisnikaPoBroju()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand("Pregled_Korisnika", con);
                cmd.CommandType = CommandType.StoredProcedure;

                string brojClana = textBox1.Text;

                if (string.IsNullOrEmpty(brojClana))
                {
                    PrikaziSveKorisnike();
                    return;
                }

                if (int.TryParse(brojClana, out int brojClanaInt))
                {
                    cmd.Parameters.Add("@Broj_Člana", SqlDbType.Int).Value = brojClanaInt;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Korisnik sa unetim brojem člana nije pronađen.");
                    }
                }
                else
                {
                    MessageBox.Show("Uneseni broj člana nije validan.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
