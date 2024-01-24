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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        public static int eid, mid;
        public void Yonlendir(int id, string bolum)
        {
            if (bolum == "mutfak")
            {
                Giris f1 = new Giris();
                f1.Close();
                Mutfak mf = new Mutfak();
                mf.Show();
                this.Hide();
            }
            else if (bolum == "garson")
            {
                Giris f1 = new Giris();
                f1.Close();
                Garson g = new Garson();
                g.Show();
                this.Hide();
            }

            else if (bolum == "yonetici")
            {
                Giris f1 = new Giris();
                f1.Close();
                Yonetici g = new Yonetici();
                g.Show();
                this.Hide();
            }
            else if (bolum == "kasa")
            {
                Giris f1 = new Giris();
                f1.Close();
                Kasa k = new Kasa();
                k.Show();
                this.Hide();
            }
            else if (bolum == "kurye")
            {
                Giris f1 = new Giris();
                f1.Close();
                Kurye kur = new Kurye();
                kur.Show();
                this.Hide();
            }
        }
        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = true;
        }
        RNYC rd = new RNYC();
        private void button1_Click_1(object sender, EventArgs e)
        {
            string kad, sfr;
            kad = textBox1.Text.Trim();
            sfr = textBox2.Text.Trim();
            string c_id, c_bolum;

            OleDbConnection bg = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0; Data Source=lokanta.accdb");
            bg.Open();
            OleDbCommand cm = new OleDbCommand("Select * From kullanicilar where k_adi='" + kad + "' and k_sifre='" + sfr + "'", bg);
            OleDbDataReader dr = cm.ExecuteReader();
            if (dr.Read())
            {
                
                c_bolum = dr["k_birim"].ToString();
		        eid =Convert.ToInt32(dr["k_id"].ToString());
                c_id=""+eid;
                Yonlendir(eid, c_bolum);

            }
            else if (textBox2.Text=="" && Convert.ToInt32(textBox1.Text) > 0 && Convert.ToInt32(textBox1.Text) < 100) {               
                MusteriEkrani me = new MusteriEkrani();
                Giris f1 = new Giris();
               Giris.mid =Convert.ToInt32(textBox1.Text);
                f1.Close();
                me.Show();
                this.Hide();
               
            }
            else
            {

                MessageBox.Show("Hatalı Giriş Tekrar Deneyiniz...", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Şifreniz Mail adresinize Gönderildi :)");
            panel1.Visible = false;
        }

        private void Giris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
    public class GiriseDon : Form
    {
        public void FormKapanma()
        {
            Giris giris = new Giris();
            giris.Show();
            this.Close();
        }
    }
    public class RNYC : Form {
        public OleDbConnection baglanti=new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0; Data Source=lokanta.accdb");
        public OleDbDataAdapter dadapter ;
        public OleDbDataReader dr ;
        public DataTable dt ;
        public OleDbCommand cm;
        public int masa_numarasi;
        public int eleman_id;
    }
}
