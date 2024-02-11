using Lexington.ViewModel;

namespace Lexington.Model
{
    internal class Music : NotifyPropertyChanged
    {
        private string _MusicName = string.Empty;

        private double _MusicProcess = 0.0;

        private double _MusicTime =0.0;

        public string MusicName
        {
            get { return _MusicName; }
            set
            {
                if (_MusicName != value)
                {
                    _MusicName = value;
                    OnPropertyChanged(nameof(MusicName));
                }
            }
        }

        public double MusicProcess
        {
            get { return _MusicProcess; }
            set
            {
                if (_MusicProcess != value)
                {
                    _MusicProcess = value;
                    OnPropertyChanged(nameof(MusicProcess));
                }
            }
        }

        public double MusicTime
        {
            get { return _MusicTime; }
            set
            {
                if (_MusicTime != value)
                {
                    _MusicTime = value;
                    OnPropertyChanged(nameof(MusicTime));
                }
            }
        }

        public Music(string Name ,double Time)
        {
            _MusicName = Name;
            _MusicProcess = 0;
            _MusicTime = Time;
        }
    }
}
