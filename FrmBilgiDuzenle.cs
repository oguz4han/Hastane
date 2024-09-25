using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane
{
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }

        public string tcNo;

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = tcNo;
            mskTC.Enabled = false;
            SqlCommand komut = new SqlCommand("select * from Hastalar where Tc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",mskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                mskTelefon.Text = dr[4].ToString();
                txtSifre.Text = dr[5].ToString();
                cmbCinsiyet.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update Hastalar set Ad=@p1,Soyad=@p2,Telefon=@p3,Sifre=@p4,Cinsiyet=@p5 where Tc=@p6 ",bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1",txtAd.Text);
            komut2.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3",mskTelefon.Text);
            komut2.Parameters.AddWithValue("@p4",txtSifre.Text);
            komut2.Parameters.AddWithValue("@p5",cmbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6",mskTC.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi","Bilgi");
        }
    }
}
