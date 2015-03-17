using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhml.Mms.Business.Interfaces
{
    public interface IRealtorValidator
    {
        bool Validate(Realtors_Master instance);
    }
}
