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
    public partial class Prosmotr : Form
    {
        public Prosmotr()
        {
            InitializeComponent();
        }

        private int Type;
        public int type_
        {
            get
            {
                return Type;
            }
            set
            {
                Type = value;

            }
        }


        private void Prosmotr_Load(object sender, EventArgs e)
        {
            switch(Type)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }
    }
}
