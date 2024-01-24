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
    public partial class GeriBildirim : Form
    {
        public GeriBildirim()
        {
            InitializeComponent();
        }
        RNYC rd = new RNYC();
        private void GeriBildirim_Load(object sender, EventArgs e)
        {
            
            rd.baglanti.Open();
            rd.dt = new DataTable();
            rd.dadapter=new OleDbDataAdapter("Select puan,yorum From geribildirim",rd.baglanti);
            rd.dadapter.Fill(rd.dt);
            dataGridView1.DataSource=rd.dt;
            rd.dadapter.Dispose();
            decimal Total = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Total+= Convert.ToDecimal(dataGridView1.Rows[i].Cells["puan"].Value);
              }
            Total /= dataGridView1.Rows.Count-1;
            Total = Math.Round(Total, 2);
            label2.Text = Total.ToString();
            rd.baglanti.Close();
        }

        private void GeriBildirim_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
