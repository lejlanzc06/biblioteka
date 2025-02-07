using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bibliotekaa
{
    public partial class PovratKnjige : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KTQ5MSB;Initial Catalog=Školska_biblioteka;Persist Security Info=True;User ID=Lejla;Password=neziclejla3101");

        public PovratKnjige()
        {
            InitializeComponent();
        }

        private void PovratKnjige_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(textBox2.Text, out var brojClana))
                {
                    MessageBox.Show("Unesite validan broj člana.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var cmd = new SqlCommand(@"SELECT p.New_ID, k.Naslov, p.Datum_Posudbe, 
                                                  kor.Prezime_i_Ime
                                            FROM Posudba p 
                                            INNER JOIN Knjiga k ON p.Inventarni_broj = k.Inventarni_broj 
                                            INNER JOIN Korisnik kor ON p.Broj_Člana = kor.Broj_Člana 
                                            WHERE p.Broj_Člana = @BrojClana AND p.Datum_Vraćanja IS NULL", con))
                {
                    cmd.Parameters.AddWithValue("@BrojClana", brojClana);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do greške: {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Unesite validan New_ID.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dateTimePicker1.Value == null)
                {
                    MessageBox.Show("Izaberite datum vraćanja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newId = Convert.ToInt32(textBox1.Text);
                var datumVracanja = dateTimePicker1.Value;

                using (var cmd = new SqlCommand("sp_VratiKnjigu", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@New_ID", newId);
                    cmd.Parameters.AddWithValue("@Datum_Vraćanja", datumVracanja);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Knjiga je uspješno vraćena.", "Uspjeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                button1_Click(null, null); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do greške: {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
