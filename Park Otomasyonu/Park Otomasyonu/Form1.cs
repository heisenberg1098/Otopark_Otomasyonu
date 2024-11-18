using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Park_Otomasyonu
{   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        char[] dizi = new char[] {
    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
    'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
};
        Random rnd = new Random();
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GNKKFD3;Initial Catalog=otopark;Integrated Security=True;Encrypt=False");
        private void label4_Click(object sender, EventArgs e)
        {
            label4.Text = null;
            for (int i = 0; i < 5; i++)
            {
                label4.Text += dizi[rnd.Next(0, 36)];
            }
            Clipboard.SetText(label4.Text);

        }
        string ıd;
        public static string admin;
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand KOMUT = new SqlCommand(string.Format("select ıd from admin where kul_ad='{0}' and sifre='{1}'", textBox1.Text, textBox2.Text), con);
            admin = KOMUT.ExecuteScalar().ToString();
            SqlCommand komut = new SqlCommand("select * from admin", con);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (textBox1.Text == oku["kul_ad"].ToString())
                {
                    if (textBox2.Text==oku["sifre"].ToString()&&textBox3.Text==label4.Text)
                    {
                        
                        anaform a1 = new anaform();
                        a1.Show();
                        this.Hide();
                    }
                    ıd = oku["ıd"].ToString();
                } 
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            anaform a1 = new anaform();
            a1.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
