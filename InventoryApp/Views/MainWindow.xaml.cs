using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;

namespace InventoryApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        Rect rcnormal;//定义一个全局rect记录还原状态下窗口的位置和大小。
        public MainWindow()
        {
            InitializeComponent();
        }
        // 最小化
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            if (e != null)
            {
                e.Handled = true;
            }
        }
        // 最大化
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            this.btnMaximize.Visibility = Visibility.Collapsed;
            this.btnNormal.Visibility = Visibility.Visible;
            rcnormal = new Rect(this.Left, this.Top, this.Width, this.Height);//保存下当前位置与大小
            this.Left = 0;//设置位置
            this.Top = 0;
            Rect rc = SystemParameters.WorkArea;//获取工作区大小
            this.Width = rc.Width;
            this.Height = rc.Height;
            if (e!=null)
            {
                e.Handled = true;
            }

        }
        // 普通大小
        private void btnNormal_Click(object sender, RoutedEventArgs e)
        {
            this.Left = rcnormal.Left;
            this.Top = rcnormal.Top;
            this.Width = rcnormal.Width;
            this.Height = rcnormal.Height;
            this.btnMaximize.Visibility = Visibility.Visible;
            this.btnNormal.Visibility = Visibility.Collapsed;
            if (e != null)
            {
                e.Handled = true;
            }
        }
        // 关闭按钮
        private void Button_MouseLeftButtonDown_Close(object sender, RoutedEventArgs e)
        {
             
            this.Close();
            if (e != null)
            {
                e.Handled = true;
            }

        }
        
        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
            e.Handled = true;
        }
        // 双击最大化
        private void Label_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
                if (this.ActualWidth == SystemParameters.WorkArea.Width)
                {
                    btnNormal_Click(null, null);
                }
                else
                {
                    btnMaximize_Click(null, null);
                }
            if (e != null)
            {
                e.Handled = true;
            }
        }
        // 改变窗体大小
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualHeight > SystemParameters.WorkArea.Height || this.ActualWidth > SystemParameters.WorkArea.Width)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                btnMaximize_Click(null, null);
            }
            if (e != null)
            {
                e.Handled = true;
            }
        }
    }
}
