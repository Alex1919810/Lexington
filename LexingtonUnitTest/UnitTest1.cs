using Lexington.Model;
using Lexington.Tools;
using System.Collections.ObjectModel;
using System.Reflection;
namespace LexingtonUnitTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod2()
        {
            DataTool.LoadMusics();
        }

        [TestMethod]
        public void TestMethod3()
        {
            ObservableCollection<string> s = new ObservableCollection<string>();
            s = FilesTool.ParseExpeditionOneAttribute("���");
        }

        [TestMethod]
        public void TestMethod4()
        {
            ObservableCollection<Matter> m = new ObservableCollection<Matter>
            {
                new Matter {StartTime= new DateTime(1990, 1, 15, 12, 34, 56, 789)},
                new Matter {}
            };
            string relativeFolderPath = "Resource/Files/ClockMatters.xml";

            // ��ȡ���򼯵�λ��
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;

            // �����ļ��е�����·��
            string folderPath = Path.Combine(Path.GetDirectoryName(assemblyLocation), relativeFolderPath);

            FilesTool.SerializeCollectionToXml<Matter>(m, folderPath);

            ObservableCollection<Matter> t = FilesTool.DeserializeCollectionFromXml<Matter>(folderPath);
            Console.WriteLine(t.ToString());
        }


    }
}