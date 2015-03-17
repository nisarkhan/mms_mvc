using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhml.Mms.Business.Interfaces;

namespace Rhml.Mms.Business.Validation
{
    public abstract class ValidatorBase<T> where T : class
    {
        private IBusinessState state;

        protected ValidatorBase(IBusinessState state)
        {
            this.state = state;
        }

        public abstract bool Validate(T instance);

        protected bool IsValid { get { return this.state.IsValid; } }
        protected void AddError(string message, params object[] data)
        {
            state.AddValidationResult(new Model.ValidationResult()
            {
                Level = Model.ValidationLevel.Error,
                Message = string.Format(message, data)
            });
        }
    }
}
