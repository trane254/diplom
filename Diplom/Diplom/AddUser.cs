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
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.Text = "Добавление пользователя";
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT UserType FROM Users", connection);
                using (SqlDataReader r = command.ExecuteReader())
                {
                    while (r.Read())
                        {
                        if (int.Parse(r[0].ToString()) == 1)
                        {
                            comboBox1.Items.Add("Администраторы");
                        }
                        else
                        {
                            comboBox1.Items.Add("Продавцы");
                        }
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connection.Open();
                int type;
                if (comboBox1.SelectedItem.ToString() == "Администратор")
                {
                    type = 1;
                }
                else
                {
                    type = 0;
                }
                SqlCommand command = new SqlCommand($"INSERT Users (Login, Password, UserType) VALUES ('{textBox1.Text}', '{textBox2.Text}', 0)",connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Пользователь добавлен в базу данных");
            }
        }
    }
}
