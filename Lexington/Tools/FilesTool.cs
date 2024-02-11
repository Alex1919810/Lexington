using Lexington.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lexington.Tools
{
    public class FilesTool
    {
        public static bool IsFileVaild(string Path)
        {
            bool result = false;
            if (System.IO.File.Exists(Path))
            {
                result = true;
            }
            return result;
        }

        public static void Parse(string XmlStr, XmlDocument XmlFile, XmlElement Root)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(XmlStr);

            XmlNamespaceManager nsManager = new XmlNamespaceManager(xmlDoc.NameTable);
            nsManager.AddNamespace("ns", "http://WebXml.com.cn/");

            XmlNodeList nodes = xmlDoc.SelectNodes("//ns:string", nsManager);

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string inputString = node.InnerText;
                    string[] parts = inputString.Split(',');
                    XmlElement City = XmlFile.CreateElement(parts[0]);
                    City.InnerText = parts[1];
                    Root.AppendChild(City);
                }
            }
        }

        public static List<string> ParseWeather(string XmlStr)
        {
            List<string> list = new List<string>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(XmlStr);

            XmlNamespaceManager nsManager = new XmlNamespaceManager(xmlDoc.NameTable);
            nsManager.AddNamespace("ns", "http://WebXml.com.cn/");

            XmlNodeList nodes = xmlDoc.SelectNodes("//ns:string", nsManager);

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    list.Add(node.InnerText);
                }
            }
            return list;
        }

        public static void SaveWeather(string XmlStr)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(XmlStr);
            // 相对于 Lexingon;component/ 的路径
            string relativeFolderPath = "Resource/Files/Weather";

            // 获取程序集的位置
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;

            // 构建文件夹的完整路径
            string folderPath = Path.Combine(Path.GetDirectoryName(assemblyLocation), relativeFolderPath);

            // 确保文件夹存在，如果不存在则创建
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string Fname = DateTime.Now.ToString("yyyy-MM-dd") + ".xml";
            // 构建要保存的文件的完整路径
            string filePath = Path.Combine(folderPath, Fname);

            //Console.WriteLine(filePath);

            xmlDoc.Save(filePath);
        }

        public static string XmlToString(string Path)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(Path);

            string xmlString = xmlDoc.OuterXml;

            return xmlString;
        }

        public static ObservableCollection<string> ParseExpeditionOneAttribute(string Attribute)
        {
            ObservableCollection<string> numbers = new ObservableCollection<string>();
            string folderPath = FilePathCombine("Expedition.xml", 0);

            XDocument xmlDoc = XDocument.Load(folderPath);

            foreach (var element in xmlDoc.Descendants())
            {
                XAttribute attribute = element.Attribute(Attribute);
                if (attribute != null)
                {
                    numbers.Add(attribute.Value);
                    //Console.WriteLine(attribute.Value);
                }


            }

            return numbers;
        }

        public static List<Expedition> ParseExpeditionAllAttribute()
        {
            List<Expedition> Expeditions = new List<Expedition>();
            string folderPath = FilePathCombine("Expedition.xml", 0);

            XDocument xmlDoc = XDocument.Load(folderPath);

            foreach (var element in xmlDoc.Descendants())
            {
                Expedition expedition = new Expedition();
                expedition.ExpeditionName = element.Name.LocalName;
                foreach (var attribute in element.Attributes())
                {
                    if (attribute != null)
                    {
                        switch (attribute.Name.ToString())
                        {
                            case "编号":
                                expedition.ExpeditionId = attribute.Value;
                                break;
                            case "时间":
                                expedition.ExpeditionTime = attribute.Value;
                                break;
                            case "油料":
                                expedition.Oil = int.Parse(attribute.Value);
                                break;
                            case "弹药":
                                expedition.Munition = int.Parse(attribute.Value);
                                break;
                            case "钢材":
                                expedition.Steel = int.Parse(attribute.Value);
                                break;
                            case "铝材":
                                expedition.Aluminium = int.Parse(attribute.Value);
                                break;
                            case "快修":
                                expedition.QuickConstruction = int.Parse(attribute.Value);
                                break;
                            case "快建":
                                expedition.RapidRepair = int.Parse(attribute.Value);
                                break;
                            case "建造图纸":
                                expedition.ShipBlueprint = int.Parse(attribute.Value);
                                break;
                            case "装备图纸":
                                expedition.EquipmentBlueprint = int.Parse(attribute.Value);
                                break;
                        }
                    }
                }
                Expeditions.Add(expedition);
            }

            return Expeditions;
        }

        public static ResoureCount ParseExpeditionResource(string id)
        {
            ResoureCount expedition = new ResoureCount();
            string folderPath = FilePathCombine("Expedition.xml", 0);

            XDocument xmlDoc = XDocument.Load(folderPath);

            foreach (var element in xmlDoc.Descendants())
            {
                XAttribute x = element.Attribute("编号");
                if (x == null || x.Value != id) continue;
                foreach (var attribute in element.Attributes())
                {
                    if (attribute != null)
                    {
                        switch (attribute.Name.ToString())
                        {
                            case "油料":
                                expedition.Oil = int.Parse(attribute.Value);
                                break;
                            case "弹药":
                                expedition.Munition = int.Parse(attribute.Value);
                                break;
                            case "钢材":
                                expedition.Steel = int.Parse(attribute.Value);
                                break;
                            case "铝材":
                                expedition.Aluminium = int.Parse(attribute.Value);
                                break;
                            case "快修":
                                expedition.QuickConstruction = int.Parse(attribute.Value);
                                break;
                            case "快建":
                                expedition.RapidRepair = int.Parse(attribute.Value);
                                break;
                            case "建造图纸":
                                expedition.ShipBlueprint = int.Parse(attribute.Value);
                                break;
                            case "装备图纸":
                                expedition.EquipmentBlueprint = int.Parse(attribute.Value);
                                break;
                        }
                    }
                }
                break;
            }

            return expedition;
        }

        // 保存到 XML 文件
        public static void SerializeToXml<T>(T objiect, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            string directoryPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, objiect);
            }
        }

        // 从 XML 文件读取数据
        public static T DeserializeFromXml<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamReader reader = new StreamReader(filePath))
            {
                return (T)serializer.Deserialize(reader);
            }
        }


        public static void SerializeCollectionToXml<T>(IEnumerable<T> collection, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<T>));

            string directoryPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, new ObservableCollection<T>(collection));
            }
        }

        public static ObservableCollection<T> DeserializeCollectionFromXml<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<T>));

            using (StreamReader reader = new StreamReader(filePath))
            {
                return (ObservableCollection<T>)serializer.Deserialize(reader);
            }
        }

        public static string FilePathCombine(string FileName, int Type)
        {

            string relativeFolderPath = "Resource";
            switch (Type)
            {
                case 0: relativeFolderPath += "/Files/"; break;
                case 1: relativeFolderPath += "/Images/"; break;
                case 2: relativeFolderPath += "/Wifes/"; break;
            }

            // 获取程序集的位置
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;

            // 构建文件夹的完整路径
            string folderPath = Path.Combine(Path.GetDirectoryName(assemblyLocation), relativeFolderPath, FileName);
            return folderPath;
        }
    }
}
