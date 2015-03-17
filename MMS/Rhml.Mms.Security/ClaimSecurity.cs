using Republic.Core.Security.Interfaces;
using System;
using System.Collections.Generic;

namespace Rhml.Mms.Security
{
    /// <summary>
    /// Class ClaimSecurity
    /// </summary>
    public class ClaimSecurity : IAppSecurity
    {
        /// <summary>
        /// Indicates if the current context is authenticated
        /// </summary>
        /// <value><c>true</c> if this instance is authenticated; otherwise, <c>false</c>.</value>
        public bool IsAuthenticated
        {
            get
            {
                return (System.Threading.Thread.CurrentPrincipal != null &&
                        System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated);
            }
        }

        /// <summary>
        /// Returns the currently authenticated user if applicable.
        /// </summary>
        /// <returns>IAppUser.</returns>
        public IAppUser GetCurrentUser()
        {
            return new ClaimsUser((System.Security.Claims.ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal);
        }
    }
}
