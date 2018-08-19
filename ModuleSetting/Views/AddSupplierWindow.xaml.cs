using Models;
using ModuleSetting.ViewModels;
using System.Windows;

namespace ModuleSetting.Views
{
    /// <summary>
    /// Interaction logic for AddSupplierWindow.xaml
    /// </summary>
    public partial class AddSupplierWindow : Window
    {
        public AddSupplierWindow()
        {
            InitializeComponent();
        }
        public AddSupplierWindow(Supplier supplier)
        {
            InitializeComponent();
            this.DataContext = new AddSupplierWindowViewModel() { Supplier = supplier, Window = this };
        }


        private void MoveWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
            e.Handled = true;
        }
    }
}
