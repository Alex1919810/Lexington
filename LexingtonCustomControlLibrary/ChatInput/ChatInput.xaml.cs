using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LexingtonCustomControlLibrary.ChatInput
{
    public partial class ChatInput : UserControl
    {
        public ChatInput()
        {
            InitializeComponent();
        }

        // 在按下回车键时发送消息
        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string message = MessageTextBox.Text;
                if (!string.IsNullOrEmpty(message))
                {
                    // 这里你可以根据需要处理发送的消息
                    MessageBox.Show($"Message sent: {message}");
                    MessageTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Please enter a message.");
                }

                // 防止回车导致换行
                e.Handled = true;
            }
        }
    }
}
