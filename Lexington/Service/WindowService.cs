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

        public ICommand CloseWindow { get; private set; }


        public WindowService() 
        {
            GetReport = new RelayCommand<object>(param => WindowManager.Instance.OpenAndCloseWindow<WeatherWindow>(typeof(MainWindow), 100));
            GetMatters = new RelayCommand<object>(param => WindowManager.Instance.OpenAndCloseWindow<ClockWindow>());
            GetMemo = new RelayCommand<object>(param => WindowManager.Instance.OpenAndCloseWindow<MemoWindow>());
            GetMusicPlayer = new RelayCommand<object>(param => WindowManager.Instance.OpenAndCloseWindow<MusicPlayerWindow>(typeof(MainWindow)));
            CloseWindow = new RelayCommand<string>(param=>WindowManager.Instance.SetWindowClose(param));
        }
    }
}
