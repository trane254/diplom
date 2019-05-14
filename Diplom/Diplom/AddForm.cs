using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public partial class AddForm : Form
    {
        private int slDg;
        public int selectedDatagrid
        {
            get
            {
                return selectedDatagrid;
            }

            set
            {
                slDg = value;
            }
        }
        private string Textboxtext1;
        private string Textboxtext2;
        private string Textboxtext3;
        private string Textboxtext4;
        public string textboxtext1
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                value = textboxtext1;
            }
        }
        public string textboxtext2
        {
            get
            {
                return textBox2.Text;
            }
            set
            {
                textboxtext2 = value;
            }
        }
        public string textboxtext3
        {
            get
            {
                return textBox3.Text;
            }
            set
            {
                textboxtext3 = value;
            }
        }
        public string textboxtext4
        {
            get
            {
                return textBox4.Text;
            }
            set
            {
                textboxtext4 = value;
            }
        }

        public AddForm()
        {
            InitializeComponent();
            
        }
        
        

        private void AddForm_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            switch (slDg)
            {
                case 1:
                    this.Text = "Добавление производителя";
                    label1.Visible = true;
                    textBox1.Visible = true;
                    label1.Text = "Производитель: ";
                    break;
                case 2:
                    this.Text = "Добавление продукта";
                    label1.Visible = true;
                    textBox1.Visible = true;
                    label1.Text = "Производитель: ";
                    label2.Visible = true;
                    textBox2.Visible = true;
                    label2.Text = "Название: ";
                    label3.Visible = true;
                    textBox3.Visible = true;
                    label3.Text = "Цена :";
                    break;
                case 3:
                    this.Text = "Добавление клиента";
                    label1.Visible = true;
                    textBox1.Visible = true;
                    label1.Text = "ФИО Клиента: ";
                    label2.Visible = true;
                    textBox2.Visible = true;
                    label2.Text = "Адрес: ";
                    label3.Visible = true;
                    textBox3.Visible = true;
                    label3.Text = "Телефон: ";
                    break;
                case 4:
                    this.Text = "Добавление продажи";
                    label1.Visible = true;
                    textBox1.Visible = true;
                    label1.Text = "Клиент: ";
                    label2.Visible = true;
                    textBox2.Visible = true;
                    label2.Text = "Покупка: ";
                    break;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (slDg)
            {
                case 1:
                    if(textBox1.Text == "")
                    {
                        MessageBox.Show("Не все поля заполнены");
                        break;
                    }
                    Textboxtext1 = textBox1.Text;
                    this.Close();
                    break;
                case 2:
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Не все поля заполнены");
                        break;
                    }
                    if (textBox2.Text == "")
                    {
                        MessageBox.Show("Не все поля заполнены");
                        break;
                    }
                    if (textBox3.Text == "")
                    {
                        MessageBox.Show("Не все поля заполнены");
                        break;
                    }
                    this.Close();
                    break;
                case 3:
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Не все поля заполнены");
                        break;
                    }
                    if (textBox2.Text == "")
                    {
                        MessageBox.Show("Не все поля заполнены");
                        break;
                    }
                    if (textBox3.Text == "")
                    {
                        MessageBox.Show("Не все поля заполнены");
                        break;
                    }
                    this.Close();
                    break;
                case 4:
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Не все поля заполнены");
                        break;
                    }
                    if (textBox2.Text == "")
                    {
                        MessageBox.Show("Не все поля заполнены");
                        break;
                    }
                    this.Close();
                    break;
            }
        }
    }
}
