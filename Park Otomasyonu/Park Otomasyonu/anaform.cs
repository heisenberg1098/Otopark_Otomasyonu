using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Park_Otomasyonu
{
    public partial class anaform : Form
    {
        public anaform()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GNKKFD3;Initial Catalog=otopark;Integrated Security=True");
        private void anaform_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand komut = new SqlCommand("select * from sınıflar", con);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["arac_sinif"].ToString() + "     saatlik ücret : " + oku["saat_ücret"].ToString());
            }

            con.Close();
            oku.Close();
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            yer2 = true;
            parkyeri p1 = new parkyeri();
            p1.Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click_1(object sender, EventArgs e)
        {


        }
        bool yer2=true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (yer2==true)
            textBox3.Text = parkyeri.yer;
            else if (yer2 == false&& parkyeri.text4==true) { 
            textBox4.Text = parkyeri.yer;
            
                con.Close();
                con.Open();
                SqlCommand komut = new SqlCommand(string.Format("select gelistarih from parkarac where parkyeri='{0}'", textBox4.Text), con);
                DateTime gelistarih = Convert.ToDateTime(komut.ExecuteScalar());
                SqlCommand komut2 = new SqlCommand(string.Format("select surucuad from parkarac where parkyeri='{0}'", textBox4.Text), con);
                textBox5.Text = komut2.ExecuteScalar().ToString();
                SqlCommand komut3 = new SqlCommand(string.Format("select aracsinif from parkarac where parkyeri='{0}'", textBox4.Text), con);

                SqlCommand komut4 = new SqlCommand(string.Format("select saat_ücret from sınıflar where ıd='{0}'", komut3.ExecuteScalar()), con);
                double saatucret = Convert.ToDouble(komut4.ExecuteScalar());
                con.Close();
                TimeSpan süre = DateTime.Now - gelistarih;
                double saat = süre.TotalHours;
                label9.Text = (saat * saatucret).ToString();
                label10.Text = saat.ToString();
                label12.Text = saatucret.ToString();
                parkyeri.text4 = false;
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand komut = new SqlCommand(string.Format("INSERT INTO parkarac (parkyeri, surucuad, plaka, tel, aracsinif, gelistarih) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", textBox3.Text, textBox1.Text, textBox2.Text, maskedTextBox1.Text, aracsinif, DateTime.Now), con);
            komut.ExecuteNonQuery();
            con.Close();
        }
        int aracsinif;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            aracsinif = comboBox1.SelectedIndex + 1;
        }
        
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
           
           
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            yer2 = false;
            parkyeri p1 = new parkyeri();
            p1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand KOMUT2 = new SqlCommand(string.Format("update admin set hasılat=hasılat+{0} where ıd='{1}'", Math.Round(Convert.ToDecimal(label9.Text), 0),Form1.admin), con);
            KOMUT2.ExecuteNonQuery();
            
            SqlCommand KOMUT = new SqlCommand(string.Format("delete from parkarac where parkyeri='{0}'",textBox4.Text), con);
            KOMUT.ExecuteNonQuery();
            con.Close();

        }
    }
}
