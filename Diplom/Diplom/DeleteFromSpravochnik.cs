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
    public partial class DeleteFromSpravochnik : Form
    {
        public DeleteFromSpravochnik()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DatagridLoad()
        {
            dataGridView1.Rows.Clear();
            if (radioButton1.Checked)
            {
                using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM ПроизводителиТовара", connect);
                    using (SqlDataReader r = command.ExecuteReader())
                    {
                        while(r.Read())
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            DataGridViewCell c1 = new DataGridViewTextBoxCell();
                            c1.Value = r[1].ToString();
                            row.Cells.AddRange(c1);
                            dataGridView1.Rows.Add(row);
                        }
                    }
                }
            }
            else
            {
                using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Категория", connect);
                    using (SqlDataReader r = command.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            DataGridViewCell c1 = new DataGridViewTextBoxCell();
                            c1.Value = r[1].ToString();
                            row.Cells.AddRange(c1);
                            dataGridView1.Rows.Add(row);
                        }
                    }
                }
            }
        }

        private void DeleteFromSpravochnik_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            radioButton1.Checked = true;
            DatagridLoad();
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            DatagridLoad();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            DatagridLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(dataGridView1.SelectedCells[0].Value.ToString());
            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connect.Open();
                string cmd;
                if(radioButton1.Checked)
                {
                    cmd = $"DELETE Категория WHERE ПроизводительТовара = '{dataGridView1.SelectedCells[0].Value.ToString()}'";
                }
                else
                {
                    cmd = $"DELETE Категория WHERE Категория = '{dataGridView1.SelectedCells[0].Value.ToString()}'";
                }
                SqlCommand command = new SqlCommand($"DELETE Категория WHERE Категория = '{dataGridView1.SelectedCells[0].Value.ToString()}'", connect);
                command.ExecuteNonQuery();
                MessageBox.Show("Добавлено в базу данных");
            }
        }
    }
}
