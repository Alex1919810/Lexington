using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LexingtonCustomControlLibrary.VolumeChange
{
    /// <summary>
    /// VolumeChange.xaml 的交互逻辑
    /// </summary>
    public partial class VolumeChange : UserControl
    {

        public static readonly DependencyProperty ValueProperty =
             DependencyProperty.Register(
                 "Value",
                 typeof(double),
                 typeof(VolumeChange),
                 new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
             );

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public VolumeChange()
        {
            InitializeComponent();
            slider.SetBinding(Slider.ValueProperty, new Binding(nameof(Value)) { Source = this, Mode = BindingMode.TwoWay });
        }

    }
}
