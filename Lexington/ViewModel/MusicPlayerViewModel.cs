using Lexington.BaseClass;
using Lexington.Command;
using Lexington.Model;
using Lexington.Service;
using Lexington.Tools;
using NAudio.Wave;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Lexington.ViewModel
{
    internal class MusicPlayerViewModel : NotifyPropertyChanged
    {
        private WaveOutEvent WaveOutEvent;

        private AudioFileReader AudioFileReader;

        private bool IsPlaying = false;

        private bool IsSlider = false;

        private bool IsVolume = false;

        private double M_Volume;

        private DispatcherTimer Timer;

        private string M_PlayPicUrl = string.Empty;

        private Music M_Music;

        private string M_TimeStamp = string.Empty;

        private WindowService M_WindowService;

        public string TimeStamp
        {
            get { return M_TimeStamp; }
            set
            {
                if (M_TimeStamp != value)
                {
                    M_TimeStamp = value;
                    OnPropertyChanged(nameof(TimeStamp));
                }
            }
        }
        public Music Music
        {
            get
            {
                return M_Music;
            }
            set
            {
                if (M_Music != value)
                {
                    M_Music = value;
                }
                OnPropertyChanged(nameof(Music));
            }
        }


        public string PlayPicUrl
        {
            get
            {
                return M_PlayPicUrl;
            }
            set
            {
                if (M_PlayPicUrl != value)
                {
                    M_PlayPicUrl = value;
                }
                OnPropertyChanged(nameof(PlayPicUrl));
            }
        }

        public double Volume
        {
            get
            {
                return M_Volume;
            }
            set
            {
                if (M_Volume != value)
                {
                    M_Volume = value;
                }
                OnPropertyChanged(nameof(Volume));
            }
        }

        public WindowService WindowSer
        {
            get
            {
                return M_WindowService;
            }
            set
            {
                if (M_WindowService != value)
                {
                    M_WindowService = value;
                }
                OnPropertyChanged(nameof(WindowSer));
            }
        }


        public ICommand PlayMusic { get; set; }

        public ICommand SliderMouseDown { get; set; }

        public ICommand SliderMouseUp { get; set; }

        public ICommand SliderValueChange { get; set; }
        public ICommand VolumeValueChange { get; set; }


        public MusicPlayerViewModel()
        {
            InitService();
            InitializeAudio();
            InitializeData();
            InitializeCommand();
        }

        private void InitService()
        {
            M_WindowService = new WindowService();
        }

        private void InitializeCommand()
        {
            PlayMusic = new RelayCommand<object>(param => MusicClick());
            SliderMouseDown = new RelayCommand<object>(param => MouseDownSlider());
            SliderMouseUp = new RelayCommand<object>(param => MouseUpSlider());
            SliderValueChange = new RelayCommand<object>(param => ValueChangedSlider());
            VolumeValueChange = new RelayCommand<object>(param => ValueChangedVolume());
        }

        private void InitializeData()
        {
            PlayPicUrl = GetPicUrl();

            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += MusicProcess_Tick;

            if (AudioFileReader != null)
            {
                TimeStamp = "00:00/" + AudioFileReader.TotalTime.ToString(@"mm\:ss");
            }
            else
            {
                TimeStamp = "00:00/00:00";
            }
        }

        private void InitializeAudio()
        {
            string audioFilePath = FilesTool.FilePathCombine("Musics/test.mp3", 0);

            string MusicName = "test";
            WaveOutEvent = new WaveOutEvent();
            AudioFileReader = new AudioFileReader(audioFilePath);
            WaveOutEvent.Init(AudioFileReader);
            WaveOutEvent.Volume = (float)Volume;
            // 当播放完成时，切换按钮状态为暂停
            WaveOutEvent.PlaybackStopped += (sender, e) =>
            {
                IsPlaying = false;
                PlayPicUrl = GetPicUrl();
                Timer.Stop();
            };

            Music = new Music(MusicName, AudioFileReader.TotalTime.TotalSeconds);
        }

        private async void MusicClick()
        {
            IsPlaying = !IsPlaying;
            PlayPicUrl = GetPicUrl();
            await Task.Run(() => TogglePlayPause());
        }

        private void TogglePlayPause()
        {
            if (IsPlaying)
            {
                Application.Current.Dispatcher.Invoke(() => WaveOutEvent.Play());
                Timer.Start();
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() => WaveOutEvent.Pause());
                Timer.Stop();
            }
        }

        private string GetPicUrl()
        {
            string s = string.Empty;
            if (IsPlaying)
            {
                s = FilesTool.FilePathCombine("Button/Pause.png", 1);
            }
            else
            {
                s = FilesTool.FilePathCombine("Button/Play.png", 1);
            }
            return s;
        }

        private void MusicProcess_Tick(object sender, EventArgs e)
        {
            if (!IsSlider)
            {
                Music.MusicProcess = AudioFileReader.CurrentTime.TotalSeconds;
                TimeStamp = AudioFileReader.CurrentTime.ToString(@"mm\:ss") + "/" + AudioFileReader.TotalTime.ToString(@"mm\:ss");
            }
        }

        private void MouseDownSlider()
        {
            IsSlider = true;
        }

        private void MouseUpSlider()
        {
            // 释放鼠标时，应用目标进度
            AudioFileReader.CurrentTime = TimeSpan.FromSeconds(Music.MusicProcess);
            IsSlider = false; // 拖动结束，恢复更新界面
        }

        private void ValueChangedSlider()
        {
            if (IsSlider)
            {
                TimeStamp = TimeSpan.FromSeconds(Music.MusicProcess).ToString(@"mm\:ss") + "/" + AudioFileReader.TotalTime.ToString(@"mm\:ss");
            }
        }



        private void ValueChangedVolume()
        {
            WaveOutEvent.Volume = (float)Volume;
        }



    }
}
