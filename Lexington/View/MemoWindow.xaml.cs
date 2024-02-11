using System.Windows;
using System.Windows.Input;

namespace Lexington.View
{
    /// <summary>
    /// MemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MemoWindow : Window
    {
        public MemoWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.MemoViewWindow(this);

            KeyBinding keyBinding = new KeyBinding
            {
                Key = Key.S,
                Modifiers = ModifierKeys.Control,
                Command = ((ViewModel.MemoViewWindow)DataContext).SaveText
            };

            this.InputBindings.Add(keyBinding);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((ViewModel.MemoViewWindow)DataContext)?.SaveText.Execute(null);
        }
    }
}
