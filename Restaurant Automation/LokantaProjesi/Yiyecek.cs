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
    public partial class Yiyecek : Form
    {
        public Yiyecek()
        {
            InitializeComponent();
        }
        RNYC rd = new RNYC();
        private void button3_Click(object sender, EventArgs e)
        {//yeni ürün butonu
            button4.Enabled = true;
            button2.Enabled = false;
            button5.Enabled = false;
            Temizle();
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("SElect m_adi From stok",rd.baglanti);
            rd.dr = rd.cm.ExecuteReader();
            listBox1.Items.Clear();
            while (rd.dr.Read())
            {
               
               comboBox1.Items.Add(rd.dr["m_adi"].ToString());
                
            }
            rd.cm = new OleDbCommand("Insert Into yiyecek (y_isim,y_resim_yol) Values('Yeni','Yeni')", rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            rd.cm = new OleDbCommand("Select max(y_id) from yiyecek",rd.baglanti);
            son_kayit=rd.cm.ExecuteScalar().ToString();
            rd.baglanti.Close();

        }
        public void DgGuncelle() {
            rd.baglanti.Open();
            rd.dt = new DataTable();
            rd.dadapter = new OleDbDataAdapter("Select * From yiyecek", rd.baglanti);
            rd.dadapter.Fill(rd.dt);
            dataGridView1.DataSource = rd.dt;
            rd.dadapter.Dispose();
            rd.baglanti.Close();
        }
        public void Temizle() {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Items.Clear();
            listBox1.Items.Clear();
            groupBox1.Visible = true;
        }
        string y_num,son_kayit;
        private void Gezgin()
        {
           
                DataRow kayit = ds.Tables["yiyecek"].Rows[sira];
                y_num = kayit.ItemArray.GetValue(0).ToString();
                textBox1.Text = kayit.ItemArray.GetValue(1).ToString();
                textBox2.Text = kayit.ItemArray.GetValue(2).ToString();
                textBox3.Text = kayit.ItemArray.GetValue(3).ToString();
                textBox3.Text = kayit.ItemArray.GetValue(3).ToString();
                textBox5.Text = kayit.ItemArray.GetValue(5).ToString();
                pictureBox1.ImageLocation = textBox5.Text;
                listBox1.Items.Clear();
                rd.baglanti.Open();
                rd.cm = new OleDbCommand("Select m_adi from stok where m_id in (Select malzeme_id From yemek_malzeme where yemek_id='" + y_num.ToString() + "')", rd.baglanti);
                    
                 rd.dr = rd.cm.ExecuteReader();
                while (rd.dr.Read())
                {
                    listBox1.Items.Add(rd.dr["m_adi"].ToString());
                }
                rd.baglanti.Close();
           
            
            
        }
        DataSet ds = new DataSet();
        
        int sira = 0;
        int toplamkayit;
        private void Yiyecek_Load(object sender, EventArgs e)
        {
            DgGuncelle();
            dataGridView1.Columns[0].HeaderCell.Value = "Yiyecek No";
            dataGridView1.Columns[1].HeaderCell.Value = "İsim";
            dataGridView1.Columns[2].HeaderCell.Value = "Fiyat";
            dataGridView1.Columns[3].HeaderCell.Value = "Hazırlanma(dk)";
            dataGridView1.Columns[4].HeaderCell.Value = "Malzeme Listesi";
            dataGridView1.Columns[5].HeaderCell.Value = "Resim yol";
            ListView listView1 = new ListView();
            rd.dadapter = new OleDbDataAdapter("Select * From yiyecek",rd.baglanti);          
            ds = new DataSet();
            rd.dadapter.Fill(ds,"yiyecek");

            rd.cm = new OleDbCommand("select count(*) from yiyecek ",rd.baglanti);
            rd.baglanti.Open();
            toplamkayit = Convert.ToInt32(rd.cm.ExecuteScalar());
            rd.baglanti.Close();
            sira = 0; 
            Gezgin();
        }

        private void button4_Click(object sender, EventArgs e)
        {//Buton kaydet
            button4.Enabled = false;
            button2.Enabled = true;
            button5.Enabled = true;
            groupBox1.Visible = false;
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("Update yiyecek SET y_isim='" + textBox1.Text.ToString() + "', y_fiyat='" + textBox2.Text.Trim() + "',y_hazirlanma_suresi='" + textBox3.Text.Trim() + "',y_malzeme_listesi='" + yemek_malzeme_listesi + "',y_resim_yol='" + textBox5.Text + "' where y_id=" + son_kayit+ "", rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.baglanti.Close();
            DgGuncelle();
            ListView listView1 = new ListView();
            rd.dadapter = new OleDbDataAdapter("Select * From yiyecek", rd.baglanti);
            ds = new DataSet();
            rd.dadapter.Fill(ds, "yiyecek");
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("Delete From yiyecek where y_isim='Yeni' or y_isim=''", rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.baglanti.Close();
            sira = 0;
            Gezgin();
            DgGuncelle();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        string dosyayolu;
       
        private void button5_Click(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("Delete From yiyecek where y_id="+Convert.ToInt32(y_num)+"", rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm.Dispose();
            
            ListView listView1 = new ListView();
            rd.dadapter = new OleDbDataAdapter("Select * From yiyecek", rd.baglanti);
            ds = new DataSet();
            rd.dadapter.Fill(ds, "yiyecek");
            rd.baglanti.Close();
            sira = 0;
            Gezgin();
            rd.baglanti.Close();
            DgGuncelle();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            sira = 0;
            Gezgin();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            sira = toplamkayit - 1;
            Gezgin();
        }

        private void button8_Click(object sender, EventArgs e)
        {
        if (sira>0)
                {
                sira--;
                Gezgin();
                }
            else
                {
                    MessageBox.Show("İlk Kayıttasınız");
                }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (sira != toplamkayit-1 )
                {
                sira++;
                Gezgin();
                }
                else
                {
                    MessageBox.Show("Son Kayıttasınız");
                }
        }
        string yemek_malzeme_listesi = "";
        int yem_id;
        int yem_malz_id;
        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(comboBox1.SelectedItem+"-"+textBox4.Text);
            rd.baglanti.Open();
           
           /* rd.cm = new OleDbCommand("Select MAX(y_id) from yiyecek where y_isim='Yeni' ",rd.baglanti);
            yem_id =Convert.ToInt32(rd.cm.ExecuteScalar())+1;
            rd.cm.Dispose();*/
            rd.cm = new OleDbCommand("Select m_id from stok where m_adi='"+comboBox1.SelectedItem.ToString()+"'",rd.baglanti);
            int mal_id = Convert.ToInt32(rd.cm.ExecuteScalar());
            rd.cm.Dispose();
            rd.cm = new OleDbCommand("Insert Into yemek_malzeme (yemek_id,malzeme_id,malzeme_miktar) Values('"+son_kayit+"', "+mal_id+",'"+textBox4.Text.ToString().Trim()+"')",rd.baglanti);
            rd.cm.ExecuteNonQuery();
            rd.cm = new OleDbCommand("Select MAX(ym_id) from yemek_malzeme", rd.baglanti);
            yem_malz_id = Convert.ToInt32(rd.cm.ExecuteScalar());
            yemek_malzeme_listesi += yem_malz_id + " ";
            rd.cm.Dispose();
            rd.baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            rd.cm = new OleDbCommand("Update yiyecek Set  y_isim='" + textBox1.Text.Trim() + "',y_fiyat='" + textBox2.Text.Trim() + "',y_hazirlanma_suresi='" + textBox3.Text.Trim() + "',y_resim_yol='" + textBox5.Text.Trim() + "' WHERE y_id=@id", rd.baglanti);
            rd.cm.Parameters.AddWithValue("@id", y_num);
            rd.cm.ExecuteNonQuery();
            rd.baglanti.Close();
            DgGuncelle();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg; |*.nef; |*.png |  Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            dosyayolu = dosya.FileName;
            textBox5.Text = dosyayolu;
            pictureBox1.ImageLocation = dosyayolu;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem); 
        }

        private void Yiyecek_FormClosed(object sender, FormClosedEventArgs e)
        {
            GiriseDon gd=new GiriseDon();
            gd.FormKapanma();
        }
       
    }
}
