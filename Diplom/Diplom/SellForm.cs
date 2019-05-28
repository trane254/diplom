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
    public partial class SellForm : Form
    {
        public SellForm()
        {
            InitializeComponent();
        }

        private void SellForm_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.Text = "Продажа";

        }
    }
}
