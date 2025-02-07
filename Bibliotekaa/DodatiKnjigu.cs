using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Bibliotekaa
{
    public partial class DodatiKnjigu : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KTQ5MSB;Initial Catalog=Školska_biblioteka;Persist Security Info=True;User ID=Lejla;Password=neziclejla3101");

        public DodatiKnjigu()
        {
            InitializeComponent();
        }

        private void DodatiKnjigu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_dodati_knjigu", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Inventarni_broj", SqlDbType.Int).Value = Convert.ToInt32(textBox1.Text);
                cmd.Parameters.Add("@Signatura", SqlDbType.NVarChar).Value = textBox2.Text;
                cmd.Parameters.Add("@Autor", SqlDbType.NVarChar).Value = textBox3.Text;
                cmd.Parameters.Add("@Naslov", SqlDbType.NVarChar).Value = textBox4.Text;
                cmd.Parameters.Add("@Izdavač", SqlDbType.NVarChar).Value = textBox5.Text;
                cmd.Parameters.Add("Izdanje", SqlDbType.NVarChar).Value = textBox6.Text;
                cmd.Parameters.Add("@Godina_izdanja", SqlDbType.Int).Value = dateTimePicker1.Value.Year;
                cmd.Parameters.Add("@Mjesto_izdanja", SqlDbType.NVarChar).Value = textBox8.Text;
                cmd.Parameters.Add("@Broj_stranica", SqlDbType.Int).Value = Convert.ToInt32(textBox9.Text);
                cmd.Parameters.Add("@Format", SqlDbType.NVarChar).Value = textBox10.Text;
                cmd.Parameters.Add("@Napomena", SqlDbType.NVarChar).Value = textBox11.Text;
                string imagePath = textBox12.Text;

                if (File.Exists(imagePath))
                {
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    cmd.Parameters.Add("@Slika", SqlDbType.VarBinary).Value = imageBytes;
                }
                else
                {
                    cmd.Parameters.Add("@Slika", SqlDbType.VarBinary).Value = DBNull.Value;
                }

                cmd.ExecuteNonQuery();
                MessageBox.Show("Knjiga je uspješno dodana.");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške: " + ex.Message);
            }
        }
    }
}
