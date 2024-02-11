using System.Windows.Input;
namespace Lexington.Inteface
{
    public interface IMouseCommand
    {
        public delegate void IMouseEventHandler(object sender, MouseEventArgs e);
    }
}
