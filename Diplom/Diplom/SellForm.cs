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
    public partial class SellForm : Form
    {
        int autoFill;
        public SellForm()
        {
            InitializeComponent();
        }
        private void ReloadAll_noAutofill()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connect.Open();
                string cmd = "SELECT * FROM Категория";
                SqlCommand command = new SqlCommand(cmd, connect);
                using (SqlDataReader r = command.ExecuteReader()) //категории
                {
                    while(r.Read())
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
                command.CommandText = "SELECT Товар.Название FROM Товар";
                using (SqlDataReader r = command.ExecuteReader()) //названия товаров
                {
                    while (r.Read())
                    {
                        comboBox3.Items.Add(r[0].ToString());
                    }
                }
                
            }
        }

        private void autoFillPreload()
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connect.Open();
                string cmd = "SELECT * FROM Категория";
                SqlCommand command = new SqlCommand(cmd, connect);
                using (SqlDataReader r = command.ExecuteReader()) //категории
                {
                    while (r.Read())
                    {
                        comboBox1.Items.Add(r[1].ToString().Replace(" ", ""));
                    }
                }
            }
        }

        private void autoFillStepOne(object sender, EventArgs e)
        {

            if (autoFill == 1)
            {
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                textBox2.Text = "";
                textBox4.Text = "";
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString)) //производитель
                {
                    connection.Open();
                    int IDCategory;
                    List<int> numbers = new List<int>();
                    SqlCommand command = new SqlCommand($"SELECT Код FROM Категория WHERE Категория = '{comboBox1.SelectedItem.ToString()}'", connection);
                    using (SqlDataReader r = command.ExecuteReader())
                    {
                        r.Read();
                        IDCategory = int.Parse(r[0].ToString());
                        r.Close();
                    }
                    command.CommandText = $"SELECT Производитель FROM Товар WHERE Категория = {IDCategory}";
                    using (SqlDataReader r = command.ExecuteReader())
                    {
                        while(r.Read())
                        {
                            numbers.Add(int.Parse(r[0].ToString()));
                        }
                    }
                    for(int i = 0; i < numbers.Count(); i++)
                    {
                        command.CommandText = $"SELECT Производитель FROM ПроизводителиТовара WHERE Код = {numbers[i]}";
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            r.Read();
                            comboBox2.Items.Add(r[0].ToString());
                            r.Close();
                        }
                    }
                }
            }
        }

        private void autoFillStepTwo(object sender, EventArgs e)
        {

            if (autoFill == 1)
            {
                int IDCategory, IDManufactur;
                comboBox3.Items.Clear();
                textBox2.Text = "";
                textBox4.Text = "";
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($"SELECT Код FROM Категория WHERE Категория = '{comboBox1.SelectedItem.ToString()}'", connection); //получаем ID категории
                    using (SqlDataReader r = command.ExecuteReader())
                    {
                        r.Read();
                        IDCategory = int.Parse(r[0].ToString());
                        r.Close();
                    }
                    command.CommandText = $"SELECT Код FROM ПроизводителиТовара WHERE Производитель = '{comboBox2.SelectedItem.ToString()}'"; //получаем ID производителя
                    using (SqlDataReader r = command.ExecuteReader())
                    {
                        r.Read();
                        IDManufactur = int.Parse(r[0].ToString());
                        r.Close();
                    }
                    command.CommandText = $"SELECT Название FROM Товар WHERE Производитель = {IDManufactur} AND Категория = {IDCategory}";
                    using (SqlDataReader r = command.ExecuteReader())
                    {
                        while(r.Read())
                        {
                            comboBox3.Items.Add(r[0].ToString());
                        }
                    }
                }
            }
        }

        private void SellForm_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            checkBox1.Checked = true;
            if (checkBox1.Checked == true)
            {
                autoFill = 1;
                autoFillPreload();
            }
            else
            {
                autoFill = 0;
                ReloadAll_noAutofill();
            }
            this.ControlBox = false;
            this.Text = "Продажа";
            //ReloadAll_noAutofill();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox4.Text = (int.Parse(textBox2.Text) * int.Parse(textBox3.Text)).ToString();
            }
            catch
            {
                textBox4.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connect.Open();
                string cmd = "SELECT Товар.Код FROM Товар WHERE Код = (SELECT MAX(Товар.Код) FROM Товар)";
                int newID, tovarID, naSklade;
                SqlCommand command = new SqlCommand(cmd, connect);
                using (SqlDataReader r = command.ExecuteReader()) //категории
                {
                    r.Read();
                    newID = int.Parse(r[0].ToString());
                    r.Close();
                }
                command.CommandText = $"SELECT Товар.Код FROM Товар WHERE Название = '{comboBox3.SelectedItem}'";
                using (SqlDataReader r = command.ExecuteReader())
                {
                    r.Read();
                    tovarID = int.Parse(r[0].ToString());
                    r.Close();
                }
                command.CommandText = $"SELECT Товар.КоличествоНаСкладе FROM Товар WHERE Товар.Название = '{comboBox3.SelectedItem}'";
                using (SqlDataReader r = command.ExecuteReader())
                {
                    r.Read();
                    naSklade = int.Parse(r[0].ToString());
                    r.Close();
                }
                if (naSklade - int.Parse(textBox3.Text) <= 0)
                {
                    MessageBox.Show($"Нехватает товаров на складе \n На складе {naSklade}", "Ошибка");
                    return;
                }
                else
                {
                    command.CommandText = $"UPDATE Товар SET КоличествоНаСкладе = {naSklade - int.Parse(textBox3.Text)} WHERE Название = '{comboBox3.SelectedItem}'";
                    command.ExecuteNonQuery();
                    command.CommandText = $"INSERT Продажи(Код, ДатаПоставки, Товар, ЦенаПродажи, Количество, Стоимость) VALUES({newID}, GETDATE(), {tovarID}, {textBox2.Text}, {textBox3.Text}, {textBox4.Text})";
                    command.ExecuteNonQuery();
                    MessageBox.Show("Добавлено в базу данных");
                }

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                //comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                textBox2.Text = "";
                textBox4.Text = "";
                autoFill = 1;
            }
            else
            {
                autoFill = 0;
                //comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                textBox2.Text = "";
                textBox4.Text = "";
                ReloadAll_noAutofill();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            using(SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT ЦенаПродажи FROM Товар WHERE Название = '{comboBox3.SelectedItem.ToString()}'", connection);
                using (SqlDataReader r = command.ExecuteReader())
                {
                    r.Read();
                    textBox2.Text = r[0].ToString();
                    r.Close();
                }
            }
        }
    }
}

//INSERT Продажи(Код, ДатаПоставки, Товар, ЦенаПродажи, Количество, Стоимость) VALUES(0, '13.06.2019', 0, 18000, 1, 18000);