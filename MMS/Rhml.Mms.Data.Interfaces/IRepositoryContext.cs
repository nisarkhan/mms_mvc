using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhml.Mms.Data.Interfaces
{
    /// <summary> The unit of work context that 'owns' the current
    /// repository.  Use this context to persiste all related changes
    /// </summary>
    public interface IRepositoryContext
    {
        void Save();
    }
}
