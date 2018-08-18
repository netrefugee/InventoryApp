using Models;
using ModuleSetting.ViewModels;
using System.Windows;

namespace ModuleSetting.Views
{
    /// <summary>
    /// Interaction logic for AddGoodWindow.xaml
    /// </summary>
    public partial class AddGoodWindow : Window
    {
        public AddGoodWindow()
        {
            InitializeComponent();
        }

        public AddGoodWindow(Good good)
        {
            InitializeComponent();
            this.DataContext = new AddGoodWindowViewModel() { Good = good, Window = this };
        }

        private void MoveWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
            e.Handled = true;
        }
    }
}
