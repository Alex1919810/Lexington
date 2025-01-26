using LexingtonCustomControlLibrary;

namespace Lexington.Model
{
    public class Matter : NotifyPropertyChanged
    {
        public string MatterName { get; set; } = string.Empty;
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        private double M_Process { get; set; } = 0;

        public bool IsRunning { get; set; } = true;

        public bool IsExpedition { get; set; } = true;

        public bool IsRedo { get; set; } = false;

        public double Process
        {
            get { return M_Process; }
            set
            {
                if (M_Process != value)
                {
                    M_Process = value;
                    OnPropertyChanged(nameof(Process));
                }
            }


        }
    }
}
