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

        public ICommand GetMusicList { get; private set; }

        public ICommand GetCalender { get; private set; }


        public ICommand CloseWindow { get; private set; }


        public WindowService() 
        {
            GetReport = new RelayCommand<string>(param => WindowManager.Instance.OpenAndCloseWindow<WeatherWindow>(param));
            GetMatters = new RelayCommand<string>(param => WindowManager.Instance.OpenAndCloseWindow<ClockWindow>(param));
            GetMemo = new RelayCommand<string>(param => WindowManager.Instance.OpenAndCloseWindow<MemoWindow>(param));
            GetMusicPlayer = new RelayCommand<string>(param => WindowManager.Instance.OpenAndCloseWindow<MusicPlayerWindow>(param));
            GetMusicList = new RelayCommand<string>(param => WindowManager.Instance.OpenAndCloseWindow<MusicListWindow>(param));
            GetCalender = new RelayCommand<string>(param => WindowManager.Instance.OpenAndCloseWindow<CalendarWindow>(param));

            CloseWindow = new RelayCommand<string>(param=>WindowManager.Instance.SetWindowClose(param));
        }
    }
}
