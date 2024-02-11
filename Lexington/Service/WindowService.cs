using Lexington.Command;
using Lexington.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace Lexington.Service
{
    public  class WindowService
    {
        private MainWindow M_MainWindow;

        private ClockWindow M_ClockWindow;

        private MemoWindow M_MemoWindow;

        private MusicPlayerWindow M_MusicPlayerWindow; 

        public ICommand GetReport { get; private set; }
        public ICommand GetMatters { get; private set; }
        public ICommand GetMemo { get; private set; }

        public ICommand GetMusicPlayer { get; private set; }
        public WindowService(MainWindow mainWindow) 
        {
            M_MainWindow = mainWindow;
            GetMatters = new RelayCommand<object>(param => OpenWindow<ClockWindow>(ref M_ClockWindow));
            GetMemo = new RelayCommand<object>(param => OpenWindow<MemoWindow>(ref M_MemoWindow));
            GetMusicPlayer = new RelayCommand<object>(param => OpenWindow<MusicPlayerWindow>(ref M_MusicPlayerWindow, M_MainWindow));
        }

        public void OpenWindow<T>(ref T ChildWindow, Window FatherWindow = null, double X = 0, double Y = 0) where T : Window
        {
            if (ChildWindow == null || !ChildWindow.IsVisible)
            {
                ChildWindow = Activator.CreateInstance<T>();
                if (FatherWindow != null)
                {
                    SetNewWindowPosition(FatherWindow, ChildWindow, X, Y);
                }

            }
            else
            {
                ChildWindow.Activate();
                return;
            }

            ChildWindow.Show();
            ChildWindow.Activate();
        }

   


        public static void SetNewWindowPosition(Window FatherWindow, Window ChildWindow, double PosX = 0, double PoxY = 0)
        {
            double FatherWindowTop = FatherWindow.Top;
            double FatherWindowLeft = FatherWindow.Left;
            ChildWindow.Left = FatherWindowLeft + PosX;
            ChildWindow.Top = FatherWindowTop + PoxY;
        }



    }
}
