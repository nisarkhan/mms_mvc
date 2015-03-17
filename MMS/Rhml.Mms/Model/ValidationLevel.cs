using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhml.Mms.Model
{
    public enum ValidationLevel
    {
        Error,
        Warning,
        Message
    }

    public class ValidationResult
    {
        public ValidationLevel Level { get; set; }
        public string Message { get; set; }
    }

}
