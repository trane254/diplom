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
    public partial class MainMenu : Form
    {
        private int TypeAccess;
        public int typeAccess
        {
            get
            {
                return TypeAccess;
            }
            set
            {
                TypeAccess = value;

            }
        }

        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.Text = "Си-бемоль - " + Properties.Settings.Default.version;
            //this.menuStrip1.Items.
            if (TypeAccess == 1)
            {
                label1.Text = $"Тип доступа: Администратор";
            }
            else
            {
                label1.Text = "Тип доступа: Продавец";
            }
            
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TypeAccess == 1)
            {
                Settings s = new Settings();
                s.ShowDialog();
            }
            else
            {
                MessageBox.Show("Вы не являетесь администратором", "Ошибка");
            }

        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spravochnik a = new Spravochnik();
            a.ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFromSpravochnik a = new DeleteFromSpravochnik();
            a.ShowDialog();
        }

        private void редактированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(TypeAccess == 0)
            {
                MessageBox.Show("Вы не являетесь администратором", "Ошибка");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SellForm a = new SellForm();
            a.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Postavka a = new Postavka();
            a.ShowDialog();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TypeAccess == 1)
            {
                //форма редактирования
            }
            else
            {
                MessageBox.Show("Вы не являетесь администратором", "Ошибка");
            }
        }

        private void категорииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prosmotr a = new Prosmotr();
            a.type_ = 1;
            a.ShowDialog();
        }

        private void производителиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prosmotr a = new Prosmotr();
            a.type_ = 2;
            a.ShowDialog();
        }

        private void поставкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prosmotr a = new Prosmotr();
            a.type_ = 3;
            a.ShowDialog();
        }

        private void продажиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prosmotr a = new Prosmotr();
            a.type_ = 4;
            a.ShowDialog();
        }


        private void товарToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prosmotr a = new Prosmotr();
            a.type_ = 5;
            a.ShowDialog();
        }
    }
}
