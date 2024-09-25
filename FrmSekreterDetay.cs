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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string tc;

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;

            // Ad - Soyad 
            SqlCommand komut1 = new SqlCommand("select AdSoyad from Sekreter where Tc=@p1",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",lblTc.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();  
            while(dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();


            // branş listesini görüntüle
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Ad from Branslar", bgl.baglanti());
            da.Fill(dt1);
            dataGridView2.DataSource = dt1;


            // Doktorları listeye aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select (Ad + ' ' + Soyad) as 'Ad',Brans from Doktorlar",bgl.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource= dt2;



            // branşı comboboxaa aktarma
            SqlCommand komut2 = new SqlCommand("select Ad from Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutKaydet = new SqlCommand("insert into Randevular (Tarih,Saat,Brans,Doktor) values (@r1,@r2,@r3,@r4)",bgl.baglanti());
            komutKaydet.Parameters.AddWithValue("@r1",mskTarih.Text);
            komutKaydet.Parameters.AddWithValue("@r2",mskSaat.Text);
            komutKaydet.Parameters.AddWithValue("@r3",cmbBrans.Text);
            komutKaydet.Parameters.AddWithValue("@r4",cmbDoktor.Text);

            komutKaydet.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Randevu oluşturuldu...");

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();

            SqlCommand komut = new SqlCommand("select Ad, Soyad from Doktorlar where Brans=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",cmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();   
            while(dr.Read())
            {
                cmbDoktor.Items.Add(dr[0]+" " + dr[1]);
            }

            bgl.baglanti().Close(); 


        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Duyurular (duyuru) values (@d1) ",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1",rchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Duyuru oluşturuldu.");

        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();
        }
    }
}
