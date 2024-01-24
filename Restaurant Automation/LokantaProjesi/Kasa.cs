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
    public partial class Kasa : Form
    {
        public Kasa()
        {
            InitializeComponent();
        }
        RNYC rd = new RNYC();
        public void MasaGoster() {
                     rd.cm = new OleDbCommand("select count(*) from masalar", rd.baglanti);
                     int m_sayisi=Convert.ToInt32(rd.cm.ExecuteScalar());
                     rd.cm = new OleDbCommand("Select * From masalar order by m_id ", rd.baglanti);
                     rd.dr = rd.cm.ExecuteReader();
                     int satir=0, sutun=0;
                     while (rd.dr.Read())
                     {
                        
                         if (sutun % 5 == 0)
                         {
                             satir++;
                             sutun = 0;
                         }
                     Button b = new Button();
                     b.Location = new System.Drawing.Point(5+sutun * 50,20+ satir * 35);
                     b.Size = new System.Drawing.Size(35, 30);

                     b.Text =rd.dr["m_id"].ToString();
                     
                     if (rd.dr["m_durum"].ToString() == "boş")
                         b.BackColor = Color.Green;
                     if(rd.dr["m_durum"].ToString()=="dolu")
                         b.BackColor = Color.PaleVioletRed;
                     if (rd.dr["m_tipi"].ToString() == "paket" && rd.dr["m_durum"].ToString()=="boş")
                         b.BackColor = Color.LightBlue;
                     if (rd.dr["m_tipi"].ToString() == "paket" && rd.dr["m_durum"].ToString() == "dolu")
                         b.BackColor = Color.DarkOrange;
                     groupBox1.Controls.Add(b);
                     b.Click += new EventHandler(b_Click);
                     sutun++;
                     }
                   //b.Click diyerek tıklama özelliğine bir event tanımladık.
        }
        private void Kasa_Load(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            MasaGoster();
            rd.baglanti.Close();

            
        }
        int m_num;
        void b_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(((Button)sender).Text + "  tıklandı");
            rd.baglanti.Open();
            string s=((Button)sender).Text.Trim();
            rd.cm = new OleDbCommand("select m_durum,m_tipi from masalar where m_id="+int.Parse(s)+"", rd.baglanti);
            rd.dr = rd.cm.ExecuteReader();
            while (rd.dr.Read())
            {
                if (rd.dr["m_durum"].ToString()=="boş" && rd.dr["m_tipi"].ToString()=="restaurant")
                    ((Button)sender).BackColor = Color.Green;
                else if (rd.dr["m_durum"].ToString()=="boş" && rd.dr["m_tipi"].ToString()=="paket")
                    ((Button)sender).BackColor = Color.LightBlue;
            }
            
            int a = Convert.ToInt32(s);
            m_num = a;
            masa_no = m_num;
            top_hesap = 0;
            rd.baglanti.Close();
            HesapCek(a);
        }
       public static int top_hesap = 0;
        
        public double HesapCek(int masa_no) {
            OleDbCommand cmr = new OleDbCommand();
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("Select y_isim, y_fiyat,y_id from yiyecek where y_id in (Select urun_id from siparis where urun_turu='yiyecek' and s_masa_id='" + m_num + "')", rd.baglanti);
            rd.dr = rd.cm.ExecuteReader();
            while (rd.dr.Read())
            {
                cmr = new OleDbCommand("Select count(*) from siparis where urun_turu='yiyecek' and urun_id=" + Convert.ToInt32(rd.dr["y_id"]) + " and s_masa_id='" + m_num + "'", rd.baglanti);
                top_hesap += Convert.ToInt32(rd.dr["y_fiyat"]) * Convert.ToInt32(cmr.ExecuteScalar());
            }

            OleDbCommand cm2 = new OleDbCommand("Select t_isim,t_id, t_fiyat from tatli where t_id in (Select urun_id from siparis where urun_turu='tatli' and s_masa_id='" + m_num + "')", rd.baglanti);
            OleDbDataReader dr2 = cm2.ExecuteReader();
            while (dr2.Read())
            {
                cmr = new OleDbCommand("Select count(*) from siparis where urun_turu='tatli' and urun_id=" + Convert.ToInt32(dr2["t_id"]) + " and s_masa_id='" + m_num + "'", rd.baglanti);
                top_hesap += Convert.ToInt32(dr2["t_fiyat"]) * Convert.ToInt32(cmr.ExecuteScalar());
            }

            OleDbCommand cm3 = new OleDbCommand("Select i_id,i_isim, i_fiyat from icecekler where i_id in (Select urun_id from siparis where urun_turu='icecek' and s_masa_id='" + m_num + "')", rd.baglanti);
            OleDbDataReader dr3 = cm3.ExecuteReader();
            while (dr3.Read())
            {
                cmr = new OleDbCommand("Select count(*) from siparis where urun_turu='icecek' and urun_id=" + Convert.ToInt32(dr3["i_id"]) + " and s_masa_id='" + m_num + "'", rd.baglanti);
                top_hesap += Convert.ToInt32(dr3["i_fiyat"]) * Convert.ToInt32(cmr.ExecuteScalar());
            }
            rd.baglanti.Close();
            textBox1.Text = "" + top_hesap;
            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rd.baglanti.Open();
           // rd.cm = new OleDbCommand("Delete From paket_siparis where s_no in (Select s_id From siparis where s_masa_id in (Select m_bir_no From masalar where m_id="+m_num+"))", rd.baglanti);
           // rd.cm.ExecuteNonQuery();
           // rd.cm.Dispose();
            rd.cm = new OleDbCommand("Delete From siparis where s_masa_id in (Select m_bir_no From masalar where m_id="+m_num+")", rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            rd.cm = new OleDbCommand("Update masalar SET m_durum='boş' , m_bir_no='0' where m_id=" + m_num + "", rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            rd.cm = new OleDbCommand("Update masalar SET m_durum='boş' , m_bir_no='0' where m_bir_no='" + m_num.ToString() + "'", rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            string z = dateTimePicker1.Value.ToShortDateString();
            rd.cm = new OleDbCommand("INSERT Into kasa (islem_zaman,islem_tutar) VALUES('"+z+"',"+int.Parse(textBox1.Text.Trim())+")",rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            //rd.cm = new OleDbCommand("Delete From paket_siparis where s_no in (Select s_id From siparis where s_masa_id='"+m_num.ToString()+"')", rd.baglanti);
           // rd.cm.ExecuteNonQuery();
          //  rd.cm.Dispose();
            rd.cm = new OleDbCommand("Delete From siparis where s_masa_id='"+m_num.ToString()+"'", rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            rd.baglanti.Close();
            MessageBox.Show("Hesap Alındı Masa durumunu güncellemek için hesap aldığınız masaya tıklayınız...","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            
          
        }
        public static int masa_no;
        private void button2_Click(object sender, EventArgs e)
        {
           // masa_no = m_num;
            button1.Enabled = true;
            Form1 f1 = new Form1();
            f1.ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Paket_Siparis ps = new Paket_Siparis();
            ps.ShowDialog();
        }

        private void Kasa_FormClosed(object sender, FormClosedEventArgs e)
        {
            GiriseDon gd = new GiriseDon();
            gd.FormKapanma();
        }
       
    }
}
