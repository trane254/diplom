﻿using System;
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
        private string Nazvanie;

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



        private void RedactForm_Load(object sender, EventArgs e) ////////////////////////////////
        {
            this.ControlBox = false;
            this.Text = "Просмотр";
            string FullNameTovar = "";
            DataGridViewTextBoxColumn dgv1 = new DataGridViewTextBoxColumn();
            dgv1.Name = "Column1";
            DataGridViewTextBoxColumn dgv2 = new DataGridViewTextBoxColumn();
            dgv2.Name = "Column2";
            DataGridViewTextBoxColumn dgv3 = new DataGridViewTextBoxColumn();
            dgv2.Name = "Column3";
            DataGridViewTextBoxColumn dgv4 = new DataGridViewTextBoxColumn();
            dgv2.Name = "Column4";
            DataGridViewTextBoxColumn dgv5 = new DataGridViewTextBoxColumn();
            dgv2.Name = "Column5";

            switch (TypeRedact)
            {

                case 1://категория
                    this.Text = "Просмотр категорий";
                    dgv1.HeaderText = "Категория";
                    dataGridView1.Columns.Add(dgv1);
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT Категория FROM Категория", connection);
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                DataGridViewTextBoxCell c1 = new DataGridViewTextBoxCell();
                                c1.Value = r[0];
                                row.Cells.Add(c1);
                                dataGridView1.Rows.Add(row);
                            }
                        }
                    }
                    break;
                case 2:
                    this.Text = "Просмотр производителей";
                    dgv1.HeaderText = "Производители";
                    dataGridView1.Columns.Add(dgv1);
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT Производитель FROM ПроизводителиТовара", connection);
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                DataGridViewTextBoxCell c1 = new DataGridViewTextBoxCell();
                                c1.Value = r[0];
                                row.Cells.Add(c1);
                                dataGridView1.Rows.Add(row);
                            }
                        }
                    }
                    break;
                case 3:
                    this.Text = "Просмотр поставок";
                    dgv1.HeaderText = "Дата поставки";
                    dgv2.HeaderText = "Товар";
                    dgv3.HeaderText = "Цена закупки";
                    dgv4.HeaderText = "Количество";
                    dgv5.HeaderText = "Стоимость";
                    dataGridView1.Columns.AddRange(dgv1, dgv2, dgv3, dgv4, dgv5);
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
                                c1.Value = r[1].ToString().Remove(10);
                                DataGridViewTextBoxCell c2 = new DataGridViewTextBoxCell();
                                c2.Value = ReturnFullNameTovar(r[2].ToString());
                                DataGridViewTextBoxCell c3 = new DataGridViewTextBoxCell();
                                c3.Value = r[3].ToString();
                                DataGridViewTextBoxCell c4 = new DataGridViewTextBoxCell();
                                c4.Value = r[4].ToString();
                                DataGridViewTextBoxCell c5 = new DataGridViewTextBoxCell();
                                c5.Value = r[5].ToString();
                                row.Cells.AddRange(c1, c2, c3, c4, c5);
                                dgv1.Width = 100;
                                dgv2.Width = 350;
                                dataGridView1.Rows.Add(row);
                            }
                        }
                    }
                    break;
                case 4:
                    this.Text = "Просмотр продаж";
                    FullNameTovar = "";
                    dgv1.HeaderText = "Дата поставки";
                    dgv2.HeaderText = "Товар";
                    dgv3.HeaderText = "Цена продажи";
                    dgv4.HeaderText = "Количество";
                    dgv5.HeaderText = "Стоимость";
                    dataGridView1.Columns.AddRange(dgv1, dgv2, dgv3, dgv4, dgv5);
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
                                c1.Value = r[1].ToString().Remove(10);
                                DataGridViewTextBoxCell c2 = new DataGridViewTextBoxCell();
                                c2.Value = ReturnFullNameTovar(r[2].ToString());
                                DataGridViewTextBoxCell c3 = new DataGridViewTextBoxCell();
                                c3.Value = r[3].ToString();
                                DataGridViewTextBoxCell c4 = new DataGridViewTextBoxCell();
                                c4.Value = r[4].ToString();
                                DataGridViewTextBoxCell c5 = new DataGridViewTextBoxCell();
                                c5.Value = r[5].ToString();
                                row.Cells.AddRange(c1, c2, c3, c4, c5);
                                dgv1.Width = 100;
                                dgv2.Width = 350;
                                dataGridView1.Rows.Add(row);
                            }
                        }
                    }
                    break;
                case 5:
                    this.Text = "Просмотр товара";
                    FullNameTovar = "";
                    dgv1.HeaderText = "Категория";
                    dgv2.HeaderText = "Производитель";
                    dgv3.HeaderText = "Название";
                    dgv4.HeaderText = "Цена Продажи";
                    dgv5.HeaderText = "Количество на складе";
                    List<string> category = new List<string>();
                    List<string> manufact = new List<string>();
                    dataGridView1.Columns.AddRange(dgv1, dgv2, dgv3, dgv4, dgv5);
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
                                //category.Add(ReturnCategory(int.Parse(r[2].ToString())));
                                //c1.Value = ReturnCategory(int.Parse(r[2].ToString()));
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
                                //manufact.Add(ReturnManufact(int.Parse(r[1].ToString())));
                                //c2.Value = ReturnManufact(int.Parse(r[1].ToString()));
                                DataGridViewTextBoxCell c3 = new DataGridViewTextBoxCell();
                                c3.Value = r[3].ToString();
                                DataGridViewTextBoxCell c4 = new DataGridViewTextBoxCell();
                                c4.Value = r[4].ToString();
                                DataGridViewTextBoxCell c5 = new DataGridViewTextBoxCell();
                                c5.Value = r[5].ToString();
                                row.Cells.AddRange(c1, c2, c3, c4, c5);
                                dataGridView1.Rows.Add(row);
                            }
                        }
                    }
                    //MessageBox.Show(category[0]);
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
                connect.Open();
                switch(typeredact)
                {
                    case 5:
                        string cmd = $"UPDATE Товар SET Производитель = {ReturnManufactID(dataGridView1.SelectedCells[1].Value.ToString())}, " +
                            $"Категория = {ReturnCategoryID(dataGridView1.SelectedCells[0].Value.ToString())}, " +
                            $"Название = '{dataGridView1.SelectedCells[2].Value.ToString()}', " +
                            $"ЦенаПродажи = {dataGridView1.SelectedCells[3].Value.ToString()}, " +
                            $"КоличествоНаСкладе = {dataGridView1.SelectedCells[4].Value.ToString()} " +
                            $"WHERE Название = '{Nazvanie}'";
                            MessageBox.Show(cmd);
                            //SqlCommand command = new SqlCommand(cmd, connect);
                            //command.ExecuteNonQuery();

                        MessageBox.Show("Отредактировано");
                        break;
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
            try
            {
                Nazvanie = dataGridView1.SelectedCells[2].Value.ToString();
                MessageBox.Show(Nazvanie);
            }
            catch (System.ArgumentOutOfRangeException )
            {

            }
        }
        
    }
}
