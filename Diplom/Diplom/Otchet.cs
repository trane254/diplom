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
using System.Diagnostics;
using Xceed.Words.NET;

namespace Diplom
{
    public partial class Otchet : Form
    {
        public Otchet()
        {
            InitializeComponent();
        }

        private int TypeOtchet;
        public int typeotchet
        {
            get
            {
                return TypeOtchet;
            }
            set
            {
                TypeOtchet = value;
            }
        }

        private void Otchet_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.Text = "Отчет";
            radioButton1.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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





        private void GenerateOtchet(bool a)
        {
            if (TypeOtchet == 0)
            {
                if (a)
                {
                    DateTime now = DateTime.Now;
                    int babki = 0;
                    int sdelki = 0;
                    int sdelki_ = 0;
                    bool checker = false;
                    List<string> Name = new List<string>();
                    List<string> Count = new List<string>();
                    List<string> Cost = new List<string>();
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand($"SELECT * FROM Продажи WHERE ДатаПоставки = '{now.ToString("d")}'", connection);
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                //babki += int.Parse(r[5].ToString());
                                //sdelki++;
                                checker = false;
                                //MessageBox.Show(r[5].ToString());
                                babki += int.Parse(r[5].ToString());
                                sdelki++;
                                for (int i = 0; i < Name.Count; i++)
                                {
                                    if (ReturnFullNameTovar(r[2].ToString()) == Name[i])
                                    {
                                        checker = true;
                                        sdelki--;
                                        Count[i] = (int.Parse(Count[i]) + int.Parse(r[4].ToString())).ToString();
                                        Cost[i] = (int.Parse(Cost[i]) + int.Parse(r[5].ToString())).ToString();
                                        //continue;
                                    }
                                }
                                if (!checker)
                                {
                                    Name.Add(ReturnFullNameTovar(r[2].ToString()));
                                    Count.Add(r[4].ToString());
                                    Cost.Add(r[5].ToString());
                                }


                                sdelki_++;
                            }
                        }
                    }
                    //%USERPROFILE%\Desktop - директория рабочего стола
                    string path = $@"Отчет по продажам за сегодня ({now.ToString("d")}).docx";
                    DocX doc = DocX.Create(path);
                    doc.InsertParagraph($"Количество продаж: {sdelki_}").
                        Font("Times New Roman");
                    doc.InsertParagraph($"Заработано: {babki} рублей").Font("Times New Roman");
                    doc.InsertParagraph($"");
                    doc.InsertParagraph($"Таблица проданного:").Font("Times New Roman");
                    Table table = doc.AddTable(sdelki + 1, 3);
                    table.Alignment = Alignment.left;
                    table.Design = TableDesign.TableGrid;
                    table.Rows[0].Cells[0].Paragraphs[0].Append("Название товара").Alignment = Alignment.center;
                    table.Rows[0].Cells[1].Paragraphs[0].Append("Количество проданного").Alignment = Alignment.center;
                    table.Rows[0].Cells[2].Paragraphs[0].Append("Стоимость").Alignment = Alignment.center;
                    for (int i = 0; i < Name.Count; i++)
                    {
                        table.Rows[i + 1].Cells[0].Paragraphs[0].Append(Name[i]).Alignment = Alignment.left;
                        table.Rows[i + 1].Cells[1].Paragraphs[0].Append(Count[i]).Alignment = Alignment.center;
                        table.Rows[i + 1].Cells[2].Paragraphs[0].Append(Cost[i]).Alignment = Alignment.center;
                    }

                    doc.InsertParagraph().InsertTableAfterSelf(table);
                    doc.Save();
                    Process.Start($"{path}");

                }
                else
                {
                    DateTime timeMin = DateTime.Today;
                    int asdf = timeMin.Day - 1;
                    timeMin = timeMin.AddDays(-asdf);
                    DateTime timeMax = timeMin;
                    timeMax = timeMin.AddMonths(1);
                    //MessageBox.Show(timeMin.ToString("d") + "_____" + timeMax.ToString("d"));
                    int babki = 0;
                    int sdelki = 0;
                    int sdelki_ = 0;
                    List<string> Name = new List<string>();
                    List<string> Count = new List<string>();
                    List<string> Cost = new List<string>();
                    bool checker = false;
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand($"SELECT * FROM Продажи", connection);
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                DateTime bd = Convert.ToDateTime(r[1].ToString());
                                if (timeMin <= bd && bd < timeMax)
                                {
                                    checker = false;
                                    //MessageBox.Show(r[5].ToString());
                                    babki += int.Parse(r[5].ToString());
                                    sdelki++;
                                    for (int i = 0; i < Name.Count; i++)
                                    {
                                        if (ReturnFullNameTovar(r[2].ToString()) == Name[i])
                                        {
                                            checker = true;
                                            sdelki--;
                                            Count[i] = (int.Parse(Count[i]) + int.Parse(r[4].ToString())).ToString();
                                            Cost[i] = (int.Parse(Cost[i]) + int.Parse(r[5].ToString())).ToString();
                                            //continue;
                                        }
                                    }
                                    if (!checker)
                                    {
                                        Name.Add(ReturnFullNameTovar(r[2].ToString()));
                                        Count.Add(r[4].ToString());
                                        Cost.Add(r[5].ToString());
                                    }

                                }
                                sdelki_++;
                            }
                        }
                        string path = $@"Отчет по продажам за месяц ({timeMin.ToString("d")} - {timeMax.ToString("d")}).docx";
                        DocX doc = DocX.Create(path);
                        doc.InsertParagraph($"Количество продаж: {sdelki_ - 1}").
                            Font("Times New Roman");
                        doc.InsertParagraph($"Заработано: {babki} рублей").Font("Times New Roman");
                        doc.InsertParagraph($"");
                        doc.InsertParagraph($"Таблица проданного:").Font("Times New Roman");
                        Table table = doc.AddTable(sdelki + 1, 3);
                        table.Alignment = Alignment.left;
                        table.Design = TableDesign.TableGrid;
                        table.Rows[0].Cells[0].Paragraphs[0].Append("Название товара").Alignment = Alignment.center;
                        table.Rows[0].Cells[1].Paragraphs[0].Append("Количество проданного").Alignment = Alignment.center;
                        table.Rows[0].Cells[2].Paragraphs[0].Append("Стоимость").Alignment = Alignment.center;
                        for (int i = 0; i < Name.Count; i++)
                        {
                            table.Rows[i + 1].Cells[0].Paragraphs[0].Append(Name[i]).Alignment = Alignment.left;
                            table.Rows[i + 1].Cells[1].Paragraphs[0].Append(Count[i]).Alignment = Alignment.center;
                            table.Rows[i + 1].Cells[2].Paragraphs[0].Append(Cost[i]).Alignment = Alignment.center;
                        }

                        doc.InsertParagraph().InsertTableAfterSelf(table);
                        doc.Save();
                        Process.Start($"{path}");
                    }
                }
            }
            if (TypeOtchet == 1)
            {
                if (a)
                {
                    DateTime now = DateTime.Now;
                    int babki = 0;
                    int postavki = 0;
                    int postavki_ = 0;
                    bool checker = false;
                    List<string> Name = new List<string>();
                    List<string> Count = new List<string>();
                    List<string> Cost = new List<string>();
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand($"SELECT * FROM Поставки WHERE ДатаПоставки = '{now.ToString("d")}'", connection);
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                //babki += int.Parse(r[5].ToString());
                                //sdelki++;
                                checker = false;
                                //MessageBox.Show(r[5].ToString());
                                babki += int.Parse(r[5].ToString());
                                postavki++;
                                for (int i = 0; i < Name.Count; i++)
                                {
                                    if (ReturnFullNameTovar(r[2].ToString()) == Name[i])
                                    {
                                        checker = true;
                                        postavki--;
                                        Count[i] = (int.Parse(Count[i]) + int.Parse(r[4].ToString())).ToString();
                                        Cost[i] = (int.Parse(Cost[i]) + int.Parse(r[5].ToString())).ToString();
                                        //continue;
                                    }
                                }
                                if (!checker)
                                {
                                    Name.Add(ReturnFullNameTovar(r[2].ToString()));
                                    Count.Add(r[4].ToString());
                                    Cost.Add(r[5].ToString());
                                }
                                postavki_++;
                            }
                        }
                    }
                    string path = $@"Отчет по поставкам за сегодня ({now.ToString("d")}).docx";
                    DocX doc = DocX.Create(path);
                    doc.InsertParagraph($"Количество поставок: {postavki_}").
                        Font("Times New Roman");
                    doc.InsertParagraph($"Поставлено на: {babki} р.").Font("Times New Roman");
                    doc.InsertParagraph($"");
                    doc.InsertParagraph($"Таблица поставок:").Font("Times New Roman");
                    Table table = doc.AddTable(postavki_ + 1, 3);
                    table.Alignment = Alignment.left;
                    table.Design = TableDesign.TableGrid;
                    table.Rows[0].Cells[0].Paragraphs[0].Append("Название товара").Alignment = Alignment.center;
                    table.Rows[0].Cells[1].Paragraphs[0].Append("Количество").Alignment = Alignment.center;
                    table.Rows[0].Cells[2].Paragraphs[0].Append("Стоимость").Alignment = Alignment.center;
                    for (int i = 0; i < Name.Count; i++)
                    {
                        table.Rows[i + 1].Cells[0].Paragraphs[0].Append(Name[i]).Alignment = Alignment.left;
                        table.Rows[i + 1].Cells[1].Paragraphs[0].Append(Count[i]).Alignment = Alignment.center;
                        table.Rows[i + 1].Cells[2].Paragraphs[0].Append(Cost[i]).Alignment = Alignment.center;
                    }

                    doc.InsertParagraph().InsertTableAfterSelf(table);
                    doc.Save();
                    Process.Start($"{path}");
                }
                else
                {
                    //SELECT * FROM Поставки
                    DateTime timeMin = DateTime.Today;
                    int asdf = timeMin.Day - 1;
                    timeMin = timeMin.AddDays(-asdf);
                    DateTime timeMax = timeMin;
                    timeMax = timeMin.AddMonths(1);
                    //MessageBox.Show(timeMin.ToString("d") + "_____" + timeMax.ToString("d"));
                    int babki = 0;
                    int sdelki = 0;
                    int sdelki_ = 0;
                    List<string> Name = new List<string>();
                    List<string> Count = new List<string>();
                    List<string> Cost = new List<string>();
                    bool checker = false;
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand($"SELECT * FROM Поставки", connection);
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                DateTime bd = Convert.ToDateTime(r[1].ToString());
                                if (timeMin <= bd && bd < timeMax)
                                {
                                    checker = false;
                                    //MessageBox.Show(r[5].ToString());
                                    babki += int.Parse(r[5].ToString());
                                    sdelki++;
                                    for (int i = 0; i < Name.Count; i++)
                                    {
                                        if (ReturnFullNameTovar(r[2].ToString()) == Name[i])
                                        {
                                            checker = true;
                                            sdelki--;
                                            Count[i] = (int.Parse(Count[i]) + int.Parse(r[4].ToString())).ToString();
                                            Cost[i] = (int.Parse(Cost[i]) + int.Parse(r[5].ToString())).ToString();
                                            //continue;
                                        }
                                    }
                                    if (!checker)
                                    {
                                        Name.Add(ReturnFullNameTovar(r[2].ToString()));
                                        Count.Add(r[4].ToString());
                                        Cost.Add(r[5].ToString());
                                    }

                                }
                                sdelki_++;
                            }
                        }
                        string path = $@"Отчет по поставкам за месяц ({timeMin.ToString("d")} - {timeMax.ToString("d")}).docx";
                        DocX doc = DocX.Create(path);
                        doc.InsertParagraph($"Количество поставок: {sdelki_ - 1}").
                            Font("Times New Roman");
                        doc.InsertParagraph($"Поставлено на: {babki} р").Font("Times New Roman");
                        doc.InsertParagraph($"");
                        doc.InsertParagraph($"Таблица поставок:").Font("Times New Roman");
                        Table table = doc.AddTable(sdelki + 1, 3);
                        table.Alignment = Alignment.left;
                        table.Design = TableDesign.TableGrid;
                        table.Rows[0].Cells[0].Paragraphs[0].Append("Название товара").Alignment = Alignment.center;
                        table.Rows[0].Cells[1].Paragraphs[0].Append("Количество").Alignment = Alignment.center;
                        table.Rows[0].Cells[2].Paragraphs[0].Append("Стоимость").Alignment = Alignment.center;
                        for (int i = 0; i < Name.Count; i++)
                        {
                            table.Rows[i + 1].Cells[0].Paragraphs[0].Append(Name[i]).Alignment = Alignment.left;
                            table.Rows[i + 1].Cells[1].Paragraphs[0].Append(Count[i]).Alignment = Alignment.center;
                            table.Rows[i + 1].Cells[2].Paragraphs[0].Append(Cost[i]).Alignment = Alignment.center;
                        }

                        doc.InsertParagraph().InsertTableAfterSelf(table);
                        doc.Save();
                        Process.Start($"{path}");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked)
            {
                GenerateOtchet(true);
            }
            else
            {
                GenerateOtchet(false);
            }
            MessageBox.Show("Отчет сгенерирован");
        }
    }
}
