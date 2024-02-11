using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lexington.View
{
    /// <summary>
    /// Weather.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherWindow : Window
    {

        Point _pressedPosition;
        bool _isDragMoved = false;
        public WeatherWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.WeatherViewWindow(this);
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

    }


}
