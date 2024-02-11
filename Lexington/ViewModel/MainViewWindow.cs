using Lexington.Model;
using Lexington.Tools;
using Lexington.View;
using Lexington.Command;
using System.Windows;
using System.Windows.Input;

namespace Lexington.ViewModel
{
    public class MainViewWindow: NotifyPropertyChanged
    {
        private MainWindow _MainWindow;

        private WeatherWindow _WeatherWindow;

        private ClockWindow _ClockWindow;

        private MemoWindow _MemoWindow;

        private MusicPlayerWindow _MusicPlayerWindow;


        private WifePic _Wife;

        private Weather _Weather;

        private WeatherService _WeatherService;

        private ChatText _ChatText;
        
        private int WifeIndex = 0;

        private int WifePicIndex = 1;

        private int PicFormatIndex = 0;

        private int DressCount = 8;

        private int ClickCount = 0;

        private string TmpText = string.Empty;

        private SemaphoreSlim DialogSemaphore = new SemaphoreSlim(1,1);

        private bool WifeState = false;

        private bool ReportVaild = false;




        public ICommand ChangeState { get; private set; }
        public ICommand ChangeDress { get; private set; }
        public ICommand ChangeWife { get; private set; }

        public ICommand ChangeFormat { get; private set; }

        public ICommand GetReport { get; private set; }
        public ICommand GetMatters { get; private set; }
        public ICommand GetMemo { get; private set; }

        public ICommand GetMusicPlayer { get; private set; }

        //public ICommand ShowMusicPlayer { get; private set; }








        public string DialogText
        {
            get { return _ChatText.Text; }
            set
            {
                if (_ChatText.Text != value)
                {
                    _ChatText.Text = value;
                    OnPropertyChanged(nameof(DialogText));
                }
            }
        }



        public string PicSource
        {
            get { return _Wife.SourceUri; }
            set {
                    if (_Wife.SourceUri != value)
                    {
                        _Wife.SourceUri = value;
                        OnPropertyChanged(nameof(PicSource));
                    }
            }
        }



        public MainViewWindow(MainWindow mainWindow)
        {
            _MainWindow = mainWindow;
            _WeatherService = new WeatherService();
            _WeatherService.GetWeather();
            _WeatherWindow = new WeatherWindow();


            _Wife = new WifePic();
            _Weather = new Weather();
            _ChatText = new ChatText();

            ChangeState = new RelayCommand(param=>NextWifeState());
            ChangeDress = new RelayCommand(param => NextDress());
            ChangeWife = new RelayCommand(param => NextWife());
            ChangeFormat = new RelayCommand(param => NextFormat());
            GetReport = new RelayCommand(param => OACReport());
            GetMatters = new RelayCommand(param => OACMatters());
            GetMemo = new RelayCommand(param => OACMemo());
            GetMusicPlayer = new RelayCommand(param=> OACMusicPlayer());


            PicSource = GetWife();
            TmpText = GlobalValue.FiveDaysWeather[0].WeatherNote;

            Task.Run(() => DisPlayMes());
        }


        private string GetWife()
        {
            string Source = System.Windows.Forms.Application.StartupPath + "\\Resource\\Wifes\\";
            Source += GlobalValue.Wifes[WifeIndex][0] + "\\" 
                    + GlobalValue.Wifes[WifeIndex][WifePicIndex] + "\\"
                    + GlobalValue.WifeStates[Convert.ToInt32(WifeState)]
                    +GlobalValue.PicFormats[PicFormatIndex];
            return Source;
        }


        private void NextWifeState()
        {
            WifeState = !WifeState;
            PicSource = GetWife();
        }

        private void NextDress()
        {
            WifePicIndex = (WifePicIndex + 1) % DressCount;
            WifePicIndex = WifePicIndex == 0 ? 1 : WifePicIndex;
            PicSource = GetWife();
        }

        private void NextWife()
        {
            WifeIndex = (WifeIndex + 1) % GlobalValue.NumbersOfWife;
            WifePicIndex = 1;
            if(GlobalValue.Wifes[WifeIndex][0] == "Lexington" && PicFormatIndex == 0)
            {
                DressCount = GlobalValue.Wifes[WifeIndex].Count - 1;
            }
            else
            {
                DressCount = GlobalValue.Wifes[WifeIndex].Count;
            }
            PicSource = GetWife();
        }

        private void NextFormat()
        {
            if(GlobalValue.Wifes[WifeIndex][0] == "Lexington" && PicFormatIndex == 0)
            {
                DressCount++;
            }
            else if(GlobalValue.Wifes[WifeIndex][0] == "Lexington")
            {
                DressCount--;
                if (GlobalValue.Wifes[WifeIndex][WifePicIndex] == "Mia")
                {
                    WifePicIndex = 1;
                }
            }

            PicFormatIndex = (PicFormatIndex + 1) % GlobalValue.NumberOfFormats;
            PicSource = GetWife();
        }

        private void OACReport()
        {
            if (ReportVaild)
            {
                ReportVaild = false;
                _WeatherWindow.Hide();
            }
            else
            {
                ReportVaild = true;
                _WeatherWindow.Show();
                _WeatherWindow.Activate();
            }
        }
        private void OACMatters()
        {
            if (_ClockWindow == null || !_ClockWindow.IsVisible)
            {
                _ClockWindow = new ClockWindow();
            }
            else
            {
                _ClockWindow.Activate();
                return;
            }

            _ClockWindow.Show();
            _ClockWindow.Activate();
            
        }

        private void OACMemo()
        {
            if (_MemoWindow == null || !_MemoWindow.IsVisible)
            {
                _MemoWindow = new MemoWindow();
            }
            else
            {
                _MemoWindow.Activate();
                return;
            }

            _MemoWindow.Show();
            _MemoWindow.Activate();

        }

        private void OACMusicPlayer()
        {
            if (_MusicPlayerWindow == null || !_MusicPlayerWindow.IsVisible)
            {
                _MusicPlayerWindow = new MusicPlayerWindow();
                SetNewWindowPosition(_MainWindow, _MusicPlayerWindow);

            }
            else
            {
                _MusicPlayerWindow.Activate();
                return;
            }

            _MusicPlayerWindow.Show();
            _MusicPlayerWindow.Activate();

        }


        public void DisplayText(int DialogState = 0)
        {
            if (DialogSemaphore.CurrentCount == 1) ClickCount = 0;
            ClickCount = (ClickCount % 3) + 1;
            switch(ClickCount)
            {
                case 1:
                    if (DialogSemaphore.CurrentCount == 1)
                    {
                        _MainWindow.DialogBorder.Visibility = Visibility.Visible;
                        DialogSemaphore.Wait();
                        if(DialogState == 1)
                        {
                            SetNewWindowPosition(_MainWindow,_WeatherWindow,100);
                            ReportVaild = true;
                            _WeatherWindow.Show();
                            _WeatherWindow.Activate();
                        }
                        Task.Run(() => DisplayTextAsync(DialogState));
                    }           
                    break;
                case 2:
                case 3:
                    break;
            }


        }

        private async Task DisplayTextAsync(int DialogState = 0)
        {


            foreach (char c in TmpText)
            {
                if (ClickCount == 2 || ClickCount == 3) break;
                await Task.Delay(_ChatText.Rate);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    DialogText += c;
                });
            }
            if(ClickCount ==1)
            {
                for (int i = 0; i < _ChatText.LiveTime * 1000; i++)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1));
                    if (ClickCount == 2 || ClickCount == 3) break;
                }

            }
            else if (ClickCount == 2)
            {
                DialogText = TmpText;
                for(int i=0;i< _ChatText.LiveTime *1000;i++)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1));
                    if (ClickCount == 3) break;

                }
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                _MainWindow.DialogBorder.Visibility = Visibility.Hidden;
                DialogText = string.Empty;
                if(DialogState == 1 && ReportVaild ==true)
                {
                    ReportVaild = false;
                    _WeatherWindow.Hide();
                }
            });

            DialogSemaphore.Release();


        }

        private async Task DisPlayMes()
        {
            while (true)
            {
                await GlobalValue.MesSemaphore.WaitAsync();
                await DialogSemaphore.WaitAsync();
                GlobalValue.Mes mes = GlobalValue.MesQue.Dequeue();
                string DiaText = string.Empty;
                if(mes.type == 0)
                {
                    bool isEx = GlobalValue.Matters[mes.index].IsExpedition;
                    if(isEx)
                    {
                        DiaText=DataTool.ExpenditionDialog(GlobalValue.Matters[mes.index]);
                    }
                    else
                    {
                        DiaText = DataTool.NormalMatterDialog(GlobalValue.Matters[mes.index]);
                    }
                }
                Application.Current.Dispatcher.Invoke(() => {
                    _MainWindow.DialogBorder.Visibility = Visibility.Visible;
                    DialogText = DiaText;
                });

                await Task.Delay(_ChatText.LiveTime * 1000);
                Application.Current.Dispatcher.Invoke(() => {
                    _MainWindow.DialogBorder.Visibility = Visibility.Hidden;
                    DialogText = string.Empty;
                });

                DialogSemaphore.Release();

            }
        }

        private void SetNewWindowPosition(Window FatherWindow,Window ChildWindow,double PosX = 0,double PoxY = 0)
        {
            double FatherWindowTop = FatherWindow.Top;
            double FatherWindowLeft = FatherWindow.Left;
            ChildWindow.Left = FatherWindowLeft + PosX;
            ChildWindow.Top = FatherWindowTop + PoxY;
        }


    }
}
