using Lexington.BaseClass;
using Lexington.Command;
using Lexington.Model;
using Lexington.Service;
using Lexington.Tools;
using Lexington.View;
using NAudio.Wave;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Lexington.ViewModel
{
    internal class MusicPlayerViewModel : BaseViewModel<MusicPlayerWindow>
    {
        private WaveOutEvent M_WaveOutEvent;

        private AudioFileReader M_AudioFileReader;

        private int M_MusicIndex = 0;

        private bool IsPlaying = false;

        private bool IsSlider = false;


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
        public ICommand ToNextMusic { get; set; }

        public ICommand ToLastMusic { get; set; }



        public MusicPlayerViewModel(MusicPlayerWindow musicPlayerWindow) : base(musicPlayerWindow)
        {
            InitService();
            InitializeData();
            InitializeAudio();
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
            ToNextMusic = new RelayCommand<object>(param=>NextMusic());
            ToLastMusic = new RelayCommand<object>(param => LastMusic());
        }

        private void InitializeData()
        {
            PlayPicUrl = GetPicUrl();

            Volume = 1.0;

            DataTool.LoadMusics();
        }

        private void InitializeAudio()
        {

            Music = GlobalValue.MusicsList[M_MusicIndex];
            string audioFilePath = Music.MusicPath;
            M_WaveOutEvent = new WaveOutEvent();
            M_AudioFileReader = new AudioFileReader(audioFilePath);
            M_WaveOutEvent.Init(M_AudioFileReader);
            M_WaveOutEvent.Volume = (float)Volume;

            InitTimer();

            M_Window.Closed += (sender,args) => M_WaveOutEvent.Dispose();


        }

        private void InitTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += MusicProcess_Tick;

            if (M_AudioFileReader != null)
            {
                TimeStamp = "00:00/" + M_AudioFileReader.TotalTime.ToString(@"mm\:ss");
            }
            else
            {
                TimeStamp = "00:00/00:00";
            }
        }


        private void  MusicClick()
        {
            IsPlaying = !IsPlaying;
            PlayPicUrl = GetPicUrl();
            TogglePlayPause();
        }

        private void TogglePlayPause()
        {
            if (IsPlaying)
            {
                 M_WaveOutEvent.Play();
                Timer.Start();
            }
            else
            {
                M_WaveOutEvent.Pause();
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
                Music.MusicProcess = M_AudioFileReader.CurrentTime.TotalSeconds;
                if (M_AudioFileReader.CurrentTime.TotalSeconds >= M_AudioFileReader.TotalTime.TotalSeconds)
                {
                    MusicStop();
                }
                TimeStamp = M_AudioFileReader.CurrentTime.ToString(@"mm\:ss") + "/" + M_AudioFileReader.TotalTime.ToString(@"mm\:ss");

            }
        }

        private void MouseDownSlider()
        {
            IsSlider = true;
        }

        private void MouseUpSlider()
        {
            // 释放鼠标时，应用目标进度
            M_AudioFileReader.CurrentTime = TimeSpan.FromSeconds(Music.MusicProcess);
            IsSlider = false; // 拖动结束，恢复更新界面
        }

        private void ValueChangedSlider()
        {
            if (IsSlider)
            {
                TimeStamp = TimeSpan.FromSeconds(Music.MusicProcess).ToString(@"mm\:ss") + "/" + M_AudioFileReader.TotalTime.ToString(@"mm\:ss");
            }
        }

        private void ValueChangedVolume()
        {
            M_WaveOutEvent.Volume = (float)Volume;
        }

        private void MusicStop()
        {
            IsPlaying = false;
            PlayPicUrl = GetPicUrl();
            Timer.Stop();
        }

        private void MusicPlay()
        {
            IsPlaying = true;
            PlayPicUrl = GetPicUrl();
            Timer.Start();
        }

        private void NextMusic()
        {
            StopAndDispose();
            M_MusicIndex = (M_MusicIndex +1) % GlobalValue.MusicsList.Count;
            ReInitAndStart();
        }

        private void LastMusic()
        {
            StopAndDispose();
            M_MusicIndex = (M_MusicIndex == 0) ? GlobalValue.MusicsList.Count - 1 : M_MusicIndex - 1;
            ReInitAndStart() ;

        }

        private void StopAndDispose()
        {
            M_WaveOutEvent.Stop();
            //M_WaveOutEvent.Dispose();

        }

        private void ReInitAndStart()
        {
            M_Music.ProcessClear();
            Music = GlobalValue.MusicsList[M_MusicIndex];
            M_AudioFileReader = new AudioFileReader(Music.MusicPath);
            M_WaveOutEvent.Init(M_AudioFileReader);
            MusicPlay();
            M_WaveOutEvent.Play();
        }




    }
}
