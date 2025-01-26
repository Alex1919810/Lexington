using LexingtonCustomControlLibrary;
using Lexington.View;
using System.Windows;

namespace Lexington.ViewModel
{
    //刚学不知道，现在懒得重构了
    internal class WeatherViewModel : NotifyPropertyChanged
    {
        private WeatherWindow _weatherWindow;
        public string FirstDate
        {
            get { return GlobalValue.FiveDaysWeather[0].Date; }
            set
            {
                if (GlobalValue.FiveDaysWeather[0].Date != value)
                {
                    GlobalValue.FiveDaysWeather[0].Date = value;
                    OnPropertyChanged(nameof(FirstDate));
                }
            }
        }

        public string FirstWeather
        {
            get { return GlobalValue.FiveDaysWeather[0].NowWeather; }
            set
            {
                if (GlobalValue.FiveDaysWeather[0].NowWeather != value)
                {
                    GlobalValue.FiveDaysWeather[0].NowWeather = value;
                    OnPropertyChanged(nameof(FirstWeather));
                }
            }
        }

        public string FirstTemp
        {
            get { return GlobalValue.FiveDaysWeather[0].Temperature; }
            set
            {
                if (GlobalValue.FiveDaysWeather[0].Temperature != value)
                {
                    GlobalValue.FiveDaysWeather[0].Temperature = value;
                    OnPropertyChanged(nameof(FirstTemp));
                }
            }
        }

        public string FirstWind
        {
            get { return GlobalValue.FiveDaysWeather[0].Wind; }
            set
            {
                if (GlobalValue.FiveDaysWeather[0].Wind != value)
                {
                    GlobalValue.FiveDaysWeather[0].Wind = value;
                    OnPropertyChanged(nameof(FirstWind));
                }
            }
        }

        public string SecondDate
        {
            get { return GlobalValue.FiveDaysWeather[1].Date; }
            set
            {
                if (GlobalValue.FiveDaysWeather[1].Date != value)
                {
                    GlobalValue.FiveDaysWeather[1].Date = value;
                    OnPropertyChanged(nameof(SecondDate));
                }
            }
        }

        public string SecondWeather
        {
            get { return GlobalValue.FiveDaysWeather[1].NowWeather; }
            set
            {
                if (GlobalValue.FiveDaysWeather[1].NowWeather != value)
                {
                    GlobalValue.FiveDaysWeather[1].NowWeather = value;
                    OnPropertyChanged(nameof(SecondWeather));
                }
            }
        }

        public string SecondTemp
        {
            get { return GlobalValue.FiveDaysWeather[1].Temperature; }
            set
            {
                if (GlobalValue.FiveDaysWeather[1].Temperature != value)
                {
                    GlobalValue.FiveDaysWeather[1].Temperature = value;
                    OnPropertyChanged(nameof(SecondTemp));
                }
            }
        }

        public string SecondWind
        {
            get { return GlobalValue.FiveDaysWeather[1].Wind; }
            set
            {
                if (GlobalValue.FiveDaysWeather[1].Wind != value)
                {
                    GlobalValue.FiveDaysWeather[1].Wind = value;
                    OnPropertyChanged(nameof(SecondWind));
                }
            }
        }

        public string ThirdDate
        {
            get { return GlobalValue.FiveDaysWeather[2].Date; }
            set
            {
                if (GlobalValue.FiveDaysWeather[2].Date != value)
                {
                    GlobalValue.FiveDaysWeather[2].Date = value;
                    OnPropertyChanged(nameof(ThirdDate));
                }
            }
        }

        public string ThirdWeather
        {
            get { return GlobalValue.FiveDaysWeather[2].NowWeather; }
            set
            {
                if (GlobalValue.FiveDaysWeather[2].NowWeather != value)
                {
                    GlobalValue.FiveDaysWeather[2].NowWeather = value;
                    OnPropertyChanged(nameof(ThirdWeather));
                }
            }
        }

        public string ThirdTemp
        {
            get { return GlobalValue.FiveDaysWeather[2].Temperature; }
            set
            {
                if (GlobalValue.FiveDaysWeather[2].Temperature != value)
                {
                    GlobalValue.FiveDaysWeather[2].Temperature = value;
                    OnPropertyChanged(nameof(ThirdTemp));
                }
            }
        }

        public string ThirdWind
        {
            get { return GlobalValue.FiveDaysWeather[2].Wind; }
            set
            {
                if (GlobalValue.FiveDaysWeather[2].Wind != value)
                {
                    GlobalValue.FiveDaysWeather[2].Wind = value;
                    OnPropertyChanged(nameof(ThirdWind));
                }
            }
        }

        public string FourthDate
        {
            get { return GlobalValue.FiveDaysWeather[3].Date; }
            set
            {
                if (GlobalValue.FiveDaysWeather[3].Date != value)
                {
                    GlobalValue.FiveDaysWeather[3].Date = value;
                    OnPropertyChanged(nameof(FourthDate));
                }
            }
        }

        public string FourthWeather
        {
            get { return GlobalValue.FiveDaysWeather[3].NowWeather; }
            set
            {
                if (GlobalValue.FiveDaysWeather[3].NowWeather != value)
                {
                    GlobalValue.FiveDaysWeather[3].NowWeather = value;
                    OnPropertyChanged(nameof(FourthWeather));
                }
            }
        }

        public string FourthTemp
        {
            get { return GlobalValue.FiveDaysWeather[3].Temperature; }
            set
            {
                if (GlobalValue.FiveDaysWeather[3].Temperature != value)
                {
                    GlobalValue.FiveDaysWeather[3].Temperature = value;
                    OnPropertyChanged(nameof(FourthTemp));
                }
            }
        }

        public string FourthWind
        {
            get { return GlobalValue.FiveDaysWeather[3].Wind; }
            set
            {
                if (GlobalValue.FiveDaysWeather[3].Wind != value)
                {
                    GlobalValue.FiveDaysWeather[3].Wind = value;
                    OnPropertyChanged(nameof(FourthWind));
                }
            }
        }

        public string FifthDate
        {
            get { return GlobalValue.FiveDaysWeather[4].Date; }
            set
            {
                if (GlobalValue.FiveDaysWeather[4].Date != value)
                {
                    GlobalValue.FiveDaysWeather[4].Date = value;
                    OnPropertyChanged(nameof(FifthDate));
                }
            }
        }

        public string FifthWeather
        {
            get { return GlobalValue.FiveDaysWeather[4].NowWeather; }
            set
            {
                if (GlobalValue.FiveDaysWeather[4].NowWeather != value)
                {
                    GlobalValue.FiveDaysWeather[4].NowWeather = value;
                    OnPropertyChanged(nameof(FifthWeather));
                }
            }
        }

        public string FifthTemp
        {
            get { return GlobalValue.FiveDaysWeather[4].Temperature; }
            set
            {
                if (GlobalValue.FiveDaysWeather[4].Temperature != value)
                {
                    GlobalValue.FiveDaysWeather[4].Temperature = value;
                    OnPropertyChanged(nameof(FifthTemp));
                }
            }
        }

        public string FifthWind
        {
            get { return GlobalValue.FiveDaysWeather[4].Wind; }
            set
            {
                if (GlobalValue.FiveDaysWeather[4].Wind != value)
                {
                    GlobalValue.FiveDaysWeather[4].Wind = value;
                    OnPropertyChanged(nameof(FifthWind));
                }
            }
        }

        private Style SetStyle(string we)
        {
            Style Cloudy = (Style)Application.Current.Resources["Cloudy"];
            Style Sunny = (Style)Application.Current.Resources["Sunny"];
            if (we.Contains("雨") || we.Contains("阴"))
            {
                return Cloudy;
            }
            return Sunny;
        }


        private void SetWeather()
        {

            FirstDate = GlobalValue.FiveDaysWeather[0].Date;
            FirstWeather = GlobalValue.FiveDaysWeather[0].NowWeather;
            FirstTemp = GlobalValue.FiveDaysWeather[0].Temperature;
            FirstWind = GlobalValue.FiveDaysWeather[0].Wind;
            _weatherWindow.FirstBorder.Style = SetStyle(FirstWeather);

            SecondDate = GlobalValue.FiveDaysWeather[1].Date;
            SecondWeather = GlobalValue.FiveDaysWeather[1].NowWeather;
            SecondTemp = GlobalValue.FiveDaysWeather[1].Temperature;
            SecondWind = GlobalValue.FiveDaysWeather[1].Wind;
            _weatherWindow.SecondBorder.Style = SetStyle(SecondWeather);


            ThirdDate = GlobalValue.FiveDaysWeather[2].Date;
            ThirdWeather = GlobalValue.FiveDaysWeather[2].NowWeather;
            ThirdTemp = GlobalValue.FiveDaysWeather[2].Temperature;
            ThirdWind = GlobalValue.FiveDaysWeather[2].Wind;
            _weatherWindow.ThirdBorder.Style = SetStyle(ThirdWeather);


            FourthDate = GlobalValue.FiveDaysWeather[3].Date;
            FourthWeather = GlobalValue.FiveDaysWeather[3].NowWeather;
            FourthTemp = GlobalValue.FiveDaysWeather[3].Temperature;
            FourthWind = GlobalValue.FiveDaysWeather[3].Wind;
            _weatherWindow.FourthBorder.Style = SetStyle(FourthWeather);


            FifthDate = GlobalValue.FiveDaysWeather[4].Date;
            FifthWeather = GlobalValue.FiveDaysWeather[4].NowWeather;
            FifthTemp = GlobalValue.FiveDaysWeather[4].Temperature;
            FifthWind = GlobalValue.FiveDaysWeather[4].Wind;
            _weatherWindow.FifthBorder.Style = SetStyle(FifthWeather);



        }


        public WeatherViewModel(WeatherWindow weatherWindow)
        {
            _weatherWindow = weatherWindow;
            SetWeather();
        }
    }
}
