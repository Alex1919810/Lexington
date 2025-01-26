using LexingtonCustomControlLibrary;

namespace Lexington.Model
{
    internal class Music : NotifyPropertyChanged
    {
        private string M_MusicName = string.Empty;

        private double M_MusicProcess = 0.0;

        private double M_MusicTime = 0.0;

        private string M_Path =string.Empty;

        public string MusicPath {  get=>M_Path;private set=>M_Path=value; }

        public string MusicName
        {
            get { return M_MusicName; }
            set
            {
                if (M_MusicName != value)
                {
                    M_MusicName = value;
                    OnPropertyChanged(nameof(MusicName));
                }
            }
        }

        public double MusicProcess
        {
            get { return M_MusicProcess; }
            set
            {
                if (M_MusicProcess != value)
                {
                    M_MusicProcess = value;
                    OnPropertyChanged(nameof(MusicProcess));
                }
            }
        }

        public double MusicTime
        {
            get { return M_MusicTime; }
            set
            {
                if (M_MusicTime != value)
                {
                    M_MusicTime = value;
                    OnPropertyChanged(nameof(MusicTime));
                }
            }
        }

        public Music(string Name, double Time, string Path)
        {
            M_MusicName = Name;
            M_MusicProcess = 0;
            M_MusicTime = Time;
            M_Path = Path;
        }

        public void ProcessClear()
        {
            M_MusicProcess = 0.0;
        }
    }
}
