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
    public partial class RedactForm : Form
    {
        private int idIndex;
        public int IDIndex
        {
            get 
            {
                return idIndex;
            }
            set
            {
                idIndex = value;
            }
        }

        private int slDg;
        public int selectedDatagrid
        {
            get
            {
                return selectedDatagrid;
            }

            set
            {
                slDg = value;
            }
        }

        public string textboxtext1
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textboxtext1 = value;
            }
        }
        public string textboxtext2
        {
            get
            {
                return textBox2.Text;
            }
            set
            {
                textboxtext2 = value;
            }
        }
        public string textboxtext3
        {
            get
            {
                return textBox3.Text;
            }
        }
        public string textboxtext4
        {
            get
            {
                return textBox4.Text;
            }
        }
        public string textboxtext5
        {
            get
            {
                return textBox5.Text;
            }
            set
            {
                textboxtext5 = value;
            }
        }



        public RedactForm()
        {
            InitializeComponent();
        }

        private void RedactForm_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.Text = "Форма редактирования";
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            switch (slDg)
            {
                case 1:
                    label1.Visible = true;
                    label1.Text = "Производитель: ";
                    textBox1.Visible = true;
                    using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connect.Open();
                        using (SqlCommand command = new SqlCommand($"SELECT * FROM Prods WHERE id_code = {idIndex}", connect))
                        {
                            SqlDataReader r = command.ExecuteReader();
                            r.Read();
                            textBox1.Text = r[1].ToString().Replace(" ","");
                        }
                    }
                    break;
                case 2:
                    label1.Visible = true;
                    label1.Text = "Производитель: ";
                    textBox1.Visible = true;
                    label2.Visible = true;
                    label2.Text = "Название: ";
                    textBox2.Visible = true;
                    label3.Visible = true;
                    label3.Text = "Цена: ";
                    textBox3.Visible = true;
                    using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connect.Open();
                        using (SqlCommand command = new SqlCommand($"SELECT * FROM Products WHERE id_code = {idIndex}", connect))
                        {
                            SqlDataReader r = command.ExecuteReader();
                            r.Read();
                            textBox1.Text = r[1].ToString().Replace(" ", "");
                            textBox2.Text = r[2].ToString().Replace(" ", "");
                            textBox3.Text = r[3].ToString().Replace(" ", "");
                        }
                    }
                    break;

                case 3:
                    label1.Visible = true;
                    label1.Text = "ФИО: ";
                    textBox1.Visible = true;
                    label2.Visible = true;
                    label2.Text = "Адрес: ";
                    textBox2.Visible = true;
                    label3.Visible = true;
                    label3.Text = "Телефон: ";
                    textBox3.Visible = true;
                    using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connect.Open();
                        using (SqlCommand command = new SqlCommand($"SELECT * FROM Cliens WHERE id_code = {idIndex}", connect))
                        {
                            SqlDataReader r = command.ExecuteReader();
                            r.Read();
                            textBox1.Text = r[1].ToString().Replace(" ", "");
                            textBox2.Text = r[2].ToString().Replace(" ", "");
                            textBox3.Text = r[3].ToString().Replace(" ", "");
                        }
                    }
                break;

                case 4:
                    label1.Visible = true;
                    label1.Text = "Клиент: ";
                    textBox1.Visible = true;
                    label2.Visible = true;
                    label2.Text = "Продукт: ";
                    textBox2.Visible = true;
                    label3.Visible = true;
                    label3.Text = "Дата продажи: ";
                    textBox3.Visible = true;
                    using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connect.Open();
                        using (SqlCommand command = new SqlCommand($"SELECT * FROM Orders WHERE id_code = {idIndex}", connect))
                        {
                            SqlDataReader r = command.ExecuteReader();
                            r.Read();
                            textBox1.Text = r[1].ToString().Replace(" ", "");
                            textBox2.Text = r[2].ToString().Replace(" ", "");
                            textBox3.Text = r[3].ToString().Replace(" ", "");
                        }
                    }
                    //UPDATE Orders SET Client = 0, product = 0, Selldata = '' WHERE id_code = 0;

                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (slDg)
            {
                case 1:
                    using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connect.Open();
                        using (SqlCommand command = new SqlCommand($"UPDATE Prods SET prod = '{textBox1.Text}' WHERE id_code = {idIndex};", connect))
                        {
                            command.ExecuteNonQuery();
                            this.Close();
                        }
                    }
                    break;

                case 2:
                    
                    using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connect.Open();
                        using (SqlCommand command = new SqlCommand($"UPDATE Products SET manufacturer = {textBox1.Text}, name = '{textBox2.Text}', price = {textBox3.Text} WHERE id_code = {idIndex};", connect))
                        {
                            command.ExecuteNonQuery();
                            this.Close();
                        }
                    }
                break;

                case 3:
                    using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connect.Open();
                        using (SqlCommand command = new SqlCommand($"UPDATE Cliens SET FirstAndSecond_name = '{textBox1.Text}', Adress = '{textBox2.Text}', Phone = '{textBox3.Text}' WHERE id_code = {idIndex};", connect))
                        {
                            command.ExecuteNonQuery();
                            this.Close();
                        }
                    }
                    break;

                case 4:
                    using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connect.Open();
                        using (SqlCommand command = new SqlCommand($"UPDATE Orders SET Client = {textBox1.Text}, product = {textBox2.Text}, Selldata = '{textBox3.Text}' WHERE id_code = {idIndex};", connect))
                        {
                            command.ExecuteNonQuery();
                            this.Close();
                        }
                    }
                    break;
            }
        }
    }
}
