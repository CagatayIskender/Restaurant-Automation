using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Data.OleDb;
namespace LokantaProjesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         
        private void button1_Click(object sender, EventArgs e)
        {
             /*StreamWriter Yaz = new StreamWriter(Application.StartupPath+"/Fatura.txt");
            Yaz.WriteLine("C# StreamWriter");
            Yaz.WriteLine("Hikmet Okumuş");
            Yaz.Close();  */
            printDocument1.Print();
        }
        RNYC rd = new RNYC();
        string satir;
        private void Form1_Load(object sender, EventArgs e)
        {
            printPreviewControl1.Zoom = 1;
            
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int i = 10;
            int masa_id = Kasa.masa_no;
            int hesap = Kasa.top_hesap;
            int ozel_hesap = 0;
            OleDbCommand cmr = new OleDbCommand();
            e.Graphics.DrawString("RNYC Restaurant \n \n",new Font("Arial Black",11),Brushes.Black,new PointF(10,i));
            e.Graphics.DrawString("Hesap Fişi,  Masa No :"+masa_id, new Font("Calibri", 11), Brushes.Black, new PointF(10, i+=20));
            e.Graphics.DrawString(dateTimePicker1.Value.ToShortDateString()+"", new Font("Calibri", 11), Brushes.Black, new PointF(10, i += 20));
            e.Graphics.DrawString("Ürünler -- Fiyat", new Font("Calibri", 11), Brushes.Black, new PointF(10, i += 20));
            e.Graphics.DrawString("", new Font("Calibri", 11), Brushes.Black, new PointF(10, i += 10));

            rd.baglanti.Open();
            rd.cm = new OleDbCommand("Select y_isim, y_fiyat,y_id from yiyecek where y_id in (Select urun_id from siparis where urun_turu='yiyecek' and s_masa_id='" + masa_id + "')", rd.baglanti);
            rd.dr = rd.cm.ExecuteReader();
            while (rd.dr.Read())
            {
                
                cmr = new OleDbCommand("Select count(*) from siparis where urun_turu='yiyecek' and urun_id="+Convert.ToInt32(rd.dr["y_id"])+" and s_masa_id='"+masa_id+"'",rd.baglanti);
                satir = "" + rd.dr["y_isim"] + " " + rd.dr["y_fiyat"] + " Tl" + "  " + " -- " + cmr.ExecuteScalar().ToString() + " Adet";
                e.Graphics.DrawString(satir , new Font("Calibri", 11), Brushes.Black, new PointF(10, i += 20));
                ozel_hesap += Convert.ToInt32(rd.dr["y_fiyat"])*Convert.ToInt32(cmr.ExecuteScalar());

            }
            
         OleDbCommand cm2= new OleDbCommand("Select t_isim,t_id, t_fiyat from tatli where t_id in (Select urun_id from siparis where urun_turu='tatli' and s_masa_id='" + masa_id + "')", rd.baglanti);
        OleDbDataReader dr2 = cm2.ExecuteReader();
        while (dr2.Read())
         {
             cmr = new OleDbCommand("Select count(*) from siparis where urun_turu='tatli' and urun_id=" + Convert.ToInt32(dr2["t_id"]) + " and s_masa_id='" + masa_id + "'", rd.baglanti);
             satir = "" + dr2["t_isim"] + " " + dr2["t_fiyat"] + " Tl" + "  " + " -- " + cmr.ExecuteScalar().ToString() + " Adet";
             e.Graphics.DrawString(satir + " \n", new Font("Calibri", 11), Brushes.Black, new PointF(10, i += 20));
             ozel_hesap += Convert.ToInt32(dr2["t_fiyat"]) * Convert.ToInt32(cmr.ExecuteScalar());
         }
            
       OleDbCommand cm3 = new OleDbCommand("Select i_id,i_isim, i_fiyat from icecekler where i_id in (Select urun_id from siparis where urun_turu='icecek' and s_masa_id='" + masa_id + "')", rd.baglanti);
        OleDbDataReader dr3 = cm3.ExecuteReader();
        while (dr3.Read())
        {
            cmr = new OleDbCommand("Select count(*) from siparis where urun_turu='icecek' and urun_id=" + Convert.ToInt32(dr3["i_id"]) + " and s_masa_id='" + masa_id + "'", rd.baglanti);
            satir = "" + dr3["i_isim"] + " " + dr3["i_fiyat"] + " Tl" + "  " + " -- " + cmr.ExecuteScalar().ToString() + " Adet";
            e.Graphics.DrawString(satir + " \n", new Font("Calibri", 11), Brushes.Black, new PointF(10, i += 20));
            ozel_hesap += Convert.ToInt32(dr3["i_fiyat"]) * Convert.ToInt32(cmr.ExecuteScalar());
        }
            rd.baglanti.Close();
            e.Graphics.DrawString("Toplam Hesap :"+ozel_hesap + " TL", new Font("Verdana", 11), Brushes.Black, new PointF(10, i += 30));
            e.Graphics.DrawString("Afiyet Olsun, Yine Bekleriz :)", new Font("Ariel", 8), Brushes.Black, new PointF(10, i += 30));

        }
    }
}
