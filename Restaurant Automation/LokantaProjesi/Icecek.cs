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
    public partial class Icecek : Form
    {
        public Icecek()
        {
            InitializeComponent();
        }
        RNYC rd = new RNYC();
        public void DgGuncelle()
        {
            rd.baglanti.Open();
            rd.dt = new DataTable();
            rd.dadapter = new OleDbDataAdapter("Select * From icecekler", rd.baglanti);
            rd.dadapter.Fill(rd.dt);
            dataGridView1.DataSource = rd.dt;
            rd.dadapter.Dispose();
            rd.baglanti.Close();
        }
        private void Icecek_Load(object sender, EventArgs e)
        {
            DgGuncelle();
            dataGridView1.Columns[0].HeaderCell.Value = "İçecek No";
            dataGridView1.Columns[1].HeaderCell.Value = "İsim";
            dataGridView1.Columns[2].HeaderCell.Value = "Fiyat";
            dataGridView1.Columns[3].HeaderCell.Value = "Stok";
            dataGridView1.Columns[4].HeaderCell.Value = "Resim yolu";
        }
        int urun_id;
        string dosyayolu;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            urun_id=Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            pictureBox1.ImageLocation = textBox4.Text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("UPDATE icecekler SET i_isim='"+textBox1.Text.Trim()+"', i_fiyat="+Convert.ToInt32(textBox2.Text.Trim())+", i_stok="+Convert.ToInt32(textBox3.Text.Trim())+", i_resim_yol='"+textBox4.Text+"' where i_id="+urun_id+"", rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            rd.baglanti.Close();
            DgGuncelle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("Delete From icecekler where [i_id]=@id", rd.baglanti);
            rd.cm.Parameters.AddWithValue("@id", urun_id);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            rd.baglanti.Close();
            DgGuncelle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("INSERT INTO icecekler (i_isim,i_fiyat,i_stok,i_resim_yol) Values('" + textBox1.Text.Trim() + "'," +Convert.ToInt32(textBox2.Text.Trim()) + "," +Convert.ToInt32(textBox3.Text.Trim()) + ",'" + textBox4.Text + "')", rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            rd.baglanti.Close();
            DgGuncelle();
            rd.baglanti.Close();
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png |  Tüm Dosyalar |*.*";
            dosya.ShowDialog();
             dosyayolu= dosya.FileName;
            textBox4.Text = dosyayolu;
            pictureBox1.ImageLocation = dosyayolu;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = false;
            pictureBox1.ImageLocation = "";
        }
    }
}
