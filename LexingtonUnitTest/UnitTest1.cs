using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using System.Xml;
using Lexington.Tools;
using Lexington.ViewModel;
using System.Collections.ObjectModel;
using Lexington;
using Lexington.View;
using Lexington.Model;
using System.Reflection;
namespace LexingtonUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            WeatherTool weather = new Lexington.Tools.WeatherTool();
            XmlDocument xmlDoc=weather.GetProviceId();
            string currentDirectory = Environment.CurrentDirectory;

            // ָ��XML�ļ�·��
            string filePath = Path.Combine(currentDirectory, "example.xml");

            // ����XML�ĵ����ļ�
            xmlDoc.Save(filePath);
            Console.WriteLine($"XML�ĵ��Ѵ���������Ϊ {filePath} �ļ���");

            // ���ж��Ի����������߼�
            Assert.IsTrue(File.Exists(filePath));
        }


        [TestMethod]
        public void TestMethod2()
        {
            WeatherTool weather = new Lexington.Tools.WeatherTool();
            XmlDocument xmlDoc = weather.GetProviceId();
            FilesTool.SaveWeather(xmlDoc.OuterXml);
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

            FilesTool.SerializeCollectionToXml<Matter>(m,folderPath);

            ObservableCollection<Matter> t =FilesTool.DeserializeCollectionFromXml<Matter>(folderPath);
            Console.WriteLine(t.ToString());
        }


    }
}