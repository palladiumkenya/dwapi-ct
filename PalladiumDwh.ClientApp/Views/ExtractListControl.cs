using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using PalladiumDwh.ClientApp.Presenters;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Views
{
    
    public partial class ExtractListControl : UserControl,IExtractListView
    {
        private List<ExtractSetting> _extractSettings=new List<ExtractSetting>();

        public ExtractListControl()
        {
            InitializeComponent();
            Presenter = new ExtractListPresenter(this);
            Presenter.Initialize();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IExtractListPresenter Presenter { get; set; }

        public string Header
        {
            get { return labelHeader.Text; }
            set { labelHeader.Text = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<ExtractSetting> ExtractSettings
        {
            get { return _extractSettings; }
            set
            {
                _extractSettings = value; 
                LoadExtractExtractSetting(_extractSettings);
            }
        }

        private void LoadExtractExtractSetting(List<ExtractSetting> extractSettings)
        {
            ClearExtractSettings();
            foreach (var e in extractSettings)
            {
                var item = new ListViewItem {Text = e.Display};
                item.SubItems.Add(e.Destination);
                listViewExtractList.Items.Add(item);
            }
        }

        public void ClearExtractSettings()
        {
            listViewExtractList.Items.Clear();
        }
    }
}
