using Lexington.Model;
using Lexington.Tools;
using System.Windows;

namespace Lexington
{
    // <summary>
    // Interaction logic for App.xaml
    // </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += App_Startup;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }
        private async void App_Startup(object sender, StartupEventArgs e)
        {
            await Task.Run(() => ProcessChange());
        }

        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            string path = Tools.FilesTool.FilePathCombine("ClockMatters.xml", 0);
            Tools.FilesTool.SerializeCollectionToXml<Matter>(GlobalValue.Matters, path);
        }

        private async Task ProcessChange()
        {
            while (true)
            {
                await Task.Delay(1000);
                await GlobalValue.MatterChangeSemaphore.WaitAsync();
                DateTime currentTime = DateTime.Now;
                for (int i = 0; i < GlobalValue.Matters.Count; i++)
                {
                    var matter = GlobalValue.Matters[i];
                    DateTime? startTime = matter.StartTime;
                    DateTime? endTime = matter.EndTime;
                    if (startTime.HasValue && endTime.HasValue)
                    {
                        if (matter.IsRunning == true)
                        {
                            matter.Process = DataTool.CalculateProcess(startTime, endTime);
                            if (currentTime >= endTime)
                            {
                                DataTool.PushMesToQue(0, i);
                                if (matter.IsRedo)
                                {
                                    currentTime = currentTime.AddSeconds(-currentTime.Second);
                                    TimeSpan tmpTime = endTime.Value - startTime.Value;
                                    matter.StartTime = currentTime;
                                    matter.EndTime = currentTime + tmpTime;
                                    matter.Process = 0;
                                    matter.IsRunning = true;
                                }
                                else
                                {
                                    matter.IsRunning = false;
                                    GlobalValue.ActiveMatter--;
                                    matter.Process = 100;
                                }
                            }
                        }

                    }
                    else
                    {
                        matter.Process = 0;
                    }
                }
                GlobalValue.MatterChangeSemaphore.Release();
                if (GlobalValue.ActiveMatter == 0)
                {
                    await GlobalValue.MatterSemaphore.WaitAsync();
                }


            }

        }
    }

}
