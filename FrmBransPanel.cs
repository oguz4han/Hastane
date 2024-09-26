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
    public partial class FrmBransPanel : Form
    {


        SqlBaglantisi bgl = new SqlBaglantisi();

        public FrmBransPanel()
        {
            InitializeComponent();
        }

        private void FrmBransPanel_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Branslar", bgl.baglanti());

            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Branslar (Ad) values (@b1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@b1", txtAd.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Eklendi...");

        }

        //tabloya verileri tek tıkla txtBox lara aktarmak
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Branslar where Id=@b1 ",bgl.baglanti());
            komut.Parameters.AddWithValue("@b1",txtId.Text);    
            komut.ExecuteNonQuery();
            bgl.baglanti().Close(); 


        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Branslar set Ad=@p1 where Id=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Güncelleme yapıldı...");
        }
    }
}
