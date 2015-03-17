using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhml.Mms.Model;

namespace Rhml.Mms.Business
{
    public sealed class BusinessState : Rhml.Mms.Business.Interfaces.IBusinessState
    {
        public bool IsImpersonatingSystemUser { get; internal set; }

        private readonly List<ValidationResult> validationResults;
        public IEnumerable<ValidationResult> ValidationResults { get { return this.validationResults; } }

        public bool IsValid
        {
            get { return this.ValidationResults.Count() == 0; }
        }

        public BusinessState() { this.validationResults = new List<ValidationResult>(); }

        public void AddValidationResult(ValidationResult validationResult)
        {
            this.validationResults.Add(validationResult);
        }

        public IDisposable AsSystemUser()
        {
            return new SystemUserScope(this);
        }

        private class SystemUserScope : IDisposable
        {
            BusinessState _state;
            public SystemUserScope(BusinessState state)
            {
                this._state = state;
                this._state.IsImpersonatingSystemUser = true;
            }

            bool _isDisposed = false;
            public void Dispose()
            {
                if (_isDisposed) return;
                this._state.IsImpersonatingSystemUser = false;
                this._state = null;
                _isDisposed = true;
            }
        }
    }
}
