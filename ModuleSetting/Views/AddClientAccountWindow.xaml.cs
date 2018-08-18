using Models;
using ModuleSetting.ViewModels;
using System.Windows;

namespace ModuleSetting.Views
{
    /// <summary>
    /// Interaction logic for AddClientAccountWindow.xaml
    /// </summary>
    public partial class AddClientAccountWindow : Window
    {
        public AddClientAccountWindow()
        {
            InitializeComponent();
        }
        public AddClientAccountWindow(ClientAccount clientAccount)
        {
            InitializeComponent();
            this.DataContext = new AddClientAccountWindowViewModel() { ClientAccount = clientAccount, Window = this };
        }


        private void MoveWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
            e.Handled = true;
        }
    }
}
