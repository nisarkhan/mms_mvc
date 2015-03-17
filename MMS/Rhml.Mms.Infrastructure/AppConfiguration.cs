using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhml.Mms.Interfaces;

namespace Rhml.Mms.Infrastructure
{
    /// <summary> Encapsulated wrapper for testable configuration manager access.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Class is instantiated by Ninject")]
    internal class AppConfiguration : IAppConfiguration
    {

        /// <summary> Get configuration information for the application
        /// security strategy used
        /// </summary>
        public string AppSecurityStrategy
        {
            get { return GetAppSetting<string>("AppSecurityStrategy"); }
        }

        /// <summary>  Get configuration information for the seeding strategy
        /// used for initializing the database
        /// </summary>
        public string DataSeedStrategy
        {
            get { return GetAppSetting<string>("DataSeedStrategy"); }
        }

        public IEnumerable<Configuration.DomainElement> DomainConfigurations
        {
            get { return ((Configuration.DomainSection)ConfigurationManager.GetSection("domainNames")).Domains.Cast<Configuration.DomainElement>(); }
        }


        public string StsIdentityKey
        {
            get { return GetAppSetting<string>("StsIdentityKey"); }
        }
        public Uri StsUrl
        {
            get
            {
                var output = GetAppSetting<string>("StsUrl");
                if (!string.IsNullOrWhiteSpace(output))
                {
                    return new Uri(output);
                }
                else if (System.Web.HttpContext.Current != null)
                {
                    var Request = System.Web.HttpContext.Current.Request;
                    var url = new System.Web.Mvc.UrlHelper(Request.RequestContext);
                    output = string.Format("{0}://{1}{2}",
                        Request.Url.Scheme,
                        Request.Url.Authority,
                        url.Content("~"));
                }
                throw new InvalidOperationException("Cannot obtain application root Url.");
            }
        }


        public Guid ArmApplicationId
        {
            get { return Guid.Parse(GetAppSetting<string>("ArmApplicationId")); }
        }

        /// <summary> Gets the application settings  (wraps ConfigurationManager method of the same signature)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        static private T GetAppSetting<T>(string name) where T : class
        {
            return ConfigurationManager.AppSettings[name] as T;
        }
    }
}
