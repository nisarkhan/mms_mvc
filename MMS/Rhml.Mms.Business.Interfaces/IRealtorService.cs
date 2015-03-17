using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhml.Mms;

namespace Rhml.Mms.Business.Interfaces
{
    /// <summary>
    /// Provides an interface to interact with Realtors
    /// </summary>
    public interface IRealtorService
    {
        /// <summary>
        /// New and edit the realtor master
        /// </summary>
        /// <param name="realtor"></param>
        void SaveRealtor(Realtors_Master realtor);
        /// <summary>
        /// Get all realtors masters
        /// </summary>
        /// <returns></returns>
        IEnumerable<Realtors_Master> GetRealtors();
        /// <summary>
        /// Get Realtor by Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Realtors_Master GetRealtor(Guid userId);
        /// <summary>
        /// Removes the specified reator and all its associated information
        /// </summary>
        /// <param name="userId"></param>
        void DeleteRealtor(Guid userId);
    }
}
