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
    public partial class Postavka : Form
    {
        public Postavka()
        {
            InitializeComponent();
        }

        private void Postavka_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.Text = "Оформление поставки";
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ReloadAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void ReloadAll()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Категория", connection);
                using (SqlDataReader r = command.ExecuteReader()) //категории
                {
                    while (r.Read())
                    {
                        comboBox1.Items.Add(r[1].ToString().Replace(" ", ""));
                    }
                }
                command.CommandText = "SELECT * FROM ПроизводителиТовара";
                using (SqlDataReader r = command.ExecuteReader()) //производители
                {
                    while (r.Read())
                    {
                        comboBox2.Items.Add(r[1].ToString().Replace(" ", ""));
                    }
                }
            }
        }

        private bool checkInBase()
        {
            bool check = false;
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT Название FROM Товар WHERE Название = '{textBox1.Text}'", connection);
                using (SqlDataReader r = command.ExecuteReader())
                {
                    try
                    {
                        r.Read();
                        string s = textBox1.Text;
                        string a = "";
                        if (s.Length < 30)
                        {
                            for(int i = 0; i < 30 - s.Length; i++)
                            {
                                a += " ";
                            }
                        }
                        s = s + a;
                        if (s == r[0].ToString())
                        {
                            check = true;
                        }
                    }
                    catch
                    {
                        check = false;
                    }
                }
            }
            return check;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int newIDpostavki, newIDTovara, idProizvoditelya, idCategory;
                string proizvoditel, category;
                category = comboBox1.SelectedItem.ToString();
                proizvoditel = comboBox2.SelectedItem.ToString();
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT Код FROM Поставки WHERE Код = (SELECT MAX(Код) FROM Поставки)", connection);
                    using (SqlDataReader r = command.ExecuteReader())
                    {
                        r.Read();
                        newIDpostavki = int.Parse(r[0].ToString()) + 1;
                        r.Close();
                    }
                    command.CommandText = "SELECT Товар.Код FROM Товар WHERE Код = (SELECT MAX(Товар.Код) FROM Товар)";
                    using (SqlDataReader r = command.ExecuteReader())
                    {
                        r.Read();
                        newIDTovara = int.Parse(r[0].ToString()) + 1;
                        r.Close();
                    }

                    command.CommandText = $"SELECT ПроизводителиТовара.Код FROM ПроизводителиТовара WHERE Производитель = '{proizvoditel}'";
                    using (SqlDataReader r = command.ExecuteReader())
                    {
                        r.Read();
                        idProizvoditelya = int.Parse(r[0].ToString());
                        r.Close();
                    }
                    command.CommandText = $"SELECT Код FROM Категория WHERE Категория = '{category}'";
                    using (SqlDataReader r = command.ExecuteReader())
                    {
                        r.Read();
                        idCategory = int.Parse(r[0].ToString());
                        r.Close();
                    }
                    if (checkInBase() == false)
                    {
                        command.CommandText = $"INSERT Товар (Код, Производитель, Категория, Название, ЦенаПродажи, КоличествоНаСкладе) VALUES ({newIDTovara}, {idProizvoditelya}, {idCategory}, '{textBox1.Text}', {textBox3.Text}, {textBox4.Text})";
                        command.ExecuteNonQuery();
                        command.CommandText = $"INSERT Поставки (Код, ДатаПоставки, Товар, Цена, Количество, Стоимость) VALUES ({newIDpostavki}, GETDATE(), {newIDTovara}, {textBox2.Text}, {textBox4.Text}, {textBox5.Text})";
                        command.ExecuteNonQuery();
                        MessageBox.Show("Добавлено в базу данных");
                        //сначала товар, потом поставка
                    }
                    else
                    {
                        int countInBase, IDTovara;
                        command.CommandText = $"SELECT КоличествоНаСкладе, Код FROM Товар WHERE Название = '{textBox1.Text}'";
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            r.Read();
                            countInBase = int.Parse(r[0].ToString());
                            IDTovara = int.Parse(r[1].ToString());
                            r.Close();
                        }
                        command.CommandText = $"UPDATE Товар SET КоличествоНаСкладе = {countInBase + int.Parse(textBox4.Text)} WHERE Название = '{textBox1.Text}'";
                        command.ExecuteNonQuery();
                        command.CommandText = $"INSERT Поставки (Код, ДатаПоставки, Товар, Цена, Количество, Стоимость) VALUES ({newIDpostavki}, GETDATE(), {IDTovara}, {textBox2.Text}, {textBox4.Text}, {textBox5.Text})";
                        command.ExecuteNonQuery();
                        MessageBox.Show("Добавлено в базу данных");
                        //сначала товар, потом поставка
                    }
                }
                //MessageBox.Show("Добавлено в базу данных");
            }
            catch
            {
                MessageBox.Show("Проверьте правильность ввода данных или вызовите администратора", "Ошибка");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkInBase() == false)
            {
                //command.CommandText = $"INSERT Товар (Код, Производитель, Категория, Название, ЦенаПродажи, КоличествоНаСкладе) VALUES ({newIDTovara}, {idProizvoditelya}, {idCategory}, '{textBox1.Text}', {textBox3.Text}, {textBox4.Text})";
                //сначала товар, потом поставка
                MessageBox.Show("Нет в базе");
            }
            else
            {

            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox5.Text = (int.Parse(textBox2.Text) * int.Parse(textBox4.Text)).ToString();
            }
            catch
            {
                textBox5.Text = "";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox5.Text = (int.Parse(textBox2.Text) * int.Parse(textBox4.Text)).ToString();
            }
            catch
            {
                textBox5.Text = "";
            }
        }
    }
}
