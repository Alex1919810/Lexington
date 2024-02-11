using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Lexington.Tools
{
    public class WeatherTool
    {

        public XmlDocument GetProviceId()
        {
            string ProP = "河北,山西,辽宁,吉林,黑龙江,江苏,浙江,安徽,福建,江西,山东,河南,湖北,湖南,广东,海南,四川,贵州,云南,陕西,甘肃,青海,台湾,内蒙古,广西,西藏,宁夏,新疆, 北京,天津,上海,重庆,香港,澳门";
            string[] Pro = ProP.Split(',');
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(root);
            foreach (string pro in Pro)
            {
                string tmp = string.Empty;
                string Code = "theRegionCode=" + pro;
                tmp = HttpRequest.SendGet("http://ws.webxml.com.cn/WebServices/WeatherWS.asmx/getSupportCityString", Code, "utf-8");
                FilesTool.Parse(tmp, xmlDoc, root);
            }

            return xmlDoc;
        }
    }
}
