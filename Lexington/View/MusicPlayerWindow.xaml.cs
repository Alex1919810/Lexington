using System.Windows;


namespace Lexington.View
{
    /// <summary>
    /// MusicPlayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MusicPlayerWindow : Window
    {
        public MusicPlayerWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.MusicPlayerViewModel();
        }
    }
}
