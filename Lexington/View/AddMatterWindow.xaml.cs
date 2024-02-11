using Lexington.Model;
using Lexington.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// AddMatterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddMatterWindow : Window
    {

        private ObservableCollection<string> MatterNames = new ObservableCollection<string>();
        private ObservableCollection<string> Times = new ObservableCollection<string>();
        private Matter ResultMatter = new Matter();

        public event EventHandler<Matter> ReturnValueUpdated;
        public AddMatterWindow()
        {
            InitializeComponent();
            MatterNames = FilesTool.ParseExpeditionOneAttribute("编号");
            Times = FilesTool.ParseExpeditionOneAttribute("时间");
            MatterList.ItemsSource = MatterNames;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 处理选择事件
            if (MatterList.SelectedItem != null)
            {

                string[] timef = Times[MatterList.SelectedIndex].Split('/');
                DateTime currentTime = DateTime.Now;

                DateTime newTime = currentTime.AddHours(int.Parse(timef[0])).AddMinutes(int.Parse(timef[1]));
                StartTimePicker.Value = currentTime;
                EndTimePicker.Value = newTime;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MatterList.Text = (string)MatterList.SelectedItem;
                });
            }
        }

        private void ComboBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
            }
        }


        private void SureClick(object sender, RoutedEventArgs e)
        {
            ResultMatter.MatterName = MatterList.Text;
            if(StartTimePicker.Value.HasValue && EndTimePicker.Value.HasValue)
            {
                if((EndTimePicker.Value <= DateTime.Now))
                {
                    MessageBox.Show("任务已过期，请重新设置结束时间！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                ResultMatter.StartTime = StartTimePicker.Value;
                ResultMatter.EndTime = EndTimePicker.Value.GetValueOrDefault().AddSeconds(-EndTimePicker.Value.GetValueOrDefault().Second);
            }
            else
            {
                MessageBox.Show("时间未设置！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!MatterNames.Contains(ResultMatter.MatterName)) ResultMatter.IsExpedition = false;
            ResultMatter.IsRedo = RedoBox.IsChecked.GetValueOrDefault();
            ResultMatter.Process=DataTool.CalculateProcess(ResultMatter.StartTime,ResultMatter.EndTime);
            ReturnValueUpdated?.Invoke(this, ResultMatter);
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
