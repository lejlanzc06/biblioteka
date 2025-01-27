using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bibliotekaa
{
    public partial class IzvještajPosuđenihKnjiga : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KTQ5MSB;Initial Catalog=Školska_biblioteka;Persist Security Info=True;User ID=Lejla;Password=neziclejla3101");

        public IzvještajPosuđenihKnjiga()
        {
            InitializeComponent();
        }

        private void IzvještajPosuđenihKnjiga_Load(object sender, EventArgs e)
        {
            LoadPosudbe();
        }

        private void LoadPosudbe()
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
                                                ORDER BY 
                                                    p.Datum_Posudbe", con))
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
