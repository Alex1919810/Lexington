﻿using Lexington.Singleton;
using Lexington.ViewModel;
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
    /// MusicListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MusicListWindow : Window
    {

        public MusicListWindow()
        {
            this.Owner = WindowManager.Instance.GetWindow<MusicPlayerWindow>();
            InitializeComponent();
            DataContext = new ViewModel.MusicListViewModel(this);

        }

      
    }
}
