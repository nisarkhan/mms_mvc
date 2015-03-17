using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Rhml.Mms.Interfaces
{
    /// <summary> interface for configuration manager to allow unit testing
    /// </summary>
    public interface IAppConfiguration
    {
        /// <summary> Get configuration information for the application
        /// security strategy used
        /// </summary>
        string AppSecurityStrategy { get; }

        /// <summary>  Get configuration information for the seeding strategy
        /// used for initializing the database
        /// </summary>
        string DataSeedStrategy { get; }

        // TODO: Add configurations applicable to your project
    }
}
