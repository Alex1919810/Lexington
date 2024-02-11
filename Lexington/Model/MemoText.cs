using Lexington.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexington.Model
{
    public class MemoText: NotifyPropertyChanged
    {
        private string _Text { get; set; } = string.Empty;

        private string _SaveTime { get; set; } = string.Empty;

        public string SaveTime {
            get { return _SaveTime; }
            set
            {
                if (_SaveTime!= value)
                {
                    _SaveTime = value;
                    OnPropertyChanged(nameof(SaveTime));
                }
            }

        }

        public string Text
        {
            get { return _Text; }
            set
            {
                if (_Text != value)
                {
                    _Text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }

        }
    }
}
