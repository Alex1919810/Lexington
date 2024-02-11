using Lexington.Tools;
using Lexington.Model;
using Lexington.Command;
using System.Windows.Input;
using NAudio.Wave;
using System.Windows;
using System.Windows.Threading;
using Lexington.Inteface;

namespace Lexington.ViewModel
{
    internal class MusicPlayerViewModel:NotifyPropertyChanged
    {
        private WaveOutEvent WaveOutEvent;

        private AudioFileReader AudioFileReader;

        private bool IsPlaying = false;

        private bool IsSlider = false;

        private DispatcherTimer Timer;

        private string _PlayPicUrl = string.Empty;

        private Music _Music;

        private string _TimeStamp = string.Empty;

        public string TimeStamp
        {
            get { return _TimeStamp; }
            set
            {
                if(_TimeStamp != value)
                {
                    _TimeStamp = value;
                    OnPropertyChanged(nameof(TimeStamp));
                }
            }
        }
        public Music Music
        {
            get
            {
                return _Music;
            }
            set
            {
                if (_Music != value)
                {
                    _Music = value;
                }
                OnPropertyChanged(nameof(Music));
            }
        }


        public string PlayPicUrl
        {
            get
            {
                return _PlayPicUrl;
            }
            set
            {
                if (_PlayPicUrl != value)
                {
                    _PlayPicUrl = value;
                }
                OnPropertyChanged(nameof(PlayPicUrl));
            }
        }
        public ICommand PlayMusic {  get; set; }

        public ICommand SliderMouseDown { get; set; }

        public ICommand SliderMouseUp { get; set; }

        public ICommand SliderValueChange { get; set; }

        public MusicPlayerViewModel() 
        {
            InitializeAudio();
            InitializeData();
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            PlayMusic = new RelayCommand(param => MusicClick());
            SliderMouseDown = new RelayCommand(param => MouseDownSlider());
            SliderMouseUp = new RelayCommand(param => MouseUpSlider());
            SliderValueChange =new RelayCommand(param => ValueChangedSlider());
        }

        private void InitializeData()
        {
            PlayPicUrl = GetPicUrl();

            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += MusicProcess_Tick;    
            
            if(AudioFileReader!=null)
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

            // 当播放完成时，切换按钮状态为暂停
            WaveOutEvent.PlaybackStopped += (sender, e) =>
            {
                IsPlaying = false;
                PlayPicUrl = GetPicUrl();
                Timer.Stop();
            };

            Music = new Music(MusicName,AudioFileReader.TotalTime.TotalSeconds);
        }

        private async void MusicClick()
        {
            IsPlaying = !IsPlaying;
            PlayPicUrl = GetPicUrl();
            await Task.Run(()=>TogglePlayPause());
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
            if(IsPlaying)
            {
                s = FilesTool.FilePathCombine("Button/Pause.png",1);
            }
            else
            {
                s = FilesTool.FilePathCombine("Button/Play.png", 1);
            }
            return s;
        }

        private void MusicProcess_Tick(object sender, EventArgs e)
        {
            if(!IsSlider)
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



    }
}
