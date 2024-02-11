using System.Windows;
using System.Windows.Input;

namespace Lexington
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

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

    }



}