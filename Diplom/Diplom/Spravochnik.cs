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

            string cmd;
            if(radioButton1.Checked)
            {
                cmd = $"SELECT * FROM ПроизводителиТовара WHERE Код = (SELECT MAX(ПроизводителиТовара.Код) FROM ПроизводителиТовара)";
            }
            else
            {
                cmd = $"SELECT * FROM Категория WHERE Код = (SELECT MAX(Категория.Код) FROM Категория)";
            }
            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connect.Open();
                SqlCommand command = new SqlCommand(cmd, connect);
                using (SqlDataReader r = command.ExecuteReader())
                {
                    r.Read();
                    int id = Convert.ToInt32(r[0]) + 1;
                    r.Close();
                    if(radioButton1.Checked)
                    {
                        command.CommandText = $"SELECT * FROM ПроизводителиТовара WHERE Производитель = '{textBox1.Text}'";
                    }
                    else
                    {
                        command.CommandText = $"SELECT * FROM Категория WHERE Категория = '{textBox1.Text}'";
                    }
                    SqlDataReader checkDublicate = command.ExecuteReader();
                    checkDublicate.Read();
                    try
                    {
                        string s_ = checkDublicate[1].ToString();
                        MessageBox.Show(s_);
                    }
                    catch(System.InvalidOperationException ex)
                    {
                        //MessageBox.Show("нету в бд");
                        if (radioButton1.Checked)
                        {
                            checkDublicate.Close();
                            command.CommandText = $"EXECUTE AddManufacturer @id = {id}, @manufacturer = '{textBox1.Text}'";
                            command.ExecuteNonQuery();
                            return;
                        }
                        else
                        {
                            checkDublicate.Close();
                            command.CommandText = $"EXECUTE AddCategory @id = {id}, @category = '{textBox1.Text}'";
                            command.ExecuteNonQuery();
                            return;
                        }
                    }
                    catch
                    {
                        //MessageBox.Show("Тут");
                    }
                    MessageBox.Show("Элемент уже содержится в справочнике", "Ошибка");
                }
            }

        }

        private void Spravochnik_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            radioButton1.Checked = true;
        }
    }
}
