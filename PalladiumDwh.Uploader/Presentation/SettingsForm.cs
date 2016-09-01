using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PalladiumDwh.Uploader.Properties;
namespace PalladiumDwh.Uploader.Presentation
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
       
            Settings.Default.UploadIpServer = serverIPTextBox.Text;
            Settings.Default.Save();
        }

     

     
    }
}
