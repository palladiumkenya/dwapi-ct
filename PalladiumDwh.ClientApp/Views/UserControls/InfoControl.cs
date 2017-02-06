using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PalladiumDwh.ClientApp.Views.UserControls
{
    public partial class InfoControl : UserControl
    {
        public InfoControl()
        {
            InitializeComponent();
        }

        public string Header
        {
            get { return labelH.Text; }
            set { labelH.Text = value; }
        }

        public string HeaderDescription
        {
            get { return labelHD.Text; }
            set { labelHD.Text = value; }
        }
    }
}
