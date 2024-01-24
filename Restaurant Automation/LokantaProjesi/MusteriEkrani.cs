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
    public partial class MusteriEkrani : Form
    {
        public MusteriEkrani()
        {
            InitializeComponent();
        }
        RNYC rd = new RNYC();
        private void MusteriEkrani_Load(object sender, EventArgs e)
        {

            rd.baglanti.Close();
            rd.baglanti.Open();
            
                OleDbCommand cm4 = new OleDbCommand("Select max(y_hazirlanma_suresi) from yiyecek where y_id in( Select urun_id from siparis where  s_masa_id='" + Giris.mid + "' and urun_turu='yiyecek' )", rd.baglanti);
                if (cm4.ExecuteScalar().ToString()!="")
                    y_h_z = Convert.ToInt32(cm4.ExecuteScalar());
                OleDbCommand cm5 = new OleDbCommand("Select max(t_hazirlanma_suresi) from tatli where t_id in( Select urun_id from siparis where  s_masa_id='" + Giris.mid + "' and urun_turu='tatli' )", rd.baglanti);
                if (cm5.ExecuteScalar().ToString() != "")
                    t_h_z = Convert.ToInt32(cm5.ExecuteScalar());
                rd.baglanti.Close();
                if (y_h_z >= t_h_z)
                    urun_zaman = y_h_z;
                else
                    urun_zaman = t_h_z;
                timer1.Start();
                Giris g = new Giris();
           
            
        }

        private void MusteriEkrani_FormClosed(object sender, FormClosedEventArgs e)
        {
            GiriseDon g = new GiriseDon();
            g.FormKapanma();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string pn = "";
            if (radioButton1.Checked)
                pn = radioButton1.Text;
            if (radioButton2.Checked)
                pn = radioButton2.Text;
            if (radioButton3.Checked)
                pn = radioButton3.Text;
            if (radioButton4.Checked)
                pn = radioButton4.Text;
            if (radioButton5.Checked)
                pn = radioButton5.Text;

            try
            {
                rd.baglanti.Open();
                OleDbCommand komut = new OleDbCommand("INSERT INTO geribildirim (puan,yorum) VALUES ("+Convert.ToInt32(pn) + ",'" + textBox1.Text.ToString() + "')", rd.baglanti);
                komut.ExecuteNonQuery();
                rd.baglanti.Close();
                MessageBox.Show("teşekkür ederiz!");
                rd.baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        int y_h_z,t_h_z=0;
        int urun_zaman=0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            label2.Text = "" + urun_zaman;
            if (urun_zaman>0)
                urun_zaman--;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
               
                rd.baglanti.Close();
                rd.baglanti.Open();
                rd.cm = new OleDbCommand("Select calisan_id from masalar where m_id=" + Giris.mid + "", rd.baglanti);
                int eleman_id = Convert.ToInt32(rd.cm.ExecuteScalar());
                rd.cm.Dispose();
                rd.cm = new OleDbCommand("Insert Into bildirimler (b_alan_id,b_bildirim) values(" + Giris.eid + ",'Masa " + Giris.mid + " Garson Bekliyor')", rd.baglanti);
                rd.cm.ExecuteNonQuery();
                rd.cm.Dispose();
                rd.baglanti.Close();
                MessageBox.Show("Garson Çağırıldı");
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                rd.baglanti.Close();
                rd.baglanti.Open();
                rd.cm = new OleDbCommand("Select calisan_id from masalar where m_id=" + Giris.mid + "", rd.baglanti);
                int eleman_id = Convert.ToInt32(rd.cm.ExecuteScalar());
                rd.cm.Dispose();
                rd.cm = new OleDbCommand("Insert Into bildirimler (b_alan_id,b_bildirim) values(" + Giris.eid + ",'Masa " + Giris.mid + " Hesap istiyor')", rd.baglanti);
                rd.cm.ExecuteNonQuery();
                rd.cm.Dispose();
                rd.baglanti.Close();
                MessageBox.Show("Hesap İstenildi");
            }
            catch (Exception)
            {


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu men = new Menu();
            men.ShowDialog();
        }
    }
}
