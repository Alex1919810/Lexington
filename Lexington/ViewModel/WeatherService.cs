using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using IPTools.Core;
using Lexington.Tools;

namespace Lexington.ViewModel
{
    public class WeatherService
    {
        private string City = string.Empty;
        private string CityCode = string.Empty;

        public WeatherService() 
        {
            GetPoint();
            //GetCityCode();
        }
        private void GetPoint()
        {

            //var search = new IP2Region.DbSearcher(Environment.CurrentDirectory + @"\ip2region.xdb");
            //var data = search.MemorySearch(GlobalValue.m_IP);
            //string[] s = data.Region.Split('|');
            //City = s[3] == "0" ? string.Empty : s[3].Replace("市", "");
            var ipinfo = IpTool.Search(GlobalValue.m_IP);
            City = ipinfo.City == string.Empty ? string.Empty : ipinfo.City;



        }

        private void GetCityCode()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("/Lexington;component/Resource/Files/CityCode.xml");
            XmlNodeList Citys = xmlDoc.GetElementsByTagName("root");
            CityCode = City==string.Empty?string.Empty: Citys[0].SelectSingleNode(City).InnerText;
        }

        public void GetWeather()
        {
            List<string> Notes = new List<string>();
            string we = string.Empty;
            string path = "/Lexington;component/Resource/Files/Weather/"+ DateTime.Now.ToString("yyyy-MM-dd");
            if(FilesTool.IsFileVaild(path))
            {
                we = FilesTool.XmlToString(path);
            }
            else
            {
                string para = "theCityCode=" + City + "&theUserID= ";
                we = HttpRequest.SendGet("http://ws.webxml.com.cn/WebServices/WeatherWS.asmx/getWeather", para);
                FilesTool.SaveWeather(we);
                
            }
            Notes = FilesTool.ParseWeather(we);
            if (Notes.Count == 1)
            {
                GlobalValue.FiveDaysWeather[0].WeatherNote = "未知地点";
                return;
            }
            string[] WeatherToday = Notes[4].Split("；");
            string Note = "提督，这是今天" + City + "的天气\n\n"+
                            WeatherToday[0] + "\n"+
                            WeatherToday[1]+"\n"+
                            WeatherToday[2]+"\n"+
                            Notes[5];
            GlobalValue.FiveDaysWeather[0].WeatherNote = Note;
            for (int i = 0;i<5;i++)
            {
                string[] weather = Notes[7 + i * 5].Split(" ");
                GlobalValue.FiveDaysWeather[i].Date = weather[0];
                GlobalValue.FiveDaysWeather[i].NowWeather = weather[1];
                GlobalValue.FiveDaysWeather[i].Temperature = Notes[8 + i * 5];
                GlobalValue.FiveDaysWeather[i].Wind = Notes[9 + i * 5];
            }
        }
    }
}
