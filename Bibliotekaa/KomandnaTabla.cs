using Bibliotekaa.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bibliotekaa
{
    public partial class KomandnaTabla : Form
    {
        public KomandnaTabla()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DodatiKnjigu dk = new DodatiKnjigu();
            dk.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PregledKnjiga pk = new PregledKnjiga();
            pk.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PosudbaKnjige pk = new PosudbaKnjige();
            pk.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IzvještajPosuđenihKnjiga i = new IzvještajPosuđenihKnjiga();
            i.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DodatiKorisnika dk = new DodatiKorisnika();
            dk.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PregledKorisnika pk = new PregledKorisnika();
            pk.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PovratKnjige pv = new PovratKnjige();
            pv.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            IzvještajVraćenihKnjiga ii = new IzvještajVraćenihKnjiga();
            ii.Show();
        }

        private void KomandnaTabla_Load(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            IzmjenaPodataka ip = new IzmjenaPodataka();
            ip.Show();
        }
    }
}
