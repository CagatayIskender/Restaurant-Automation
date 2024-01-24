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
    public partial class Stok_Ekle : Form
    {
        public Stok_Ekle()
        {
            InitializeComponent();
        }
        RNYC rd = new RNYC();

        public void DgGuncelle() {
            rd.baglanti.Open();
            rd.dt = new DataTable();
            rd.dadapter = new OleDbDataAdapter("Select * From stok", rd.baglanti);
            rd.dadapter.Fill(rd.dt);
            dataGridView1.DataSource = rd.dt;
            rd.dadapter.Dispose();
            rd.baglanti.Close();
        }
        private void Stok_Ekle_Load(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            
            rd.dt = new DataTable();
            rd.dadapter = new OleDbDataAdapter("Select * From stok",rd.baglanti);
            rd.dadapter.Fill(rd.dt);
            dataGridView1.DataSource = rd.dt;
            rd.dadapter.Dispose();
            rd.cm =new OleDbCommand("Select m_adi From stok Group By m_adi",rd.baglanti);
            rd.dr = rd.cm.ExecuteReader();
            while (rd.dr.Read())
            {
                comboBox1.Items.Add(rd.dr["m_adi"].ToString());
            }
            dataGridView1.Columns[0].HeaderText = "Malzeme No";
            dataGridView1.Columns[1].HeaderText = "İsmi";
            dataGridView1.Columns[2].HeaderText = "Stok";
            dataGridView1.Columns[3].HeaderText = "Son Alış Fiyatı";

            rd.baglanti.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            rd.baglanti.Open();

            OleDbCommand ekle = new OleDbCommand("Update stok set m_stok=m_stok+"+int.Parse(textBox1.Text.Trim())+", m_alis_fiyat="+int.Parse(textBox2.Text.Trim())+" where m_adi='"+comboBox1.SelectedItem+"'",rd.baglanti);
            ekle.ExecuteNonQuery();
            ekle.Dispose();

            string z = dateTimePicker1.Value.ToShortDateString();
            double maaliyet=0;
            if (comboBox2.SelectedItem=="(adet)")
                maaliyet = int.Parse(textBox1.Text)  * (int.Parse(textBox2.Text)) * -1;
            else if (comboBox2.SelectedItem=="(gr)")
                maaliyet = int.Parse(textBox1.Text) / 1000 * (int.Parse(textBox2.Text)) * -1;

            OleDbCommand cm2 = new OleDbCommand("INSERT Into kasa (islem_zaman,islem_tutar) VALUES('"+z+"',"+maaliyet+")",rd.baglanti);
            cm2.ExecuteNonQuery();
            cm2.Dispose();
           // rd.cm.ExecuteNonQuery();
           // rd.cm.Dispose();
            rd.baglanti.Close();
            DgGuncelle();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            OleDbCommand cm2 = new OleDbCommand("INSERT Into stok (m_adi,m_stok,m_alis_fiyat) VALUES('" +textBox3.Text.Trim()+ "'," + 0 + ", "+0+")", rd.baglanti);
            cm2.ExecuteNonQuery();
            cm2.Dispose();
            rd.baglanti.Close();
            comboBox1.Items.Add(textBox3.Text.Trim());
            comboBox1.Focus();
            DgGuncelle();
            panel1.Visible = false;
        }
    }
}
