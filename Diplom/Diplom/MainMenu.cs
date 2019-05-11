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
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            s.ShowDialog();
        }

        private void базаДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database a = new Database();
            a.ShowDialog();
        }
    }
}
