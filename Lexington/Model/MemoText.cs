using Lexington.BaseClass;

namespace Lexington.Model
{
    public class MemoText : NotifyPropertyChanged
    {
        private string M_Text { get; set; } = string.Empty;

        private string M_SaveTime { get; set; } = string.Empty;

        public string SaveTime
        {
            get { return M_SaveTime; }
            set
            {
                if (M_SaveTime != value)
                {
                    M_SaveTime = value;
                    OnPropertyChanged(nameof(SaveTime));
                }
            }

        }

        public string Text
        {
            get { return M_Text; }
            set
            {
                if (M_Text != value)
                {
                    M_Text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }

        }
    }
}
