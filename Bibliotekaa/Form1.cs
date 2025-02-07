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

namespace Bibliotekaa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KTQ5MSB;Initial Catalog=Školska_biblioteka;Persist Security Info=True;User ID=Lejla;Password=neziclejla3101");

        private void button1_Click(object sender, EventArgs e)
        {

            {

                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_prijava1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = textBox1.Text;
                cmd.Parameters.Add("@Lozinka", SqlDbType.NVarChar).Value = textBox2.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    KomandnaTabla kt = new KomandnaTabla();
                    kt.Show();

                }
                else
                {

                    MessageBox.Show("Prijava nije uspjela");
                }
                con.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
