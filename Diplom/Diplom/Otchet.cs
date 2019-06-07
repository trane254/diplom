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

        private void Otchet_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            radioButton1.Checked = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GenerateOtchet(bool a)
        {
            if (a)
            {
                DateTime now = DateTime.Now;
                int babki = 0;
                int sdelki = 0;
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($"SELECT * FROM Продажи WHERE ДатаПоставки = '{now.ToString("d")}'", connection);
                    using (SqlDataReader r = command.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            babki += int.Parse(r[5].ToString());
                            sdelki++;
                        }
                    }
                }
                //%USERPROFILE%\Desktop - директория рабочего стола
                string path = $@"Отчет за сегодня ({now.ToString("d")}).docx";
                DocX doc = DocX.Create(path);
                doc.InsertParagraph($"Количество сделок: {sdelki}").
                    Font("Times New Roman");
                doc.InsertParagraph($"Заработано: {babki} рублей").Font("Times New Roman");
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
                                //MessageBox.Show(r[5].ToString());
                                babki += int.Parse(r[5].ToString());
                                sdelki++;
                            }

                        }
                    }
                    string path = $@"Отчет за месяц ({timeMin.ToString("d")} - {timeMax.ToString("d")}).docx";
                    DocX doc = DocX.Create(path);
                    doc.InsertParagraph($"Количество сделок: {sdelki}").
                        Font("Times New Roman");
                    doc.InsertParagraph($"Заработано: {babki} рублей").Font("Times New Roman");
                    doc.Save();
                    Process.Start($"{path}");

                    //MessageBox.Show(path);
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
        }
    }
}
