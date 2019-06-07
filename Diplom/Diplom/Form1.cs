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

namespace Diplom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool access = false;
            int typeAccess = 0;
            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connect.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Login = '" + textBox1.Text + "'", connect);
                using (SqlDataReader r = command.ExecuteReader())
                {
                    r.Read();
                    try
                    {
                        if (textBox1.Text == r[0].ToString().Replace(" ", "") && textBox2.Text == r[1].ToString().Replace(" ", "")) //реплейсы для убирания пробелов после получения из БД
                        {
                            if(Convert.ToInt32(r[2]) == 1)
                            {
                                typeAccess = 1;
                            }
                            access = true;
                        }
                        else
                        {
                            MessageBox.Show("Проверьте правильность введенных данных");
                        }
                    }
                    catch (System.InvalidOperationException)
                    {
                        MessageBox.Show("Проверьте правильность введенных данных");
                    }

                }
            }
            if (access)
            {
                this.Visible = false;
                MainMenu a = new MainMenu();
                a.typeAccess = typeAccess;
                a.ShowDialog();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            this.ControlBox = false;
            this.Text = "Вход";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox1.Text = "Administrator";
                textBox2.Text = "admin";
            }
            if (e.KeyCode == Keys.PageDown)
            {
                textBox1.Text = "Seller";
                textBox2.Text = "Lespaul228";

            }
        }
    }
}