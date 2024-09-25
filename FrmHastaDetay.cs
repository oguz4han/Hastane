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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        public string tc;

        SqlBaglantisi bgl = new SqlBaglantisi();


        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;
            // Ad soyad Çekme
            // Ad soyad Çekme
            // Ad soyad Çekme
            SqlCommand komut = new SqlCommand("select Ad,Soyad from Hastalar where TC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            // randevu geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Randevular Where TC="+tc,bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            // branşları çekme
            SqlCommand komut2 = new SqlCommand("select Ad from Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();


            
            

        }
        // Doktor bilgileri çekme
        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("Select Ad,Soyad from Doktorlar where Brans=@p1 ",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close(); 
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Randevular where Brans='"+cmbBrans.Text+"'",bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;  


        }

        private void lnkBilgileriDüzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle frmBilgiDuzenle = new FrmBilgiDuzenle();
            frmBilgiDuzenle.tcNo = lblTc.Text;
            frmBilgiDuzenle.Show();
            
        }
    }
}
