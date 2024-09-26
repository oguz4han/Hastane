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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }


        SqlBaglantisi bgl = new SqlBaglantisi();
        public string tc;

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;

            //ad soyad çekmek
            SqlCommand komut1 = new SqlCommand("select Ad,Soyad from Doktorlar where Tc=@p1",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",lblTc.Text);

            SqlDataReader dr = komut1.ExecuteReader();

            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0]+" "+dr[1];
            }
            bgl.baglanti().Close();


            // randevuları çekmek
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Randevular where Doktor='"+lblAdSoyad.Text+"'", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle frm = new FrmDoktorBilgiDuzenle();
            frm.tc = lblTc.Text;
            frm.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen  = dataGridView1.SelectedCells[0].RowIndex;
            rchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
