using LexingtonCustomControlLibrary;
using Lexington.Command;
using Lexington.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace Lexington.Service
{
    public class WifeChangeService:NotifyPropertyChanged
    {

        private int WifeIndex = 0;

        private int WifePicIndex = 1;

        private int PicFormatIndex = 0;

        private int DressCount = 8;

        private bool WifeState = false;

        private WifePic M_Wife;

        public string PicSource
        {
            get { return M_Wife.SourceUri; }
            set
            {
                if (M_Wife.SourceUri != value)
                {
                    M_Wife.SourceUri = value;
                    OnPropertyChanged(nameof(PicSource));
                }
            }
        }

        public ICommand ChangeState { get; private set; }
        public ICommand ChangeDress { get; private set; }
        public ICommand ChangeWife { get; private set; }

        public ICommand ChangeFormat { get; private set; }

        public WifeChangeService()
        {
            M_Wife = new WifePic();
            PicSource = GetWife();

            ChangeState = new RelayCommand<object>(param => NextWifeState());
            ChangeDress = new RelayCommand<object>(param => NextDress());
            ChangeWife = new RelayCommand<object>(param =>NextWife());
            ChangeFormat = new RelayCommand<object>(param =>NextFormat());
        }

        private string GetWife()
        {
            string Source = System.Windows.Forms.Application.StartupPath + "\\Resource\\Wifes\\";
            Source += GlobalValue.Wifes[WifeIndex][0] + "\\"
                    + GlobalValue.Wifes[WifeIndex][WifePicIndex] + "\\"
                    + GlobalValue.WifeStates[Convert.ToInt32(WifeState)]
                    + GlobalValue.PicFormats[PicFormatIndex];
            return Source;
        }


        public void NextWifeState()
        {
            WifeState = !WifeState;
            PicSource = GetWife();
        }

        public void NextDress()
        {
            WifePicIndex = (WifePicIndex + 1) % DressCount;
            WifePicIndex = WifePicIndex == 0 ? 1 : WifePicIndex;
            PicSource = GetWife();
        }

        public void NextWife()
        {
            WifeIndex = (WifeIndex + 1) % GlobalValue.NumbersOfWife;
            WifePicIndex = 1;
            if (GlobalValue.Wifes[WifeIndex][0] == "Lexington" && PicFormatIndex == 0)
            {
                DressCount = GlobalValue.Wifes[WifeIndex].Count - 1;
            }
            else
            {
                DressCount = GlobalValue.Wifes[WifeIndex].Count;
            }
            PicSource = GetWife();
        }

        public void NextFormat()
        {
            if (GlobalValue.Wifes[WifeIndex][0] == "Lexington" && PicFormatIndex == 0)
            {
                DressCount++;
            }
            else if (GlobalValue.Wifes[WifeIndex][0] == "Lexington")
            {
                DressCount--;
                if (GlobalValue.Wifes[WifeIndex][WifePicIndex] == "Mia")
                {
                    WifePicIndex = 1;
                }
            }

            PicFormatIndex = (PicFormatIndex + 1) % GlobalValue.NumberOfFormats;
            PicSource = GetWife();
        }
    }
}
