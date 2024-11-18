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
    public partial class parkyeri : Form
    {
        public parkyeri()
        {
            InitializeComponent();
            for (int i = 1; i <= 41; i++)
            {
                Button button = this.Controls["button" + i.ToString()] as Button;
                if (button != null)
                {
                    button.Click += new EventHandler(Button_Click);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                yer = clickedButton.Text;
                this.Close();
            }
        }

        public static string yer;
        private void button1_Click(object sender, EventArgs e)
        {
            yer = button1.Text;
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GNKKFD3;Initial Catalog=otopark;Integrated Security=True");
        private void parkyeri_Load(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand komut = new SqlCommand("select * from parkarac", con);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                string parkyeriValue = oku["parkyeri"].ToString();
                foreach (Button button in this.Controls.OfType<Button>())
                {
                    if (button.Text == parkyeriValue)
                    {
                        button.BackColor = System.Drawing.Color.Red; // Renk değişimi
                    }
                }
            }

            con.Close();

        }
        public static bool text4;
        private void parkyeri_FormClosing(object sender, FormClosingEventArgs e)
        {
            text4 = true;
        }
    }
}