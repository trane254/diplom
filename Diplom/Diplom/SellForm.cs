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
        public SellForm()
        {
            InitializeComponent();
        }
        private void ReloadAll()
        {
            using(SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
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
                using (SqlDataReader r = command.ExecuteReader()) //производители
                {
                    while (r.Read())
                    {
                        comboBox3.Items.Add(r[0].ToString());
                    }
                }
                
            }
        }

        private void SellForm_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ControlBox = false;
            this.Text = "Продажа";
            ReloadAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connect.Open();
                string cmd = $"SELECT Товар.ЦенаПродажи FROM Товар WHERE Название = '{comboBox3.SelectedItem.ToString()}'";
                //MessageBox.Show(cmd);
                SqlCommand command = new SqlCommand(cmd, connect);
                using (SqlDataReader r = command.ExecuteReader())
                {
                    r.Read();
                    textBox2.Text = r[0].ToString();
                    r.Close();
                }
            }
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
    }
}

//INSERT Продажи(Код, ДатаПоставки, Товар, ЦенаПродажи, Количество, Стоимость) VALUES(0, '13.06.2019', 0, 18000, 1, 18000);