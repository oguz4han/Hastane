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

namespace Hastane
{
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmDoktorGiris_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select Tc,Sifre from Doktorlar where Tc=@p1 and Sifre=@p2 ",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",mskTc.Text);
            komut.Parameters.AddWithValue("@p2",txtSifre.Text);

            SqlDataReader rdr = komut.ExecuteReader();
            if(rdr.Read())
            {
                FrmDoktorDetay frm = new FrmDoktorDetay();
                frm.tc = mskTc.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı tc veya şifre...");
            }

        }
    }
}
