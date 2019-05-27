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
    public partial class Spravochnik : Form
    {
        public Spravochnik()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) //производители
            {
                using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand($"SELECT * FROM ПроизводителиТовара WHERE Код = (SELECT MAX(ПроизводителиТовара.Код) FROM ПроизводителиТовара)", connect);
                    try
                    {
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            r.Read();
                            int id = Convert.ToInt32(r[0]) + 1;
                            r.Close();
                            command.CommandText = $"SELECT * FROM ПроизводителиТовара WHERE Производитель = '{textBox1.Text}'";
                            SqlDataReader CheckDublicate = command.ExecuteReader();
                            CheckDublicate.Read();
                            string s = CheckDublicate[1].ToString().Replace(" ", "");
                            if (textBox1.Text == s)
                            {
                                MessageBox.Show("Этот производитель уже содержится в базе", "Ошибка");
                                return;
                            }
                            CheckDublicate.Close();
                            command.CommandText = $"EXECUTE AddManufacturer @id = {id}, @manufacturer = '{textBox1.Text}'";
                            command.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка", "");
                        return;
                    }
                }
            }

            else //категория
            {
                using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand($"SELECT * FROM Категория WHERE Код = (SELECT MAX(Категория.Код) FROM Категория)", connect);
                    try
                    {
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            r.Read();
                            int id = Convert.ToInt32(r[0]) + 1;
                            r.Close();
                            command.CommandText = $"SELECT * FROM Категория WHERE Категория = '{textBox1.Text}'";
                            SqlDataReader CheckDublicate = command.ExecuteReader();
                            CheckDublicate.Read();
                            string s = CheckDublicate[1].ToString().Replace(" ", "");
                            if (textBox1.Text == s)
                            {
                                MessageBox.Show("Эта категория уже содержится в базе", "Ошибка");
                                return;
                            }
                            CheckDublicate.Close();
                            command.CommandText = $"EXECUTE AddCategory @id = {id}, @category = '{textBox1.Text}'";
                            command.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка", "");
                        return;
                    }
                }
            }
        }

        private void Spravochnik_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            textBox1.Text = "Струны";
        }
    }
}
