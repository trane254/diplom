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
        public RedactForm()
        {
            InitializeComponent();
        }

        private int TypeRedact;
        public int typeredact
        {
            get
            {
                return TypeRedact;
            }
            set
            {
                TypeRedact = value;
            }
        }
        public string IDCode;

        private string ReturnFullNameTovar(string value_)
        {
            string FullNameTovar = "";
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand($"SELECT Производитель, Категория FROM Товар WHERE Код = {int.Parse(value_)}", connection))
                {
                    int idmanufact, idcategory;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        idmanufact = int.Parse(reader[0].ToString());
                        idcategory = int.Parse(reader[1].ToString());
                        reader.Close();
                    }

                    cmd.CommandText = $"SELECT Категория FROM Категория WHERE Код = {idcategory}";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        FullNameTovar += reader[0].ToString().Replace(" ", "") + " ";
                        reader.Close();
                    }

                    cmd.CommandText = $"SELECT Производитель FROM ПроизводителиТовара WHERE Код = {idmanufact}";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        FullNameTovar += reader[0].ToString().Replace(" ", "") + " ";
                        reader.Close();
                    }
                    cmd.CommandText = $"SELECT Название FROM Товар WHERE Код = {value_}";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        FullNameTovar += reader[0].ToString();
                        reader.Close();
                    }
                }
            }
            return FullNameTovar;
        }

        private string ReturnManufact(int value)
        {
            //SELECT Производитель FROM ПроизводителиТовара WHERE Код =
            string s;
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT Производитель FROM ПроизводителиТовара WHERE Код = {value}", connection);
                using (SqlDataReader r = command.ExecuteReader())
                {
                    r.Read();
                    s = r[0].ToString();
                    r.Close();
                }
            }
            return s;
        }

        private string ReturnCategory(int value)
        {
            //SELECT Производитель FROM ПроизводителиТовара WHERE Код =
            string s;
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT Категория FROM Категория WHERE Код = {value}", connection);
                using (SqlDataReader r = command.ExecuteReader())
                {
                    r.Read();
                    s = r[0].ToString();
                    r.Close();
                }
            }
            return s;
        }

        private int ReturnCategoryID(string s)
        {
            int a;
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT Код FROM Категория WHERE Категория = '{s}'", connection);
                using (SqlDataReader r = command.ExecuteReader())
                {
                    r.Read();
                    a = int.Parse(r[0].ToString());
                    r.Close();
                }
            }
            return a;
        }

        private int ReturnManufactID(string s)
        {
            int a;
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT Код FROM ПроизводителиТовара  WHERE Производитель = '{s}'", connection);
                using (SqlDataReader r = command.ExecuteReader())
                {
                    r.Read();
                    a = int.Parse(r[0].ToString());
                    r.Close();
                }
            }
            return a;
        }

        //private ReturnID

        private void RedactForm_Load(object sender, EventArgs e) ////////////////////////////////
        {
            this.ControlBox = false;
            this.Text = "Редактирование ";
            string FullNameTovar = "";
            List<string> category = new List<string>();
            List<string> manufact = new List<string>();
            DataGridViewTextBoxColumn dgv1 = new DataGridViewTextBoxColumn();
            dgv1.Name = "Column1";
            DataGridViewTextBoxColumn dgv2 = new DataGridViewTextBoxColumn();
            dgv2.Name = "Column2";
            DataGridViewTextBoxColumn dgv3 = new DataGridViewTextBoxColumn();
            dgv3.Name = "Column3";
            DataGridViewTextBoxColumn dgv4 = new DataGridViewTextBoxColumn();
            dgv4.Name = "Column4";
            DataGridViewTextBoxColumn dgv5 = new DataGridViewTextBoxColumn();
            dgv5.Name = "Column5";
            DataGridViewTextBoxColumn dgv6 = new DataGridViewTextBoxColumn();
            dgv6.Name = "Column6";
            DataGridViewTextBoxColumn dgv7 = new DataGridViewTextBoxColumn();
            dgv7.Name = "Column7";
            DataGridViewTextBoxColumn dgv8 = new DataGridViewTextBoxColumn();
            dgv8.Name = "Column8";

            switch (TypeRedact)
            {
                case 1://категория
                    this.Text += "категорий";
                    dgv1.HeaderText = "IDCode";
                    dgv2.HeaderText = "Категория";
                    dataGridView1.Columns.AddRange(dgv1, dgv2);
                    dataGridView1.Columns[0].Visible = false;
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT * FROM Категория", connection);
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                DataGridViewTextBoxCell c1 = new DataGridViewTextBoxCell();
                                c1.Value = r[0].ToString();
                                DataGridViewTextBoxCell c2 = new DataGridViewTextBoxCell();
                                c2.Value = r[1];
                                row.Cells.AddRange(c1, c2);
                                dataGridView1.Rows.Add(row);
                            }
                        }
                    }
                    break;
                case 2:
                    this.Text += "производителей";
                    dgv1.HeaderText = "IDCode";
                    dgv2.HeaderText = "Производитель";
                    dataGridView1.Columns.AddRange(dgv1, dgv2);
                    dataGridView1.Columns[0].Visible = false;
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT * FROM ПроизводителиТовара", connection);
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                DataGridViewTextBoxCell c1 = new DataGridViewTextBoxCell();
                                c1.Value = r[0].ToString();
                                DataGridViewTextBoxCell c2 = new DataGridViewTextBoxCell();
                                c2.Value = r[1].ToString();
                                row.Cells.AddRange(c1, c2);
                                dataGridView1.Rows.Add(row);
                            }
                        }
                    }
                    break;
                case 3:
                    this.Text += " поставок";
                    dgv1.HeaderText = "Дата поставки";
                    dgv2.HeaderText = "Товар";
                    dgv3.HeaderText = "Цена закупки";
                    dgv4.HeaderText = "Количество";
                    dgv5.HeaderText = "Стоимость";
                    dgv6.HeaderText = "IDCode";
                    dgv7.HeaderText = "Категория";
                    dgv8.HeaderText = "Производитель";
                    dataGridView1.Columns.AddRange(dgv6, dgv1, dgv2, dgv3, dgv4, dgv5);
                    dataGridView1.Columns[0].Visible = false;
                    //dataGridView1.Columns[1].Visible = false;
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT * FROM Поставки", connection);
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                DataGridViewTextBoxCell c1 = new DataGridViewTextBoxCell();
                                c1.Value = r[0].ToString();
                                DataGridViewTextBoxCell c2 = new DataGridViewTextBoxCell();
                                c2.Value = r[1].ToString().Remove(10);
                                dgv2.Width = 350;
                                DataGridViewTextBoxCell c3 = new DataGridViewTextBoxCell();
                                c3.Value = ReturnFullNameTovar(r[2].ToString());
                                DataGridViewTextBoxCell c4 = new DataGridViewTextBoxCell();
                                c4.Value = r[3].ToString();
                                DataGridViewTextBoxCell c5 = new DataGridViewTextBoxCell();
                                c5.Value = r[4].ToString();
                                DataGridViewTextBoxCell c6 = new DataGridViewTextBoxCell();
                                c6.Value = int.Parse(c4.Value.ToString()) * int.Parse(c5.Value.ToString());
                                
                                row.Cells.AddRange(c1, c2, c3, c4, c5, c6);
                                dataGridView1.Rows.Add(row);
                            }
                        }
                    }
                    dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[5].ReadOnly = true;
                    break;
                case 4:
                    this.Text += " продаж";
                    FullNameTovar = "";
                    dgv1.HeaderText = "Дата поставки";
                    dgv2.HeaderText = "Товар";
                    dgv3.HeaderText = "Цена продажи";
                    dgv4.HeaderText = "Количество";
                    dgv5.HeaderText = "Стоимость";
                    dgv6.HeaderText = "IDCode";
                    dgv7.HeaderText = "IDCodeTovara";
                    dataGridView1.Columns.AddRange(dgv6, dgv1, dgv2, dgv3, dgv4, dgv5, dgv7);
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT * FROM Продажи", connection);
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                DataGridViewTextBoxCell c1 = new DataGridViewTextBoxCell();
                                c1.Value = r[0].ToString();
                                DataGridViewTextBoxCell c2 = new DataGridViewTextBoxCell();
                                c2.Value = r[1].ToString().Remove(10);
                                DataGridViewTextBoxCell c3 = new DataGridViewTextBoxCell();
                                c3.Value = ReturnFullNameTovar(r[2].ToString());
                                //MessageBox.Show(r[2].ToString());
                                dgv2.Width = 350;
                                DataGridViewTextBoxCell c4 = new DataGridViewTextBoxCell();
                                c4.Value = r[3].ToString();
                                DataGridViewTextBoxCell c5 = new DataGridViewTextBoxCell();
                                c5.Value = r[4].ToString();
                                DataGridViewTextBoxCell c6 = new DataGridViewTextBoxCell();
                                c6.Value = int.Parse(c4.Value.ToString()) * int.Parse(c5.Value.ToString());
                                DataGridViewTextBoxCell c7 = new DataGridViewTextBoxCell();
                                c7.Value = r[2].ToString();

                                row.Cells.AddRange(c1, c2, c3, c4, c5, c6, c7);
                                dataGridView1.Rows.Add(row);
                            }
                        }
                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.Columns[5].ReadOnly = true;
                    }
                    break;
                case 5:
                    this.Text += " товара";
                    FullNameTovar = "";
                    dgv1.HeaderText = "Категория";
                    dgv2.HeaderText = "Производитель";
                    dgv3.HeaderText = "Название";
                    dgv4.HeaderText = "Цена Продажи";
                    dgv5.HeaderText = "Количество на складе";
                    dgv6.HeaderText = "Код";
                    dataGridView1.Columns.AddRange(dgv6 ,dgv1, dgv2, dgv3, dgv4, dgv5);
                    dataGridView1.Columns[0].Visible = false;
                    
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT * FROM Категория", connection);
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                //MessageBox.Show(r[1].ToString());
                                category.Add(r[1].ToString());
                            }
                        }
                        command.CommandText = "SELECT * FROM ПроизводителиТовара";
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while(r.Read())
                            {
                                manufact.Add(r[1].ToString());
                            }
                        }

                        command.CommandText = "SELECT * FROM Товар";
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                DataGridViewTextBoxCell c6 = new DataGridViewTextBoxCell();
                                c6.Value = r[0].ToString();
                                DataGridViewComboBoxCell c1 = new DataGridViewComboBoxCell();
                                c1.Items.Add(ReturnCategory(int.Parse(r[2].ToString())));
                                c1.Value = c1.Items[0];
                                for(int i = 0; i < category.Count; i++)
                                {
                                    if (category[i] == c1.Value.ToString())
                                    {
                                        continue;
                                    }
                                    c1.Items.Add(category[i]);
                                }
                                DataGridViewComboBoxCell c2 = new DataGridViewComboBoxCell();
                                c2.Items.Add(ReturnManufact(int.Parse(r[1].ToString())));
                                c2.Value = c2.Items[0];
                                for(int i = 0; i < manufact.Count; i++)
                                {
                                    if (manufact[i] == c2.Value.ToString())
                                    {
                                        continue;
                                    }
                                    c2.Items.Add(manufact[i]);
                                }
                                DataGridViewTextBoxCell c3 = new DataGridViewTextBoxCell();
                                c3.Value = r[3].ToString();
                                DataGridViewTextBoxCell c4 = new DataGridViewTextBoxCell();
                                c4.Value = r[4].ToString();
                                DataGridViewTextBoxCell c5 = new DataGridViewTextBoxCell();
                                c5.Value = r[5].ToString();
                                row.Cells.AddRange(c6, c1, c2, c3, c4, c5);
                                dataGridView1.Rows.Add(row);
                            }
                        }
                    }
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.connectionString))
            {
                string cmd = "";
                connect.Open();
                SqlCommand command = new SqlCommand(cmd, connect);
                switch(typeredact)
                {
                    case 1:
                        cmd = $"UPDATE Категория SET Категория = '{dataGridView1.SelectedCells[1].Value.ToString()}'" +
                            $" WHERE Код = {dataGridView1.SelectedCells[0].Value.ToString()}";
                        command.CommandText = cmd;
                        command.ExecuteNonQuery();
                        MessageBox.Show("Отредактировано");
                        break;
                    case 2:
                        cmd = $"UPDATE ПроизводителиТовара SET Производитель = '{dataGridView1.SelectedCells[1].Value.ToString()}' " +
                            $"WHERE Код = {dataGridView1.SelectedCells[0].Value.ToString()}";
                        command.CommandText = cmd;
                        command.ExecuteNonQuery();
                        MessageBox.Show("Отредактировано");
                        break;
                    case 3:
                        cmd = $"UPDATE Поставки SET " +
                            $"Товар = {IDCode}, " +
                            $"Цена = {dataGridView1.SelectedCells[3].Value.ToString()}, " +
                            $"Количество = {dataGridView1.SelectedCells[4].Value.ToString()}, " +
                            $"Стоимость = {dataGridView1.SelectedCells[5].Value.ToString()} " +
                            $"WHERE Код = {dataGridView1.SelectedCells[0].Value.ToString()}";
                        //MessageBox.Show(cmd);
                        command.CommandText = cmd;
                        command.ExecuteNonQuery();
                        MessageBox.Show("Отредактировано");
                        break;
                    case 4:
                        cmd = $"UPDATE Продажи SET Товар = {dataGridView1.SelectedCells[6].Value.ToString()}, " +
                            $"ЦенаПродажи = {dataGridView1.SelectedCells[3].Value.ToString()}, " +
                            $"Количество = {dataGridView1.SelectedCells[4].Value.ToString()} ," +
                            $"Стоимость = {dataGridView1.SelectedCells[5].Value.ToString()} " +
                            $"WHERE Код = {dataGridView1.SelectedCells[0].Value.ToString()}";
                        //MessageBox.Show(cmd);
                        command.CommandText = cmd;
                        command.ExecuteNonQuery();
                        MessageBox.Show("Отредактировано");
                        break;
                    case 5:
                        cmd = $"UPDATE Товар SET Производитель = {ReturnManufactID(dataGridView1.SelectedCells[2].Value.ToString())}, " +
                            $"Категория = {ReturnCategoryID(dataGridView1.SelectedCells[1].Value.ToString())}, " +
                            $"Название = '{dataGridView1.SelectedCells[3].Value.ToString()}', " +
                            $"ЦенаПродажи = {dataGridView1.SelectedCells[4].Value.ToString()}, " +
                            $"КоличествоНаСкладе = {dataGridView1.SelectedCells[5].Value.ToString()} " +
                            $"WHERE Код = '{IDCode}'";

                        command = new SqlCommand(cmd, connect);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Отредактировано");
                        break;
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (typeredact != 4)
                {
                    IDCode = dataGridView1.SelectedCells[0].Value.ToString();
                }
                if (typeredact == 3)
                {
                    dataGridView1.SelectedCells[5].Value = int.Parse(dataGridView1.SelectedCells[3].Value.ToString()) * int.Parse(dataGridView1.SelectedCells[4].Value.ToString());
                }
                if (typeredact == 4)
                {
                    dataGridView1.SelectedCells[5].Value = int.Parse(dataGridView1.SelectedCells[3].Value.ToString()) * int.Parse(dataGridView1.SelectedCells[4].Value.ToString());
                    //using (SqlConnection connection = new SqlConnection())
                    //{
                    //    connection.Open();

                    //}
                }
            }
            catch (System.ArgumentOutOfRangeException )
            {

            }

        }
        
    }
}
