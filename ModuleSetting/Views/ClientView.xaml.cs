using DevExpress.Xpf.Grid;
using System.Windows.Controls;

namespace ModuleSetting.Views
{
    /// <summary>
    /// Interaction logic for ClienterView
    /// </summary>
    public partial class ClientView : UserControl
    {
        public ClientView()
        {
            InitializeComponent();
        }

        private void GridControl_ItemsSourceChanged(object sender, ItemsSourceChangedEventArgs e)
        {
            if (e.Source is GridControl)
            {
                ((TableView)((GridControl)e.Source).View).BestFitColumns();
            }
        }
    }
}
