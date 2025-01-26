using LexingtonCustomControlLibrary;
using Lexington.Command;
using Lexington.Model;
using Lexington.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Lexington.ViewModel
{
    internal class ClockViewModel : NotifyPropertyChanged
    {


        public ICommand AddMatter { get; set; }

        public ICommand ChangeMatter { get; set; }

        public ICommand DeleteMatter { get; set; }


        private int TmpIndex = 0;


        public ObservableCollection<Matter> Matters
        {
            get { return GlobalValue.Matters; }
            set
            {
                if (GlobalValue.Matters != value)
                {
                    GlobalValue.Matters = value;
                    OnPropertyChanged(nameof(Matters));
                }
            }
        }

        public ClockViewModel()
        {

            AddMatter = new RelayCommand<object>(param => MatterAdd());
            ChangeMatter = new RelayCommand<object>(param => MatterChange(param));
            DeleteMatter = new RelayCommand<object>(param => MatterDelete(param));



        }

        private void MatterAdd()
        {
            AddMatterWindow addMatterWindow = new AddMatterWindow();
            addMatterWindow.ReturnValueUpdated += ChildWindow_AddMatter;
            addMatterWindow.ShowDialog();
        }

        private void ChildWindow_AddMatter(object sender, Matter e)
        {
            GlobalValue.MatterChangeSemaphore.Wait();
            Matters.Add(e);
            if (GlobalValue.ActiveMatter == 0) GlobalValue.MatterSemaphore.Release();
            GlobalValue.ActiveMatter++;
            GlobalValue.MatterChangeSemaphore.Release();

            AddMatterWindow childWindow = (AddMatterWindow)sender;
            childWindow.ReturnValueUpdated -= ChildWindow_AddMatter;
        }

        private void MatterChange(object param)
        {
            Matter tmpMatter = param as Matter;
            AddMatterWindow changeMatterWindow = new AddMatterWindow();
            changeMatterWindow.MatterList.Text = tmpMatter.MatterName;
            changeMatterWindow.StartTimePicker.Value = tmpMatter.StartTime;
            changeMatterWindow.EndTimePicker.Value = tmpMatter.EndTime;
            changeMatterWindow.RedoBox.IsChecked = tmpMatter.IsRedo;
            changeMatterWindow.ReturnValueUpdated += ChildWindow_ChangeMatter;
            TmpIndex = Matters.IndexOf(tmpMatter);
            changeMatterWindow.ShowDialog();

        }

        private void ChildWindow_ChangeMatter(object sender, Matter e)
        {
            GlobalValue.MatterChangeSemaphore.Wait();
            if (Matters[TmpIndex].IsRunning == false)
            {
                if (GlobalValue.ActiveMatter == 0) GlobalValue.MatterSemaphore.Release();
                GlobalValue.ActiveMatter++;
            }
            Matters[TmpIndex] = e;

            GlobalValue.MatterChangeSemaphore.Release();

            AddMatterWindow childWindow = (AddMatterWindow)sender;
            childWindow.ReturnValueUpdated -= ChildWindow_ChangeMatter;
        }


        private void MatterDelete(object param)
        {
            Matter tmpMatter = param as Matter;
            Matters.Remove(tmpMatter);

        }





    }
}
