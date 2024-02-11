using Lexington.Inteface;
using System.Windows.Input;
using static Lexington.Inteface.IMouseCommand;

namespace Lexington.Command
{
    internal class MouseCommand : IMouseCommand
    {
        public event EventHandler CanExecuteChanged;
        public event IMouseEventHandler LeftMousePress;
        public event IMouseEventHandler LeftMouseRelease;
        public event IMouseEventHandler RightMousePress;
        public event IMouseEventHandler RightMouseRelease;


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var args = (MouseEventArgs)parameter;
            if (args.LeftButton == MouseButtonState.Pressed)
            {
                LeftMousePress?.Invoke(this, args);             
            }
            else if(args.LeftButton == MouseButtonState.Released)
            {
                LeftMouseRelease?.Invoke(this, args);
            }
            else if(args.RightButton == MouseButtonState.Pressed)
            {
                RightMousePress?.Invoke(this, args);
            }
            else if(args.RightButton == MouseButtonState.Released)
            { 
                RightMouseRelease?.Invoke(this, args);
            }
        }
    }
}
