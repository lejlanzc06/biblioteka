using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bibliotekaa
{
    public partial class PregledKnjiga : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KTQ5MSB;Initial Catalog=Školska_biblioteka;Persist Security Info=True;User ID=Lejla;Password=neziclejla3101");

        public PregledKnjiga()
        {
            InitializeComponent();
        }

        private void PregledKnjiga_Load(object sender, EventArgs e)
        {
            UcitajSveKnjige();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FiltrirajKnjigePoNaslovu();
        }

        private void UcitajSveKnjige()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Pregled_Knjiga", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Naslov", SqlDbType.NVarChar).Value = DBNull.Value;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške: " + ex.Message);
            }
        }

        private void FiltrirajKnjigePoNaslovu()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Pregled_Knjiga", con);
                cmd.CommandType = CommandType.StoredProcedure;

                string naslov = textBox1.Text;

                if (string.IsNullOrEmpty(naslov))
                {
                    UcitajSveKnjige();
                    return;
                }

                cmd.Parameters.Add("@Naslov", SqlDbType.NVarChar).Value = naslov;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške: " + ex.Message);
            }
        }
    }
}
