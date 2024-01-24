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
    public partial class Kurye : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= lokanta.accdb");

        public Kurye()
        {
            InitializeComponent();
        }




        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int s_id;
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            s_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        int tutar;
       
        public void DgGuncelle()
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= lokanta.accdb");
            baglanti.Open();
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT s_id,s_adres,s_fiyat,s_odeme_turu FROM paket_siparis", baglanti);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            adapter.Dispose();
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            int kayit_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            OleDbCommand komut = new OleDbCommand("DELETE  FROM paket_siparis WHERE s_id=@id", baglanti);
            komut.Parameters.AddWithValue("@id", kayit_id);
            komut.ExecuteNonQuery();
            komut.Dispose();
            string z = dateTimePicker1.Value.ToShortDateString();


            OleDbCommand cm2 = new OleDbCommand("INSERT into kasa(islem_zaman, islem_tutar) values('" + z + "', " + tutar + ")", baglanti);
            cm2.ExecuteNonQuery();
            baglanti.Close();
            DgGuncelle();

        }

        private void Kurye_Load(object sender, EventArgs e)
        {
             DgGuncelle();
            dataGridView1.Columns[0].HeaderCell.Value = "Sipariş No";
            dataGridView1.Columns[1].HeaderCell.Value = "Adres";
            dataGridView1.Columns[1].HeaderCell.Value = "Fiyat";
            dataGridView1.Columns[1].HeaderCell.Value = "Ödeme Türü";
        }

        private void dataGridView1_SelectionChanged_2(object sender, EventArgs e)
        {
            tutar = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
        }

        private void Kurye_FormClosed(object sender, FormClosedEventArgs e)
        {
            GiriseDon gd = new GiriseDon();
            gd.FormKapanma();
        }
    }
}
