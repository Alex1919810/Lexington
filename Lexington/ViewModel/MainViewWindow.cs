using Lexington.Command;
using Lexington.Model;
using Lexington.Service;
using Lexington.Tools;
using Lexington.View;
using Lexington.BaseClass;
using System.Windows;
using System.Windows.Input;

namespace Lexington.ViewModel
{
    public class MainViewWindow : BViewModel<MainWindow>
    {

        private WeatherWindow M_WeatherWindow;


        private WeatherService M_WeatherService;

        private ChatText M_ChatText;

        private int ClickCount = 0;

        private string TmpText = string.Empty;

        private SemaphoreSlim DialogSemaphore = new SemaphoreSlim(1, 1);

        private bool ReportVaild = false;

        private WifeChangeService M_WifeService;

        private WindowService M_WindowService;


        public ICommand DialogTextDisplay {  get;private set; }


        public string DialogText
        {
            get { return M_ChatText.Text; }
            set
            {
                if (M_ChatText.Text != value)
                {
                    M_ChatText.Text = value;
                    OnPropertyChanged(nameof(DialogText));
                }
            }
        }


        public WifeChangeService WifeSer
        {
            get { return M_WifeService; }
            set
            {
                if (M_WifeService != value)
                {
                    M_WifeService = value;
                    OnPropertyChanged(nameof(WifeSer));
                }
            }
        }

        public WindowService WindowSer
        {
            get { return M_WindowService; }
            set
            {
                if (M_WindowService != value)
                {
                    M_WindowService = value;
                    OnPropertyChanged(nameof(WindowSer));
                }
            }
        }


        public MainViewWindow(MainWindow mainWindow):base(mainWindow)
        {
            InitData();
            InitCommand();
            StartTask();
        }

        private void InitData()
        {
            M_WeatherService = new WeatherService();
            M_WeatherService.GetWeather();
            M_WeatherWindow = new WeatherWindow();

            M_WindowService = new WindowService(M_Window);

            M_ChatText = new ChatText();

            M_WifeService = new WifeChangeService();

            TmpText = GlobalValue.FiveDaysWeather[0].WeatherNote;
        }

        private void InitCommand()
        {
            DialogTextDisplay = new RelayCommand<string>(param => DisplayText(int.Parse(param)));
        }

        private void StartTask()
        {
            Task.Run(() => DisPlayMes());
        }




        public void DisplayText(int DialogState = 0)
        {
            if (DialogSemaphore.CurrentCount == 1) ClickCount = 0;
            ClickCount = (ClickCount % 3) + 1;
            switch (ClickCount)
            {
                case 1:
                    if (DialogSemaphore.CurrentCount == 1)
                    {
                        M_Window.DialogBorder.Visibility = Visibility.Visible;
                        DialogSemaphore.Wait();
                        if (DialogState == 1)
                        {
                            SetNewWindowPosition(M_Window, M_WeatherWindow, 100);
                            ReportVaild = true;
                            M_WeatherWindow.Show();
                            M_WeatherWindow.Activate();
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
                await Task.Delay(M_ChatText.Rate);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    DialogText += c;
                });
            }
            if (ClickCount == 1)
            {
                for (int i = 0; i < M_ChatText.LiveTime * 1000; i++)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1));
                    if (ClickCount == 2 || ClickCount == 3) break;
                }

            }
            else if (ClickCount == 2)
            {
                DialogText = TmpText;
                for (int i = 0; i < M_ChatText.LiveTime * 1000; i++)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1));
                    if (ClickCount == 3) break;

                }
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                M_Window.DialogBorder.Visibility = Visibility.Hidden;
                DialogText = string.Empty;
                if (DialogState == 1 && ReportVaild == true)
                {
                    ReportVaild = false;
                    M_WeatherWindow.Hide();
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
                if (mes.type == 0)
                {
                    bool isEx = GlobalValue.Matters[mes.index].IsExpedition;
                    if (isEx)
                    {
                        DiaText = DataTool.ExpenditionDialog(GlobalValue.Matters[mes.index]);
                    }
                    else
                    {
                        DiaText = DataTool.NormalMatterDialog(GlobalValue.Matters[mes.index]);
                    }
                }
                Application.Current.Dispatcher.Invoke(() =>
                {
                    M_Window.DialogBorder.Visibility = Visibility.Visible;
                    DialogText = DiaText;
                });

                await Task.Delay(M_ChatText.LiveTime * 1000);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    M_Window.DialogBorder.Visibility = Visibility.Hidden;
                    DialogText = string.Empty;
                });

                DialogSemaphore.Release();

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
