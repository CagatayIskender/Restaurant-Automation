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
    public partial class Yonetici : Form
    {
        public Yonetici()
        {
            InitializeComponent();
        }

        private void elemanİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Eleman_Ekle ek = new Eleman_Ekle();
            ek.ShowDialog();

        }

        private void Yonetici_FormClosed(object sender, FormClosedEventArgs e)
        {
            GiriseDon kp = new GiriseDon();
            kp.FormKapanma();
        }
        RNYC rd = new RNYC();
        int gider=0, gelir=0;
        private void Yonetici_Load(object sender, EventArgs e)
        {
            string z = dateTimePicker1.Value.ToShortDateString();
            rd.baglanti.Open();

            rd.cm = new OleDbCommand("Select sum(islem_tutar)  as toplam From kasa where islem_zaman='"+z+"'", rd.baglanti);
            rd.dr = rd.cm.ExecuteReader();
            while (rd.dr.Read())
            {
                label2.Text = "" + rd.dr["toplam"];
                
            }

            rd.cm = new OleDbCommand("Select sum(islem_tutar) From kasa where islem_tutar< 0", rd.baglanti);
            gider += Convert.ToInt32(rd.cm.ExecuteScalar());
            OleDbCommand cmr = new OleDbCommand("Select sum(k_maas) as toplam from kullanicilar",rd.baglanti);
            gider += Convert.ToInt32(cmr.ExecuteScalar());
            label4.Text = "" + gider;
            rd.cm = new OleDbCommand("Select sum(islem_tutar)  From kasa where islem_tutar>0", rd.baglanti);
            gelir = Convert.ToInt32(rd.cm.ExecuteScalar());
            label6.Text = "" + gelir;

            label8.Text = (gelir - gider).ToString();
           
            rd.baglanti.Close();
        }

        private void müşteriGeribildirimleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeriBildirim gb = new GeriBildirim();
            gb.ShowDialog();
        }

        private void menüyüGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void yiyeceklerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Yiyecek y = new Yiyecek();
            y.ShowDialog();
        }

        private void içeceklerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Icecek i = new Icecek();
            i.ShowDialog();
        }

        private void tatlılarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tatli t = new Tatli();
            t.ShowDialog();
        }

        private void mutfakMalzemeİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stok_Ekle se = new Stok_Ekle();
            se.ShowDialog();
        }

        private void yeniMasaEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void restoranaEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("Insert Into masalar(m_durum,m_bir_no,calisan_id,m_tipi) values('boş','0',0,'restaurant')", rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            rd.baglanti.Close();
        }

        private void paketSiparişeEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("Insert Into masalar(m_durum,m_bir_no,calisan_id,m_tipi) values('boş','0',0,'paket')", rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            rd.baglanti.Close();
        }
        
    }
    
}
