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
    public partial class Paket_Siparis : Form
    {
        public Paket_Siparis()
        {
            InitializeComponent();
        }
        //recep
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=lokanta.accdb");
        OleDbCommand cmd;
        OleDbDataReader dr;
        public int x = 0, y = 0, z = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            Kasa.masa_no = Convert.ToInt32(comboBox2.SelectedItem);
            Form1 f = new Form1();
            f.ShowDialog();
        }
        RNYC rd = new RNYC();
        private void Paket_Siparis_Load(object sender, EventArgs e)
        {
            label3.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label3.Text = "0";
            label7.Text = "0";
            label8.Text = "0";

            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM masalar where m_tipi='paket'";
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox2.Items.Add(dr["m_id"]);
            }
            con.Close();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM yiyecek";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["y_isim"]);

            }
            con.Close();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM icecekler";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr["i_isim"]);

            }
            con.Close();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM tatli";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox5.Items.Add(dr["t_isim"]);

            }
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            x -= 1;

            if (x < 0)
                x = 0;
            label3.Text = Convert.ToString(x);
        }

      
        private void button6_Click(object sender, EventArgs e)
        {
            y -= 1;

            if (y < 0)
                y = 0;
            label7.Text = Convert.ToString(y);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label7.Visible = true;
            y += 1;
            label7.Text = Convert.ToString(y);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            z -= 1;
            if (z < 0)
                z = 0;
            label8.Text = Convert.ToString(z);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label8.Visible = true;
            z += 1;
            label8.Text = Convert.ToString(z);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            x += 1;
            label3.Text = Convert.ToString(x);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string zaman = DateTime.Now.ToString("hh:mm:ss");
            int i = 0, a, u_id;
            con.Open();
            OleDbCommand komut = new OleDbCommand();
            a = Convert.ToInt32(label3.Text);
            while (i < a)
            {
                OleDbCommand cm2 = new OleDbCommand("Select y_id from yiyecek where y_isim ='" + comboBox1.SelectedItem + "'", con);
                u_id = Convert.ToInt32(cm2.ExecuteScalar());
                komut = new OleDbCommand("INSERT INTO siparis (s_masa_id, urun_turu, urun_id,durum,s_zaman) VALUES ('" + comboBox2.SelectedItem + "', '" + "yiyecek" + "','" + u_id.ToString() + "','hazırlanıyor','" + zaman + "' )", con);
                cmd = new OleDbCommand("Update masalar set m_durum='dolu'where m_id=" + Convert.ToInt32(comboBox2.SelectedItem) + "", con);
                komut.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                komut.Dispose();
                cmd.Dispose();
                i++;
            }
            con.Close();
            i = 0;
            con.Open();
            a = Convert.ToInt32(label7.Text);
            while (i < a)
            {
                OleDbCommand cm2 = new OleDbCommand("Select i_id from icecekler where i_isim ='" + comboBox4.SelectedItem + "'", con);
                u_id = Convert.ToInt32(cm2.ExecuteScalar());
                komut = new OleDbCommand("INSERT INTO siparis (s_masa_id, urun_turu, urun_id,durum,s_zaman) VALUES ('" + comboBox2.SelectedItem + "', '" + "icecek" + "','" + u_id.ToString() + "','-','" + zaman + "' )", con);

                komut.ExecuteNonQuery();
                komut.Dispose();
                i++;
            }
            i = 0;
            con.Close();

            con.Open();
            a = Convert.ToInt32(label8.Text);
            while (i < a)
            {
                OleDbCommand cm2 = new OleDbCommand("Select t_id from tatli where t_isim ='" + comboBox5.SelectedItem + "'", con);
                u_id = Convert.ToInt32(cm2.ExecuteScalar());

                komut = new OleDbCommand("INSERT INTO siparis (s_masa_id, urun_turu, urun_id,durum,s_zaman) VALUES ('" + comboBox2.SelectedItem + "', '" + "tatli" + "','" + u_id.ToString() + "','-','" + zaman + "' )", con);

                komut.ExecuteNonQuery();
                komut.Dispose();
                i++;
            }
            i = 0;
            con.Close();
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("Insert Into paket_siparis (s_adres,s_fiyat,s_tel,s_odeme_turu) values('"+richTextBox1.Text+"',"+HesapCek(Convert.ToInt32(comboBox2.SelectedItem))+",'"+textBox1.Text+"','"+comboBox3.SelectedItem+"')",rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.baglanti.Close();
            MessageBox.Show("Sipariş Verildi");
            
        }
        int top_hesap=0;
        public double HesapCek(int masa_no)
        {
           
            //MessageBox.Show("mnum "+m_num);
            OleDbCommand cmr2 = new OleDbCommand();
            OleDbCommand cmr = new OleDbCommand("Select * from siparis where s_masa_id='" +masa_no + "'", rd.baglanti);
            OleDbDataReader drr = cmr.ExecuteReader();
            while (drr.Read())
            {
                if (drr["urun_turu"].ToString() == "yiyecek")
                {
                    cmr2 = new OleDbCommand("Select y_fiyat from yiyecek where y_id=" + Convert.ToInt32(drr["urun_id"].ToString()) + "", rd.baglanti);
                    top_hesap += Convert.ToInt32(cmr2.ExecuteScalar());

                }
                else if (drr["urun_turu"].ToString() == "icecek")
                {
                    cmr2 = new OleDbCommand("Select i_fiyat from icecekler where i_id=" + Convert.ToInt32(drr["urun_id"].ToString()) + "", rd.baglanti);
                    top_hesap += Convert.ToInt32(cmr2.ExecuteScalar());
                }
                else if (drr["urun_turu"].ToString() == "tatli")
                {
                    cmr2 = new OleDbCommand("Select t_fiyat from tatli where t_id=" + Convert.ToInt32(drr["urun_id"].ToString()) + "", rd.baglanti);
                    top_hesap += Convert.ToInt32(cmr2.ExecuteScalar());
                }
            }
           // textBox1.Text = "" + top_hesap;
            return top_hesap;
        }
    }
}
