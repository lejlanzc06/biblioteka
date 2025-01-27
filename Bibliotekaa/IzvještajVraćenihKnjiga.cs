using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bibliotekaa
{
    public partial class IzvještajVraćenihKnjiga : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KTQ5MSB;Initial Catalog=Školska_biblioteka;Persist Security Info=True;User ID=Lejla;Password=neziclejla3101");

        public IzvještajVraćenihKnjiga()
        {
            InitializeComponent();
        }

        private void IzvještajVraćenihKnjiga_Load(object sender, EventArgs e)
        {
            LoadVraćeneKnjige();
        }

        private void LoadVraćeneKnjige()
        {
            try
            {
                using (var cmd = new SqlCommand(@"SELECT 
                                                    p.New_ID, 
                                                    p.Broj_Člana, 
                                                    k.Naslov, 
                                                    kor.Prezime_i_Ime, 
                                                    p.Datum_Posudbe, 
                                                    p.Datum_Vraćanja
                                                FROM 
                                                    Posudba p
                                                JOIN 
                                                    Knjiga k ON p.Inventarni_broj = k.Inventarni_broj
                                                JOIN 
                                                    Korisnik kor ON p.Broj_Člana = kor.Broj_Člana
                                                WHERE 
                                                    p.Datum_Vraćanja IS NOT NULL
                                                ORDER BY 
                                                    p.Datum_Vraćanja", con))
                {
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
    }
}
