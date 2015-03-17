using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhml.Mms.Business.Interfaces
{
    /// <summary>
    /// Provides an interface to manage business related state information contained in the request scope.
    /// </summary>
    public interface IBusinessState
    {
        /// <summary>
        /// Adds a result of validation perfomed during a buisness operation
        /// </summary>
        /// <param name="validationResult"></param>
        void AddValidationResult(Rhml.Mms.Model.ValidationResult validationResult);

        /// <summary>
        /// Gets or set a value indicating if the business operation is currently impersonating the system user.
        /// </summary>
        bool IsImpersonatingSystemUser { get; }

        /// <summary>
        /// Gets a value indicating if the business operation is valid.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Returns a collection of validation results produced during the business operation.
        /// </summary>
        System.Collections.Generic.IEnumerable<Rhml.Mms.Model.ValidationResult> ValidationResults { get; }

        /// <summary>
        /// Returns a scope object which causes CurrentUser to return a system user for the lifetime of the scope object.
        /// </summary>
        /// <returns></returns>
        IDisposable AsSystemUser();
    }
}
