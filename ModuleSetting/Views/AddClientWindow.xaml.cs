using Models;
using ModuleSetting.ViewModels;
using System.Windows;

namespace ModuleSetting.Views
{
    /// <summary>
    /// Interaction logic for AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
        }

        public AddClientWindow(Client client)
        {
            InitializeComponent();
            this.DataContext = new AddClientWindowViewModel() { Client = client, Window = this };
        }


        private void MoveWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
            e.Handled = true;
        }
    }
}
