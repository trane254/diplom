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

            if (Properties.Settings.Default.LoginType == "Client")
            {

                string[] Logins = { "Administrator", "Seller" };
                string[] Passwords = { "admin", "qwerty" };
                for (int i = 0; i < Logins.Length; i++)
                {
                    if (textBox1.Text == Logins[i] && textBox2.Text == Passwords[i])
                    {
                        access = true;
                        break;
                    }
                }
                if (access == false)
                {
                    MessageBox.Show("Проверьте правильность введенных данных");
                }
                
            }
            else//серверный вход
            {
                using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Login = '" + textBox1.Text + "'", connect);
                    using (SqlDataReader r = command.ExecuteReader())
                    {
                        r.Read();
                        try 
                        {
                            if (textBox1.Text == r[0].ToString().Replace(" ","") && textBox2.Text == r[1].ToString().Replace(" ","")) //реплейсы для убирания пробелов после получения из БД
                            {
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
            }
            if (access)
            {
                this.Visible = false;
                MainMenu a = new MainMenu();
                a.ShowDialog();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.Text = "Вход";
            Database a = new Database();
            a.ShowDialog();
        }
    }
}
