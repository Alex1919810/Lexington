using Lexington.Model;
using Lexington.Tools;
using Lexington.View;
using Lexington.Command;
using System.Windows.Input;

namespace Lexington.ViewModel
{
    internal class MemoViewWindow:NotifyPropertyChanged
    {
        private MemoWindow _MemoWindow;

        private MemoText _MemoText;

        private string FilePath;

        public ICommand SaveText { get; private set; }



        public MemoText Memo
        {
            get { return _MemoText; }
            set
            {
                if (_MemoText != value)
                {
                    _MemoText = value;
                    OnPropertyChanged(nameof(Memo));
                }
            }
        }

        public MemoViewWindow(MemoWindow memoWindow)
        {
            _MemoWindow = memoWindow;
            FilePath = FilesTool.FilePathCombine("Memo.xml", 0);
            _MemoText = new MemoText();
            Memo = FilesTool.DeserializeFromXml<MemoText>(FilePath);

            SaveText = new RelayCommand(param=>TextSave());
        }


        public void TextSave()
        {

            Memo.SaveTime = "保存时间 " + DateTime.Now.ToString();
            Memo.Text = _MemoWindow.MemoText.Text;
            FilesTool.SerializeToXml<MemoText>(Memo, FilePath);
        }
    }
}
