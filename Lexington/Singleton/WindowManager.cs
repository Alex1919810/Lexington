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

namespace Lexington.Singleton
{
    public class WindowManager
    {
        private Dictionary<Type, Window> M_Windows;

        private static WindowManager M_Instance;


        private WindowManager()
        {
            M_Windows = new Dictionary<Type, Window>();
        }

        public static WindowManager Instance
        {
            get
            {
                if (M_Instance == null)
                {
                    M_Instance = new WindowManager();
                }
                return M_Instance;
            }
        }

        public T GetWindow<T>() where T : Window, new()
        {
            Type windowType = typeof(T);
            if (!M_Windows.ContainsKey(windowType))
            {
                M_Windows[windowType] = new T();
                M_Windows[windowType].Closed += (sender, args) => M_Windows.Remove(windowType);
            }
            return (T)M_Windows[windowType];
        }

        public Window GetWindow(Type windowType)
        {
            Window window = null;
            M_Windows.TryGetValue(windowType,out window);
            return window;
        }

        public void OpenWindow<T>(Type FatherWindowType = null,double X = 0,double Y=0) where T : Window, new()
        {
            var window = GetWindow<T>();
            if(FatherWindowType != null && M_Windows.ContainsKey(FatherWindowType))
            {
                SetNewWindowPosition(M_Windows[FatherWindowType],window,X,Y);
            }
            window.Show();
            window.Activate();
        }

        public void AddWindow<T>(T window) where T : Window, new()
        {
            Type windowType = typeof(T);
            if (!M_Windows.ContainsKey(windowType))
            {
                M_Windows[windowType] = window;
                M_Windows[windowType].Closed += (sender, args) => M_Windows.Remove(windowType);
            }
        }

        public bool IsWindowVisible(Type type)
        {
            if (!M_Windows.ContainsKey(type)) return false;
            else
            {
                return M_Windows[type].Visibility == Visibility.Visible;
            }
        }

        public void SetWindowHide(Type type)
        {
            if (!M_Windows.ContainsKey(type)) return;
            else
            {
                M_Windows[type].Hide();
            }
        }

        public void SetNewWindowPosition(Window FatherWindow, Window ChildWindow, double PosX = 0, double PoxY = 0)
        {
            double FatherWindowTop = FatherWindow.Top;
            double FatherWindowLeft = FatherWindow.Left;
            ChildWindow.Left = FatherWindowLeft + PosX;
            ChildWindow.Top = FatherWindowTop + PoxY;
        }



    }
}
