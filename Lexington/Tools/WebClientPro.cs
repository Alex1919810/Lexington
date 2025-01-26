using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Lexington.Tools
{
    class WebClientPro : WebClient
    {
        public int m_timeOut { get; set; }

        public WebClientPro(int timeOut)
        {
            m_timeOut = timeOut;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest webRequest = (HttpWebRequest)base.GetWebRequest(address);
            webRequest.Timeout = m_timeOut;
            webRequest.ReadWriteTimeout = m_timeOut;
            return webRequest;
        }
    }
}
