using Lexington.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexington.Model
{
    public class Matter : NotifyPropertyChanged
    {
        public string MatterName { get; set; } = string.Empty;
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        private double _Process { get; set; } = 0;

        public bool IsRunning { get; set; } = true;

        public bool IsExpedition { get; set; } = true;

        public bool IsRedo { get; set; } = false;

        public double Process
        {
            get { return _Process; }
            set
            {
                if (_Process != value)
                {
                    _Process = value;
                    OnPropertyChanged(nameof(Process));
                }
            }


        }
    }
}
