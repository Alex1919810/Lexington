using System.Windows;
using System.Windows.Input;

namespace Lexington.View
{
    /// <summary>
    /// ClockWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ClockWindow : Window
    {
        public ClockWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.ClockViewModel();
        }

        private void DataGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }


    }
}
