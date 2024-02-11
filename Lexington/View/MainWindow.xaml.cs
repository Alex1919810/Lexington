using Lexington.Model;
using Lexington.Tools;
using Lexington.View;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Lexington
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Point _pressedPosition;
        bool _isDragMoved = false;
        //bool _isResize = false;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            DataContext = new ViewModel.MainViewWindow(this);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            // 处理打开事件
            Show();
            Activate();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // 处理退出事件
            Application.Current.Shutdown();
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            // 隐藏当前窗口
            Hide();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _pressedPosition = e.GetPosition(this);

        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            // 在拖动时移动窗口
            if (Mouse.LeftButton == MouseButtonState.Pressed && _pressedPosition != e.GetPosition(this))
            {
                _isDragMoved = true;
                DragMove();
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // 停止拖动
            if (_isDragMoved)
            {
                _isDragMoved = false;
                e.Handled = true;
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ViewModel.MainViewWindow TmpMainViewWindow = DataContext as ViewModel.MainViewWindow;
            TmpMainViewWindow.DisplayText(1);
        }



    }



}