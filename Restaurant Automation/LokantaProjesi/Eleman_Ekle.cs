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
    public partial class Eleman_Ekle : Form
    {
        public Eleman_Ekle()
        {
            InitializeComponent();
        }
        RNYC rd = new RNYC();
        public void DgGunelle() {
            rd.baglanti.Open();
            rd.dt = new DataTable();
            rd.dadapter = new OleDbDataAdapter("Select * From kullanicilar where k_birim <> 'yonetici'", rd.baglanti);
            rd.dadapter.Fill(rd.dt);
            dataGridView1.DataSource = rd.dt;
            rd.dadapter.Dispose();
            rd.baglanti.Close();
        }
        
        private void Eleman_Ekle_Load(object sender, EventArgs e)
        {
            
            DgGunelle();
            dataGridView1.Columns[0].HeaderCell.Value = "Eleman No";
            dataGridView1.Columns[1].HeaderCell.Value = "Adı";
            dataGridView1.Columns[2].HeaderCell.Value = "Soyadı";
            dataGridView1.Columns[3].HeaderCell.Value = "Telefon";
            dataGridView1.Columns[4].HeaderCell.Value = "Birim";
            dataGridView1.Columns[5].HeaderCell.Value = "Şifre";
            dataGridView1.Columns[6].HeaderCell.Value = "Maaş";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        int k_id;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            k_id =Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
           // comboBox1.Items.Add(dataGridView1.CurrentRow.Cells[4].Value.ToString());
            //comboBox1.SelectedIndex = 0;
            textBox4.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("mutfak");
            comboBox1.Items.Add("kasa");
            comboBox1.Items.Add("kurye");
            comboBox1.Items.Add("garson");
            button1.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = true;
            MessageBox.Show("Yeni Eleman Bilgilerini Giriniz");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("UPDATE kullanicilar SET k_adi='"+textBox1.Text.Trim()+"' , k_soyadi='"+textBox2.Text.Trim()+"', k_tel='"+textBox3.Text.Trim()+"',k_birim='"+comboBox1.SelectedItem.ToString()+"',k_sifre='"+textBox4.Text.Trim()+"',k_maas="+Convert.ToInt32(textBox5.Text)+" where k_id=@id",rd.baglanti);
            rd.cm.Parameters.AddWithValue("@id", k_id);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            rd.baglanti.Close();
            DgGunelle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("INSERT INTO kullanicilar (k_adi,k_soyadi,k_tel,k_birim,k_sifre,k_maas) Values('" + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "','" + textBox3.Text.Trim() + "','" + comboBox1.SelectedItem + "','" + textBox4.Text.Trim() + "'," + Convert.ToInt32(textBox5.Text.Trim()) + ")", rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            rd.baglanti.Close();
            DgGunelle();
            rd.baglanti.Close();
            button1.Enabled = true;
            button4.Enabled = true;
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("Delete From kullanicilar where [k_id]=@id",rd.baglanti);
            rd.cm.Parameters.AddWithValue("@id",k_id);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            rd.baglanti.Close();
            DgGunelle();
        }
    }
}
