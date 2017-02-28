using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PalladiumDwh.ClientApp.Presenters;

namespace PalladiumDwh.ClientApp.Views
{
    public partial class FeedBack : Form, IFeedBackView
    {
        public FeedBack()
        {
            InitializeComponent();
            Presenter = new FeedBackPresenter(this);
            Presenter.Initialize();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            Presenter.Send();
        }

        public string Title
        {
            get { return Text; }
            set { Text = value; }
        }

        public string Header { get; set; }

        public string HeaderDescription { get; set; }

        public IFeedBackPresenter Presenter { get; set; }


        public int Phone
        {
            get { return Convert.ToInt32(textBoxPhone.Text); }
            set { textBoxPhone.Text = value.ToString(); }
        }

        public string Email
        {
            get { return textBoxEmail.Text; }
            set { textBoxEmail.Text = value; }
        }

        public string Comment
        {
            get { return textBoxComment.Text; }
            set { textBoxComment.Text = value; }
        }

        public bool SendLogs
        {
            get { return checkBoxSendLogs.Checked; }
            set { checkBoxSendLogs.Checked = value; }
        }

        public Task StartUp()
        {
            throw new NotImplementedException();
        }
    }
}
