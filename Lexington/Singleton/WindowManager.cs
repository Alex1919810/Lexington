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
                return null;
            }
            return (T)M_Windows[windowType];
        }

        public Window GetWindow(Type windowType)
        {
            Window window = null;
            M_Windows.TryGetValue(windowType,out window);
            return window;
        }

        public void OpenAndCloseWindow<T>(string param) where T : Window, new()
        {
            Type FatherWindowType = null; double X = 0; double Y = 0;

            var window = GetWindow<T>();
            if(window == null)
            {
                if (param != null)
                {
                    ParseParam(param, ref FatherWindowType, ref X, ref Y);
                }
                Type windowType = typeof(T);
                M_Windows[windowType] = new T();
                M_Windows[windowType].Closed += (sender, args) => M_Windows.Remove(windowType);
                window = (T)M_Windows[windowType];
                if (FatherWindowType != null && M_Windows.ContainsKey(FatherWindowType))
                {
                    SetNewWindowPosition(M_Windows[FatherWindowType], window, X, Y);
                }
                window.Show();
                window.Activate();
            }
            else
            {
                window.Close();
            }
        }

        public void OpenAndCloseWindow<T>(Type FatherWindowType=null,double X=0.0, double Y=0.0) where T : Window, new()
        {
            var window = GetWindow<T>();
            if (window == null)
            {
                Type windowType = typeof(T);
                M_Windows[windowType] = new T();
                M_Windows[windowType].Closed += (sender, args) => M_Windows.Remove(windowType);
                window = (T)M_Windows[windowType];
                if (FatherWindowType != null && M_Windows.ContainsKey(FatherWindowType))
                {
                    SetNewWindowPosition(M_Windows[FatherWindowType], window, X, Y);
                }
                window.Show();
                window.Activate();
            }
            else
            {
                window.Close();
            }
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

        private void ParseParam(string param,ref Type FatherWindowType ,ref double X ,ref double Y )
        {
            string[] s = param.Split(',');
            for (int i = 0; i < s.Count(); i++)
            {
                string t = s[i];
                if (i == 0)
                {
                    t = "Lexington.View." + t;
                    FatherWindowType = Type.GetType(t);
                }
                else if (i == 1) X = Convert.ToDouble(t);
                else if (i == 2) Y = Convert.ToDouble(t);
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

        public void SetWindowClose(Type type)
        {
            if (!M_Windows.ContainsKey(type)) return;
            else
            {
                M_Windows[type].Close();
            }
        }

        public void SetWindowClose(string param)
        {
            param = "Lexington.View." + param;
            Type type = Type.GetType(param);
            if (!M_Windows.ContainsKey(type)) return;
            else
            {
                M_Windows[type].Close();
            }
        }

        private void SetNewWindowPosition(Window FatherWindow, Window ChildWindow, double PosX = 0, double PoxY = 0)
        {
            double FatherWindowTop = FatherWindow.Top;
            double FatherWindowLeft = FatherWindow.Left;
            ChildWindow.Left = FatherWindowLeft + PosX;
            ChildWindow.Top = FatherWindowTop + PoxY;
        }



    }
}
