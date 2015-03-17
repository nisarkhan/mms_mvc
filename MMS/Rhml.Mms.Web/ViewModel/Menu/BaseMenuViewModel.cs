using Republic.Core.Interfaces;
using Republic.Core.Security.Interfaces;
using Republic.Core.Web.Mvc.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace Rhml.Mms.Web.ViewModel.Menu
{
    /// <summary> Menu view model base class to assist with the creation
    /// of menu objects that can be used to encapsulate program navigation
    /// along with application security.  This class can also be used
    /// to create partial views in pages that support next page and
    /// previous page navigation.
    /// </summary>
    public abstract class BaseMenuViewModel
    {
        /// <summary>
        /// Returns a generated menu item based on the expression provided
        /// </summary>
        /// <typeparam name="TController">The Controller</typeparam>
        /// <param name="expression">Expression which points to the action method</param>
        /// <param name="displayName">A name for the menu item</param>
        /// <param name="role">Roles the current user must be a member of for this item to be displayed</param>
        /// <param name="authorize">Only show this menu item if the user is authenticated</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), 
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "area"), 
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed"), 
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), 
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]    // class is injected
        protected MenuItem GetMenuItem<TController>(Expression<Action<TController>> expression, 
            string displayName = null, 
            IEnumerable<string> role = null, 
            bool? authorize = null, 
            string area = "") where TController : System.Web.Mvc.Controller
        {
            if (expression == null) throw new ArgumentNullException("expression");
            if (expression.Body.NodeType != ExpressionType.Call) throw new InvalidOperationException("Menu item must be built from action method");

            var output = new MenuItem();
            output.DisplayName = string.Empty;

            output.Controller = typeof(TController).Name.Replace("Controller", string.Empty);

            var methodInfo = ((MethodCallExpression)expression.Body).Method;
            output.Action = methodInfo.Name;

            if (displayName == null)
            {
                var displayAttribute = methodInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
                if (displayAttribute != null)
                {
                    output.DisplayName = ((DisplayAttribute)displayAttribute).Name;
                }
                else
                {
                    output.DisplayName = output.Action;
                }
            }
            else
            {
                output.DisplayName = displayName;
            }

            if (authorize == null)
            {
                var authorizeAttribute = (IEnumerable<AppAuthorizeAttribute>)methodInfo.GetCustomAttributes(typeof(AppAuthorizeAttribute), false);
                if (authorizeAttribute != null &&
                    authorizeAttribute.Count() > 0)
                {
                    output.Authorize = true;
                    output.Roles = (role == null) ?
                        new List<string>(authorizeAttribute.Where(x => x.Roles != null).SelectMany(x => x.Roles)) :
                        output.Roles = new List<string>(role);
                }
                else
                {
                    output.Authorize = false;
                }
            }
            else
            {
                output.Authorize = authorize.Value;
            }

            return output;
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        protected IEnumerable<MenuItem> FilterMenu(IEnumerable<MenuItem> menu, IServiceLocator service)
        {
            if (menu == null)
            {
                throw new ArgumentNullException("menu");
            }
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
            var security = service.Locate<IAppSecurity>();
            var user = security.GetCurrentUser();
            var userRoles = (user != null) ? user.Roles : null;

            var output = menu.ToList();

            //When anonymous remove all items that require some authorization
            if (!security.IsAuthenticated)
            {
                output.RemoveAll(item => (item.Authorize));
            }

            if (userRoles != null)
            {
                output.RemoveAll(item => item.Roles != null && item.Roles.Count() > 0 &&
                                         !item.Roles.Intersect(userRoles, StringComparer.InvariantCultureIgnoreCase).Any());
            }

            return output;
        
        }
    }
}