using Republic.Core.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Rhml.Mms.Security
{
    /// <summary>
    /// Class ClaimsUser
    /// </summary>
    public class ClaimsUser : IAppUser
    {
        private ClaimsPrincipal _principal;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimsUser"/> class.
        /// </summary>
        public ClaimsUser()
        {
            _principal = System.Threading.Thread.CurrentPrincipal as ClaimsPrincipal;
            CheckPrincipal();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimsUser"/> class.
        /// </summary>
        /// <param name="principal">The principal.</param>
        public ClaimsUser(ClaimsPrincipal principal)
        {
            _principal = principal;
            CheckPrincipal();
        }

        /// <summary>
        /// Checks the principal.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Cannot create ClaimsUser without a principal of type 'System.Security.Claims.ClaimsPrincipal'</exception>
        private void CheckPrincipal()
        {
            if (_principal == null)
                throw new InvalidOperationException("Cannot create ClaimsUser without a principal of type 'System.Security.Claims.ClaimsPrincipal'");
        }

        /// <summary>
        /// Gets the claim value.
        /// </summary>
        /// <param name="claimName">Name of the claim.</param>
        /// <returns>System.String.</returns>
        private string GetClaimValue(string claimName)
        {
            var claim = _principal.FindFirst(claimName);
            return (claim == null) ? string.Empty : claim.Value;
        }

        /// <summary>
        /// user identifier
        /// </summary>
        /// <value>The user id.</value>
        public Guid UserId
        {
            get { return Guid.Parse(GetClaimValue(ClaimTypes.NameIdentifier)); }
        }

        /// <summary>
        /// user first name
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName
        {
            get { return GetClaimValue(ClaimTypes.Name); }
        }

        /// <summary>
        /// user last name
        /// </summary>
        /// <value>The last name.</value>
        public string LastName
        {
            get { return GetClaimValue(ClaimTypes.Surname); }
        }

        /// <summary>
        /// user's main email identifier
        /// </summary>
        /// <value>The email.</value>
        public string Email
        {
            get { return GetClaimValue(ClaimTypes.Email); }
        }

        /// <summary>
        /// application roles available to the user
        /// </summary>
        /// <value>The roles.</value>
        public IEnumerable<string> Roles
        {
            get
            {
                return new System.Collections.ObjectModel.ReadOnlyCollection<string>(
                    (from claim in _principal.Claims
                     where claim.Type == ClaimTypes.Role
                     select claim.Value).ToList());
            }
        }

        /// <summary>
        /// application privileges available to the user
        /// </summary>
        /// <value>The roles.</value>
        public IEnumerable<string> Privileges
        {
            get
            {
                return new System.Collections.ObjectModel.ReadOnlyCollection<string>(
                    (from claim in _principal.Claims
                     where claim.Type == Republic.Core.Security.ClaimType.Privilege
                     select claim.Value).ToList());
            }
        }

        /// <summary>Gets the claims for the current user.</summary>
        public IEnumerable<Claim> Claims
        {
            get { return _principal.Claims; }
        }



        public IEnumerable<string> Permissions
        {
            get { throw new NotImplementedException(); }
        }

        public string Username
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
