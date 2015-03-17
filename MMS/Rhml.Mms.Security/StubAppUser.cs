using Republic.Core.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Rhml.Mms.Security 
{
    /// <summary>
    /// Exposed user properties (POCO)
    /// </summary>
    public class StubAppUser : IAppUser
    {
        /// <summary> constructor
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <param name="firstName">users first name</param>
        /// <param name="lastName">users last name</param>
        /// <param name="email">email used to uniquely identify the user</param>
        /// <param name="roles">the application specific roles supported by the user</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "userId")]
        public StubAppUser(Guid userId, string firstName, string lastName, string email, IEnumerable<string> roles)
        {
            UserId = UserId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Roles = new System.Collections.ObjectModel.ReadOnlyCollection<string>(roles.ToList());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUser"/> class. This is a copy constructor
        /// </summary>
        /// <param name="copy">A role to copy.</param>
        public StubAppUser(StubAppUser copy)
        {
            if (copy == null)
            {
                throw new ArgumentNullException("copy");
            }
            UserId = copy.UserId;
            FirstName = copy.FirstName;
            LastName = copy.LastName;
            Email = copy.Email;
            Roles = new System.Collections.ObjectModel.ReadOnlyCollection<string>(copy.Roles.ToList());
        }

        /// <summary> Initializes a new instance of the <see cref="AppUser"/> class.
        /// </summary>
        protected StubAppUser()
        {
            // unused
        }

        /// <summary> user identifier </summary>
        public Guid UserId { get; protected set;  }
        
        /// <summary> user first name </summary>
        public string FirstName { get; protected set;  }
        
        /// <summary> user last name </summary>
        public string LastName { get; protected set; }

        /// <summary> user's main email identifier </summary>
        public string Email { get; protected set;  }

        /// <summary> application roles available to the user </summary>
        public IEnumerable<string> Roles { get; protected set; }

        /// <summary> application roles available to the user </summary>
        public IEnumerable<string> Privileges { get; protected set; }

        /// <summary>Gets the claims for the current user.</summary>
        public IEnumerable<Claim> Claims
        {
            get { return Enumerable.Empty<Claim>(); }
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
