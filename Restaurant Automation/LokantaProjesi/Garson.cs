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
    public partial class Garson : Form
    {//bendeki
        public Garson()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=lokanta.accdb");
        OleDbCommand cmd;
        OleDbDataReader dr;
        public int x = 0, y = 0, z = 0,b_id;
        private void Garson_Load(object sender, EventArgs e)
        {
            bildirimguncelle();
            dataGridView1.Columns[0].HeaderText = "Bildirim No";
            dataGridView1.Columns[1].HeaderText = "Bildirim";
            label3.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label3.Text = "0";
            label7.Text = "0";
            label8.Text = "0";

            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM masalar where m_tipi='restaurant' order by m_id";
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox2.Items.Add(dr["m_id"]);
                comboBox3.Items.Add(dr["m_id"]);
                comboBox6.Items.Add(dr["m_id"]);

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
        RNYC rd = new RNYC();
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
            rd.cm = new OleDbCommand("Update masalar set m_durum='dolu',calisan_id="+garson_id+" where m_id="+Convert.ToInt32(comboBox2.SelectedItem)+"",rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            rd.baglanti.Close();
            MessageBox.Show("Sipariş Verildi :)");
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

        private void button8_Click(object sender, EventArgs e)
        {
            z -= 1;

            if (z < 0)
                z = 0;
            label8.Text = Convert.ToString(z);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            x += 1;
            label3.Text = Convert.ToString(x);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label7.Visible = true;
            y += 1;
            label7.Text = Convert.ToString(y);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label8.Visible = true;
            z += 1;
            label8.Text = Convert.ToString(z);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            bildirimguncelle();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("Delete from bildirimler where b_id =" + b_id + "", con);
            cmd.ExecuteNonQuery();
            con.Close();
            bildirimguncelle();
        }
        int garson_id = Giris.eid;
        public void bildirimguncelle()
        {
            con.Open();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("Select b_id,b_bildirim from bildirimler where b_alan_id=" + garson_id + " or b_alan_id="+0+"", con);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            da.Dispose();
            con.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            b_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
        }

        private void Garson_FormClosed(object sender, FormClosedEventArgs e)
        {
            GiriseDon gd = new GiriseDon();
            gd.FormKapanma();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("Update siparis set s_masa_id='" + comboBox6.SelectedItem + "'where s_masa_id='" + comboBox3.SelectedItem + "'", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = new OleDbCommand("Update masalar set m_durum='boş' where m_id="+Convert.ToInt32(comboBox3.SelectedItem)+"",con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = new OleDbCommand("Update masalar set m_durum='dolu' where m_id="+Convert.ToInt32(comboBox6.SelectedItem)+"",con);
            cmd.ExecuteNonQuery();
            
            con.Close();
            MessageBox.Show("Masa Taşıma Başarılı :)");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            con.Open();

            cmd = new OleDbCommand("Update masalar set m_bir_no='" + comboBox6.SelectedItem + "'where m_id=" + Convert.ToInt32(comboBox3.SelectedItem) + "", con);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("Update masalar set m_bir_no='" + comboBox3.SelectedItem + "'where m_id=" + Convert.ToInt32(comboBox6.SelectedItem) + "", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Masa Birleştirme Başarılı :)");   
        }
    }
}
