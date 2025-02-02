using Lexington.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LexingtonCustomControlLibrary.Notify;

namespace Lexington.BaseClass
{
    public class BaseViewModel<T_Window>:NotifyPropertyChanged where T_Window : Window 
    {
        public ICommand WindowMove_LeftMouseDown { get;private set; }

        public ICommand WindowMove_LeftMouseUp { get; private set; }

        public ICommand WindowMove_MouseMove { get; private set; }

        protected Point M_Point;

        protected T_Window M_Window;

        protected bool IsDragging = false;

        protected BaseViewModel(T_Window B_Window)
        {
            M_Window = B_Window;

            WindowMove_LeftMouseDown = new RelayCommand<MouseButtonEventArgs>(LeftMouseDown);
            WindowMove_LeftMouseUp = new RelayCommand<MouseButtonEventArgs>(LeftMouseUp);
            WindowMove_MouseMove = new RelayCommand<MouseEventArgs>(MouseMove);
        }

        protected void LeftMouseDown(MouseButtonEventArgs e)
        {
            M_Point = e.GetPosition(M_Window);
        }

        protected void MouseMove(MouseEventArgs e)
        {
            // 在拖动时移动窗口
            if (Mouse.LeftButton == MouseButtonState.Pressed && M_Point != e.GetPosition(M_Window))
            {
                IsDragging = true;
                M_Window.DragMove();
            }
        }

        protected void LeftMouseUp(MouseButtonEventArgs e)
        {
            // 停止拖动
            if (IsDragging)
            {
                IsDragging = false;
                e.Handled = true;
            }
        }



    }
}
