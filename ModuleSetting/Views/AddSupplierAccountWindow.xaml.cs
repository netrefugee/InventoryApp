using Models;
using ModuleSetting.ViewModels;
using System.Windows;

namespace ModuleSetting.Views
{
    /// <summary>
    /// Interaction logic for AddSupplierAccountWindow.xaml
    /// </summary>
    public partial class AddSupplierAccountWindow : Window
    {
        public AddSupplierAccountWindow()
        {
            InitializeComponent();
        }
                public AddSupplierAccountWindow(SupplierAccount supplierAccount)
        {
            InitializeComponent();
            this.DataContext = new AddSupplierAccountWindowViewModel() { SupplierAccount = supplierAccount, Window = this };
        }


        private void MoveWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
            e.Handled = true;
        }
    }
}
