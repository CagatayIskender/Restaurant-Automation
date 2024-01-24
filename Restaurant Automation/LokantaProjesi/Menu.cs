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
    public partial class Menu : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=lokanta.accdb");

        OleDbCommand cmd;
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

            PictureBox pb = new PictureBox();
            Label l = new Label();
            con.Open();
            cmd = new OleDbCommand("Select y_resim_yol,y_isim,y_fiyat from yiyecek order by y_id", con);
            OleDbDataReader dr = cmd.ExecuteReader();

            int x = 50, y = 20, z = 80, t = 220;
            l = new Label();
            l.Location = new System.Drawing.Point(x, y);
            l.Font = new Font("Arial Black", 24);
            l.ForeColor = Color.Brown;
            l.Text = "Yemekler";
            l.AutoSize = true;
            this.Controls.Add(l);
            x = 50;
            y += 100;

            t += 100;
            while (dr.Read())
            {
                if (x == 1100)
                {
                    x = 50;
                    y += 250;
                    z = 80;
                    t += 250;
                }

                pb = new PictureBox();
                pb.Location = new System.Drawing.Point(x, y);
                pb.Size = new System.Drawing.Size(320, 180);
                pb.ImageLocation = dr["y_resim_yol"].ToString();
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(pb);
                l = new Label();
                l.Location = new System.Drawing.Point(z, t);
                l.Text = dr["y_isim"].ToString().Trim();
                l.ForeColor = Color.DarkMagenta;
                l.Font = new Font("Times New Roman", 15);
                l.AutoSize = true;
                this.Controls.Add(l);
                l = new Label();
                l.Location = new System.Drawing.Point(z + 170, t);
                l.Text = dr["y_fiyat"].ToString().Trim() + "  TL";
                l.ForeColor = Color.Green;
                l.Font = new Font("Times New Roman", 15);
                l.AutoSize = true;

                this.Controls.Add(l);
                x += 350;
                z += 350;

            }
            cmd = new OleDbCommand("Select t_resim_yol,t_isim,t_fiyat from tatli order by t_id", con);
            dr = cmd.ExecuteReader();
            x = 50;
            y += 400;
            z = 80;
            t += 400;
            l = new Label();
            l.Location = new System.Drawing.Point(x, y - 100);
            l.Font = new Font("Arial Black", 24);
            l.ForeColor = Color.Brown;
            l.Text = "Tatlılar";

            l.AutoSize = true;
            this.Controls.Add(l);
            while (dr.Read())
            {
                if (x == 1100)
                {
                    x = 50;
                    y += 250;
                    z = 80;
                    t += 250;
                }

                pb = new PictureBox();
                pb.Location = new System.Drawing.Point(x, y);
                pb.Size = new System.Drawing.Size(320, 180);
                pb.ImageLocation = dr["t_resim_yol"].ToString();
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(pb);
                l = new Label();
                l.Location = new System.Drawing.Point(z, t);
                l.Text = dr["t_isim"].ToString().Trim();
                l.AutoSize = true;
                l.ForeColor = Color.DarkTurquoise;
                l.Font = new Font("Times New Roman", 15);
                this.Controls.Add(l);
                l = new Label();
                l.Location = new System.Drawing.Point(z + 170, t);
                l.Text = dr["t_fiyat"].ToString().Trim() + "  TL";
                l.AutoSize = true;
                l.ForeColor = Color.Green;
                l.Font = new Font("Times New Roman", 15);
                this.Controls.Add(l);
                x += 350;
                z += 350;

            }


















            cmd = new OleDbCommand("Select i_resim_yol,i_isim,i_fiyat from icecekler order by i_id", con);
            dr = cmd.ExecuteReader();
            x = 50;
            y += 400;
            z = 80;
            t += 400;
            l = new Label();
            l.Location = new System.Drawing.Point(x, y - 100);
            l.Font = new Font("Arial Black", 24);
            l.ForeColor = Color.Brown;
            l.Text = "İçecekler";

            l.AutoSize = true;
            this.Controls.Add(l);
            while (dr.Read())
            {
                if (x == 1100)
                {
                    x = 50;
                    y += 250;
                    z = 80;
                    t += 250;
                }

                pb = new PictureBox();
                pb.Location = new System.Drawing.Point(x, y);
                pb.Size = new System.Drawing.Size(320, 180);
                pb.ImageLocation = dr["i_resim_yol"].ToString();
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(pb);
                l = new Label();
                l.Location = new System.Drawing.Point(z, t);
                l.Text = dr["i_isim"].ToString().Trim();
                l.AutoSize = true;
                l.ForeColor = Color.DarkTurquoise;
                l.Font = new Font("Times New Roman", 15);
                this.Controls.Add(l);
                l = new Label();
                l.Location = new System.Drawing.Point(z + 170, t);
                l.Text = dr["i_fiyat"].ToString().Trim() + "  TL";
                l.AutoSize = true;
                l.ForeColor = Color.Green;
                l.Font = new Font("Times New Roman", 15);
                this.Controls.Add(l);
                x += 350;
                z += 350;

            }
            pb = new PictureBox();
            pb.Location = new System.Drawing.Point(x, y + 100);
            pb.Size = new System.Drawing.Size(320, 180);

            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(pb);
            con.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
