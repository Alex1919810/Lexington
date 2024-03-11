using Lexington.Model;
using Lexington.Tools;
using System.Collections.ObjectModel;

namespace Lexington
{
    internal static class GlobalValue
    {
        public struct Mes
        {
            public int type;
            public int index;
        }

        public static List<List<string>> Wifes = new List<List<string>>()
        {
            new List<string> { "Lexington","Normal", "Remodel" , "WeddingDress", "BiKiNi", "Flower", "Red","Moon","Mia" }
        };

        public static string[] WifeStates = new string[2] { "Normal", "Damage" };

        public static string[] PicFormats = new string[2] { ".gif", ".png" };

        public static int NumbersOfWife = Wifes.Count;

        public static int NumberOfFormats = PicFormats.Length;

        public static string m_IP = HttpRequest.GetIPFromHtml(HttpRequest.HttpGetPageHtml("http://www.net.cn/static/customercare/yourip.asp", "utf-8"));

        public static Weather[] FiveDaysWeather = new Weather[5]
        {
                new Weather() {},
                new Weather() {},
                new Weather() {},
                new Weather() {},
                new Weather() {}
        };

        public static ResoureCount ResourceGetTotal = new ResoureCount();

        public static ObservableCollection<Matter> Matters = Tools.FilesTool.DeserializeCollectionFromXml<Matter>(Tools.FilesTool.FilePathCombine("ClockMatters.xml", 0));

        public static Queue<Mes> MesQue = new Queue<Mes>();

        public static SemaphoreSlim MesSemaphore = new SemaphoreSlim(0);

        public static int ActiveMatter = Tools.DataTool.CountActiveMatter();

        public static SemaphoreSlim MatterChangeSemaphore = new SemaphoreSlim(1, 1);

        public static SemaphoreSlim MatterSemaphore = new SemaphoreSlim(0);

        public static ObservableCollection<Music> MusicsList = new ObservableCollection<Music>();





        static GlobalValue()
        {

        }
    }
}
