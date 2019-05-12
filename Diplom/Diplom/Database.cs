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
    public partial class Database : Form
    {
        int selectedDatagrid; //выбор нужного datagridview

        public Database()
        {
            InitializeComponent();
            
        }
        private void LoadAll()
        {
            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connect.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Prods", connect);
                using (SqlDataReader r = command.ExecuteReader())
                {
                    while(r.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewCell c1 = new DataGridViewTextBoxCell();
                        c1.Value = r[0].ToString();
                        row.Cells.Add(c1);
                        DataGridViewCell c2 = new DataGridViewTextBoxCell();
                        c2.Value = r[1].ToString();
                        row.Cells.Add(c2);
                        dataGridView1.Rows.Add(row);
                    }

                }
                command.CommandText = "SELECT * FROM Products";
                using (SqlDataReader r = command.ExecuteReader())
                {
                    while(r.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewCell c1 = new DataGridViewTextBoxCell();
                        c1.Value = r[0].ToString();
                        row.Cells.Add(c1);
                        DataGridViewCell c2 = new DataGridViewTextBoxCell();
                        c2.Value = r[1].ToString();
                        row.Cells.Add(c2);
                        DataGridViewCell c3 = new DataGridViewTextBoxCell();
                        c3.Value = r[2].ToString();
                        row.Cells.Add(c3);
                        DataGridViewCell c4 = new DataGridViewTextBoxCell();
                        c4.Value = r[3].ToString();
                        row.Cells.Add(c4);
                        dataGridView2.Rows.Add(row);
                    }
                }
                command.CommandText = "SELECT * FROM Cliens";
                using (SqlDataReader r = command.ExecuteReader())
                {
                    while (r.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewCell c1 = new DataGridViewTextBoxCell();
                        c1.Value = r[0].ToString();
                        row.Cells.Add(c1);
                        DataGridViewCell c2 = new DataGridViewTextBoxCell();
                        c2.Value = r[1].ToString();
                        row.Cells.Add(c2);
                        DataGridViewCell c3 = new DataGridViewTextBoxCell();
                        c3.Value = r[2].ToString();
                        row.Cells.Add(c3);
                        DataGridViewCell c4 = new DataGridViewTextBoxCell();
                        c4.Value = r[3].ToString();
                        row.Cells.Add(c4);
                        dataGridView3.Rows.Add(row);
                    }
                }
                command.CommandText = "SELECT * FROM Orders";
                using (SqlDataReader r = command.ExecuteReader())
                {
                    while (r.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewCell c1 = new DataGridViewTextBoxCell();
                        c1.Value = r[0].ToString();
                        row.Cells.Add(c1);
                        DataGridViewCell c2 = new DataGridViewTextBoxCell();
                        c2.Value = r[1].ToString();
                        row.Cells.Add(c2);
                        DataGridViewCell c3 = new DataGridViewTextBoxCell();
                        c3.Value = r[2].ToString();
                        row.Cells.Add(c3);
                        DataGridViewCell c4 = new DataGridViewTextBoxCell();
                        c4.Value = r[3].ToString();
                        row.Cells.Add(c4);
                        dataGridView4.Rows.Add(row);
                    }
                }
            }
        }

        private void Database_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void button1_Click(object sender, EventArgs e) //добавление в базу, потом сделаю
        {
            switch (selectedDatagrid)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    label1.Text = $"selected {dataGridView1.CurrentRow.Index}";
                    using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connect.Open();
                        SqlCommand command = new SqlCommand($"EXECUTE DeleteProds @id={dataGridView1.CurrentRow.Index}", connect);
                        command.ExecuteNonQuery();
                        //MessageBox.Show("df","sf");
                        break;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            selectedDatagrid = 1;
            label1.Text = $"datagrid {selectedDatagrid}";
        }
        private void dataGridView2_Click(object sender, EventArgs e)
        {
            selectedDatagrid = 2;
            label1.Text = $"datagrid {selectedDatagrid}";
        }
        private void dataGridView3_Click(object sender, EventArgs e)
        {
            selectedDatagrid = 3;
            label1.Text = $"datagrid {selectedDatagrid}";
        }
        private void dataGridView4_Click(object sender, EventArgs e)
        {
            selectedDatagrid = 4;
            label1.Text = $"datagrid {selectedDatagrid}";
        }



    }
}
