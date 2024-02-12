using Lexington.Command;
using Lexington.Singleton;
using Lexington.View;
using System.Windows.Input;

namespace Lexington.Service
{
    public class WindowService
    {
        public ICommand GetReport { get; private set; }
        public ICommand GetMatters { get; private set; }
        public ICommand GetMemo { get; private set; }

        public ICommand GetMusicPlayer { get; private set; }

        public WindowService() 
        {
            GetReport = new RelayCommand<object>(param => WindowManager.Instance.OpenWindow<WeatherWindow>(typeof(MainWindow), 100));
            GetMatters = new RelayCommand<object>(param => WindowManager.Instance.OpenWindow<ClockWindow>(typeof(ClockWindow)));
            GetMemo = new RelayCommand<object>(param => WindowManager.Instance.OpenWindow<MemoWindow>(typeof(MemoWindow)));
            GetMusicPlayer = new RelayCommand<object>(param => WindowManager.Instance.OpenWindow<MusicPlayerWindow>(typeof(MainWindow)));
        }
    }
}
