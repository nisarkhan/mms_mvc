using Dot.Core.Interfaces;
using Dot.Core.Web;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace Dot.MMS.Web.Utility
{
    public class SystemUtil
    {
        private IServiceLocator _service;
        private ILogger _log;

        /// <summary> Constructor.  The logging service is dependency injected into this object
        /// </summary>
        /// <param name="service"></param>
        public SystemUtil(IServiceLocator service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
            _service = service;
            _log = _service.Locate<ILogger>();
        }

        /// <summary>  Writes a custom audit trail to DB(not Log4Net)
        /// </summary>
        /// <param name="message">The message to write</param>
        public void Log(string message)
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                var request = HttpContext.Current.Request;
                
                AuditInfo audit = new AuditInfo()
                {                    
                    UserName = (request.IsAuthenticated) ? HttpContext.Current.User.Identity.Name : "Unknown",
                    IPAddress = LocalIPAddress(),
                    AreaAccessed = request.RawUrl,
                    Message = message,
                    Timestamp = DateTime.Now
                };
                LogIt(audit);
            }
        }        

        private static string LocalIPAddress()
        {
            IPHostEntry host;
            StringBuilder localIP = new StringBuilder();
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP.Append(ip.ToString() + " / ");
                }
            }
            return localIP.ToString().Remove(localIP.ToString().Length - 3);
        }

        private void LogIt(AuditInfo audit)
        {
            _log.Log("UserName: " + audit.UserName + " IpAddress: " + audit.IPAddress + " Area Accessed: " + audit.AreaAccessed + " Message: " + audit.Message);
        }
        
    }
}