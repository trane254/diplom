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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }
        private void ReloadAll()
        {
            dataGridView1.Rows.Clear();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Users", connection);
                using (SqlDataReader r = command.ExecuteReader())
                {
                    while (r.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewCell c1 = new DataGridViewTextBoxCell();
                        c1.Value = r[0].ToString();
                        DataGridViewCell c2 = new DataGridViewTextBoxCell();
                        c2.Value = r[1].ToString();
                        DataGridViewCell c3 = new DataGridViewTextBoxCell();
                        c3.Value = r[2].ToString();
                        row.Cells.AddRange(c1, c2, c3);
                        dataGridView1.Rows.Add(row);
                    }
                }
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.connectionString;
            ReloadAll();
            this.ControlBox = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.ReadOnly = false;
            }
            else
            {
                textBox1.ReadOnly = true;
            }
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddUser a = new AddUser();
            a.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connect.Open();
                SqlCommand command = new SqlCommand($"DELETE Users WHERE Login = '{dataGridView1.SelectedCells[0].Value.ToString()}'", connect);
                command.ExecuteNonQuery();
            }
            ReloadAll();
            MessageBox.Show("Пользователь удален");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.connectionString = textBox1.Text;
        }
    }
}
