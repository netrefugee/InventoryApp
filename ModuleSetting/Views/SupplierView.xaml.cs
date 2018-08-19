using DevExpress.Xpf.Grid;
using System.Windows.Controls;

namespace ModuleSetting.Views
{
    /// <summary>
    /// Interaction logic for SupplierView
    /// </summary>
    public partial class SupplierView : UserControl
    {
        public SupplierView()
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
