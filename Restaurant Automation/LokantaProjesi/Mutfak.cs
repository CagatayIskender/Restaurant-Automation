using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace LokantaProjesi
{
    public partial class Mutfak : Form
    {
        public Mutfak()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=lokanta.accdb");
        OleDbCommand cmd;
        OleDbDataAdapter da;
        OleDbDataReader dr;
        public void dg_guncelle()
        {
            con.Open();
            DataTable dt = new DataTable();
            da = new OleDbDataAdapter("Select s_id , urun_id, durum from siparis where durum='hazırlanıyor'", con);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            da.Dispose();
            con.Close();

        }
        private void Mutfak_Load(object sender, EventArgs e)
        {
            dg_guncelle();
            dataGridView1.Columns[0].HeaderText = "Sipariş No";
            dataGridView1.Columns[1].HeaderText = "Ürün No";
            dataGridView1.Columns[2].HeaderText = "Durum";
        }
        RNYC rd=new RNYC();
        private void Mutfak_FormClosed(object sender, FormClosedEventArgs e)
        {
            GiriseDon kp = new GiriseDon();
            kp.FormKapanma();
        }
        int y_id, t_id;
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cm3 ;
            OleDbCommand cmr2;
            cmr2 = new OleDbCommand("Select y_id from yiyecek where y_isim='"+textBox1.Text+"'",con);
            string yemek_idsi=cmr2.ExecuteScalar().ToString();
            OleDbCommand cmr = new OleDbCommand("Select malzeme_id,malzeme_miktar from yemek_malzeme where yemek_id ='"+yemek_idsi+"'",con);
            OleDbDataReader drr = cmr.ExecuteReader();
            while (drr.Read())
            {   
                int m_m = Convert.ToInt32(drr["malzeme_miktar"]);
                int m_id=Convert.ToInt32(drr["malzeme_id"]);
                cm3 = new OleDbCommand("Select m_stok from stok where m_id="+m_id+"",con);
                int m_eski_stok = Convert.ToInt32(cm3.ExecuteScalar());
                m_eski_stok -= m_m;
                cmr2 = new OleDbCommand("Update stok set m_stok="+m_eski_stok+" where m_id="+m_id+"",con);
                cmr2.ExecuteNonQuery();
                cmr2.Dispose();
            }
            cmd = new OleDbCommand("Update siparis set durum ='Hazır'where s_id =" + y_id + "", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
           
            cmd = new OleDbCommand("Insert into bildirimler (b_alan_id,b_bildirim) values (" + 0 + ",' "+textBox1.Text+" Hazır')", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            dg_guncelle();
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            y_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            con.Close();
            con.Open();
            OleDbCommand cmr = new OleDbCommand("Select y_isim from yiyecek where y_id in (Select urun_id from siparis where urun_turu='yiyecek' and s_id =" + y_id + ")", con);
            textBox1.Text = "" + cmr.ExecuteScalar().ToString();
            con.Close();
        }
    }
}
