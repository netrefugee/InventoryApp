using DevExpress.Xpf.Core;
using Models;
using ModuleSetting.ViewModels;
using System.Windows;

namespace ModuleSetting.Views
{
    /// <summary>
    /// Interaction logic for AddFarmerWindow.xaml
    /// </summary>
    public partial class AddFarmerWindow : Window
    {
        public AddFarmerWindow()
        {
            InitializeComponent();
        }
        public AddFarmerWindow(Farmer farmer)
        {
            InitializeComponent();
            this.DataContext = new AddFarmerWindowViewModel() { Farmer = farmer,Window=this };
        }

        private void MoveWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
            e.Handled = true;
        }
    }
}
