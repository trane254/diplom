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
        
        public string textboxtext1
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textboxtext1 = value;
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
                    label1.Visible = true;
                    textBox1.Visible = true;
                    break;
                case 2:
                    label1.Visible = true;
                    textBox1.Visible = true;
                    label2.Visible = true;
                    textBox2.Visible = true;
                    break;
                case 3:
                    label1.Visible = true;
                    textBox1.Visible = true;
                    label2.Visible = true;
                    textBox2.Visible = true;
                    label3.Visible = true;
                    textBox3.Visible = true;
                    break;
                case 4:
                    label1.Visible = true;
                    textBox1.Visible = true;
                    label2.Visible = true;
                    textBox2.Visible = true;
                    label3.Visible = true;
                    textBox3.Visible = true;
                    label4.Visible = true;
                    textBox4.Visible = true;
                    break;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (slDg)
            {
                case 1:
                    label1.Text = "Производитель: ";
                    label1.Visible = true;
                    textBox1.Visible = true;
                    if(textBox1.Text == "")
                    {
                        MessageBox.Show("Введите название производителя");
                        break;
                    }
                    textboxtext1 = textBox1.Text;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
    }
}
