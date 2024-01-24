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
    public partial class Stok_Goruntule : Form
    {
        public Stok_Goruntule()
        {
            InitializeComponent();
        }
        RNYC rd = new RNYC();
        private void Stok_Goruntule_Load(object sender, EventArgs e)
        {
            rd.baglanti.Open();
            rd.dt = new DataTable();
            rd.dadapter = new OleDbDataAdapter("Select  * from stok",rd.baglanti);
            rd.dadapter.Fill(rd.dt);
            dataGridView1.DataSource = rd.dt;
            rd.dadapter.Dispose();
            rd.baglanti.Close();
        }
    }
}
