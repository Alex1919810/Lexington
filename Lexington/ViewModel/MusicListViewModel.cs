using Lexington.BaseClass;
using Lexington.Command;
using Lexington.Model;
using Lexington.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace Lexington.ViewModel
{
    internal class MusicListViewModel : BaseViewModel<MusicListWindow>
    {
        private double M_OffsetX;
        private double M_OffsetY;


        public delegate void MusicHandler(int Index);

        public MusicHandler SwitchMusicHandler;

        public ObservableCollection<Music> MusicLists
        {
            get
            {
                return GlobalValue.MusicsList;
            }
            set
            {
                if (GlobalValue.MusicsList != value)
                {
                    GlobalValue.MusicsList = value;
                    OnPropertyChanged(nameof(MusicLists));
                }
            }
        }

        public ICommand ToSwitchMusic { get; private set; }


        public MusicListViewModel(MusicListWindow musicListWindow) : base(musicListWindow)
        {
            M_OffsetX = 0;
            M_OffsetY = -100;

            ToSwitchMusic = new RelayCommand<object>(param=>TriggerSwitch(param));

            M_Window.Loaded += ChildWindow_Loaded;
            M_Window.Owner.LocationChanged += Owner_LocationChanged;
        }

        private void ChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePosition();
        }

        private void Owner_LocationChanged(object sender, EventArgs e)
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            Window owner = M_Window.Owner;
            if (owner != null)
            {
                M_Window.Left = owner.Left - M_OffsetX;
                M_Window.Top = owner.Top - M_OffsetY;
            }
        }

        private void TriggerSwitch(object param)
        {
            SwitchMusicHandler?.Invoke((int)param);
        }

    }
}


