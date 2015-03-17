using Republic.Core.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhml.Mms.Security
{
    /// <summary> Stubbed security for local testing and development.
    /// </summary>
    public class StubAppSecurity : IAppSecurity
    {
        //Local users is a store of users inteded for testing and development
        //Remove and replace with an actual user store for production
        //For testing purposes, the first user is always "logged on"
        private List<StubAppUser> localUsers = new List<StubAppUser>()
        {
            new StubAppUser(new Guid("17519C5C-4864-4954-8BE8-E05A6B9119DB"), 
                "Joe", "Smith", "Joe.Smith@stub.gov", new string[] { "Admin", "User"}),
            new StubAppUser(new Guid("27FC5E5D-175F-4DBA-9A98-8E7D6E581F6F"), 
                "Sally", "Jones", "Sally.Jones@stub.gov", new string[] {"User"}),
            new StubAppUser(new Guid("65961BE5-C26B-4B03-891F-ECC4DD532D10"), 
                "Garry", "Johnson", "Garry.Johnson@stub.gov", new string[] {"Admin"}),
        };


        /// <summary>
        /// Indicates if the current context is authenticated
        /// </summary>
        /// <value><c>true</c> if this instance is authenticated; otherwise, <c>false</c>.</value>
        public bool IsAuthenticated { get { return true; } }

        /// <summary>
        /// Returns the currently authenticated user if applicable.
        /// </summary>
        /// <returns>IAppUser.</returns>
        public IAppUser GetCurrentUser()
        {
            return localUsers.First();
        }

    }
}
