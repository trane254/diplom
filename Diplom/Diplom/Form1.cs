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
            bool acces = false;
            string[] Logins = { "Administrator", "Seller" };
            string[] Passwords = {"admin", "qwerty" };
            for(int i = 0; i < Logins.Length; i++)
            {
                if (textBox1.Text == Logins[i] && textBox2.Text == Passwords[i])
                {
                    acces = true;
                }
            }
            if (acces)
            {
                this.Visible = false;
                MainMenu a = new MainMenu();
                a.ShowDialog();
            }
            else
            {
                MessageBox.Show("Проверьте правильность ввода логина и пароля", "", MessageBoxButtons.OK);
            }


            //if (textBox1.Text == "admin" && textBox2.Text == "admin")
            //{
                
            //    MessageBox.Show("Done", "Connect", MessageBoxButtons.OK);
            //    this.Visible = false;
            //    MainMenu a = new MainMenu(); //добавить потом проверку, какой тип пользюка зашёл
            //    a.Show();

            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }
    }
}
