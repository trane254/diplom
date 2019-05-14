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
        private void ReloadAll()
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();

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
            this.ControlBox = false;
            this.Text = "База данных";
            label1.Visible = false;
            ReloadAll();
        }

        private void button1_Click(object sender, EventArgs e) //добавление в базу
        {
            switch (selectedDatagrid)
            {
                case 1:
                    using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        int newID;
                        connect.Open();
                        SqlCommand command = new SqlCommand($"SELECT MAX(id_code) FROM Prods", connect);
                        SqlDataReader r = command.ExecuteReader();
                        r.Read();
                        try
                        {
                            newID = Convert.ToInt32(r[0]) + 1;
                            r.Close();
                            AddForm a = new AddForm();
                            a.selectedDatagrid = selectedDatagrid;
                            a.ShowDialog();
                            command.CommandText = $"INSERT Prods (id_code, prod) VALUES ({newID}, '{a.textboxtext1}')";
                            command.ExecuteNonQuery();
                            label1.Text = $"datagrid {selectedDatagrid}; addform {a.textboxtext1}";
                            ReloadAll();
                        }
                        catch (System.InvalidOperationException ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "Внутренняя ошибка");
                        }
                    }
                    break;
                case 2:
                    using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        int newID;
                        connect.Open();
                        SqlCommand command = new SqlCommand($"SELECT MAX(id_code) FROM Products", connect);
                        SqlDataReader r = command.ExecuteReader();
                        r.Read();
                        try
                        {
                            newID = Convert.ToInt32(r[0]) + 1;
                            r.Close();
                            AddForm a = new AddForm();
                            a.selectedDatagrid = selectedDatagrid;
                            a.ShowDialog();
                            command.CommandText = $"INSERT Products (id_code, manufacturer, name, price) VALUES ({newID}, {a.textboxtext1}, '{a.textboxtext2}', {a.textboxtext3})";
                            command.ExecuteNonQuery();
                            label1.Text = $"datagrid {selectedDatagrid}; addform {a.textboxtext2}";
                            ReloadAll();
                        }
                        catch (System.InvalidOperationException ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "Внутренняя ошибка");
                        }
                    }
                    break;
                case 3:
                    using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        int newID;
                        connect.Open();
                        SqlCommand command = new SqlCommand($"SELECT MAX(id_code) FROM Cliens", connect);
                        SqlDataReader r = command.ExecuteReader();
                        r.Read();
                        try
                        {
                            newID = Convert.ToInt32(r[0]) + 1;
                            r.Close();
                            AddForm a = new AddForm();
                            a.selectedDatagrid = selectedDatagrid;
                            a.ShowDialog();
                            command.CommandText = $"INSERT Cliens (id_code, FirstAndSecond_name, Adress, Phone) VALUES ({newID}, '{a.textboxtext1}', '{a.textboxtext2}', '{a.textboxtext3}')";
                            command.ExecuteNonQuery();
                            label1.Text = $"datagrid {selectedDatagrid}; addform {a.textboxtext3}";
                            ReloadAll();
                        }
                        catch (System.InvalidOperationException ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "Внутренняя ошибка");
                        }  
                    }
                    break;
                case 4:
                    using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        int newID;
                        connect.Open();
                        SqlCommand command = new SqlCommand($"SELECT MAX(id_code) FROM Orders", connect);
                        SqlDataReader r = command.ExecuteReader();
                        r.Read();
                        try
                        {
                            newID = Convert.ToInt32(r[0]) + 1;
                            r.Close();
                            AddForm a = new AddForm();
                            a.selectedDatagrid = selectedDatagrid;
                            a.ShowDialog();
                            command.CommandText = $"INSERT Orders (id_code, Client, product, Selldata) VALUES ({newID}, {a.textboxtext1}, {a.textboxtext2}, GETDATE());";
                            command.ExecuteNonQuery();
                            label1.Text = $"datagrid {selectedDatagrid}; addform {a.textboxtext3}";
                            ReloadAll();
                        }
                        catch (System.InvalidOperationException ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "Внутренняя ошибка");
                        }
                    }
                    break;
            }
        }
        private void button2_Click(object sender, EventArgs e) //удаление из базы
        {
            switch (selectedDatagrid)
            {
                case 1:
                     for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Selected)
                        {
                            label1.Text = $"selected {dataGridView1.CurrentCell.Value}";
                            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                            {
                                connect.Open();
                                SqlCommand command = new SqlCommand($"EXECUTE DeleteProds @id={dataGridView1.CurrentCell.Value}", connect);
                                command.ExecuteNonQuery();
                                ReloadAll();
                                break;
                            }
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (dataGridView2.Rows[i].Selected)
                        {
                            label1.Text = $"selected {dataGridView2.CurrentCell.Value}";
                            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                            {
                                connect.Open();
                                SqlCommand command = new SqlCommand($"EXECUTE DeleteProducts @id={dataGridView2.CurrentCell.Value}", connect);
                                command.ExecuteNonQuery();
                                ReloadAll();
                                break;
                            }
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        if (dataGridView3.Rows[i].Selected)
                        {
                            label1.Text = $"selected {dataGridView3.CurrentCell.Value}";
                            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                            {
                                connect.Open();
                                SqlCommand command = new SqlCommand($"EXECUTE DeleteCliens @id={dataGridView3.CurrentCell.Value}", connect);
                                command.ExecuteNonQuery();
                                ReloadAll();
                                break;

                            }
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < dataGridView4.Rows.Count; i++)
                    {
                        if (dataGridView4.Rows[i].Selected)
                        {
                            label1.Text = $"selected {dataGridView4.CurrentCell.Value}";
                            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
                            {
                                connect.Open();
                                SqlCommand command = new SqlCommand($"EXECUTE DeleteOrders @id={dataGridView4.CurrentCell.Value}", connect);
                                command.ExecuteNonQuery();
                                ReloadAll();
                                break;
                            }
                        }
                    }
                    break;
            }
        }
        private void button3_Click(object sender, EventArgs e) //закрытие окна работы с базой
        {
            this.Close();
        }
        private void button4_Click(object sender, EventArgs e) //редактирование
        {
            switch (selectedDatagrid)
            {
                case 1:
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Selected)
                        {
                            RedactForm a = new RedactForm();
                            label1.Text = $"selected {dataGridView1.CurrentCell.Value}";
                            a.selectedDatagrid = selectedDatagrid;
                            a.IDIndex = Convert.ToInt32(dataGridView1.CurrentCell.Value);
                            a.ShowDialog();
                            //label1.Text = $"redact {dataGridView1.CurrentRow.Index} + {dataGridView1.CurrentCell.Value}";
                            ReloadAll();
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (dataGridView2.Rows[i].Selected)
                        {
                            RedactForm a = new RedactForm();
                            label1.Text = $"selected {dataGridView2.CurrentCell.Value}";
                            a.selectedDatagrid = selectedDatagrid;
                            a.IDIndex = Convert.ToInt32(dataGridView2.CurrentCell.Value);
                            a.ShowDialog();
                            ReloadAll();
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        if (dataGridView3.Rows[i].Selected)
                        {
                            RedactForm a = new RedactForm();
                            label1.Text = $"selected {dataGridView3.CurrentCell.Value}";
                            a.selectedDatagrid = selectedDatagrid;
                            a.IDIndex = Convert.ToInt32(dataGridView3.CurrentCell.Value);
                            a.ShowDialog();
                            ReloadAll();
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < dataGridView4.Rows.Count; i++)
                    {
                        if (dataGridView4.Rows[i].Selected)
                        {
                            RedactForm a = new RedactForm();
                            label1.Text = $"selected {dataGridView4.CurrentCell.Value}";
                            a.selectedDatagrid = selectedDatagrid;
                            a.IDIndex = Convert.ToInt32(dataGridView4.CurrentCell.Value);
                            a.ShowDialog();
                            ReloadAll();
                        }
                    }
                    break;
            }
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

        private void button5_Click(object sender, EventArgs e)
        {
            ReloadAll();
        }
    }
}
