using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Rhml.Mms.Web.Extensions
{
    public static class ModelStateExtensionMethods
    {
        /// <summary> Retrieves only the ModelState binding errors (not all model state values).
        /// This is more convenient to quickly find form binding problems in tracing code.  This
        /// can also be attached to logging, debugging, or tracing systems.
        /// </summary>
        /// <param name="mstate">The current model state (controller ModelState context)</param>
        /// <returns>A dictionary of model state errors (empty if none are found)</returns>
        public static Dictionary<string, string> GetErrors(this ModelStateDictionary mstate)
        {
            if (mstate == null)
            {
                throw new ArgumentNullException("mstate");
            }
            var badkeys = new Dictionary<string, string>();
            foreach (var key in mstate.Keys)
            {
                var errors = mstate[key].Errors;
                if (errors.Count > 0)
                {
                    string errorMessage = string.Empty;
                    foreach (var e in errors)
                    {
                        errorMessage += "error: " + e.ErrorMessage + "exception: " + e.Exception.Message;
                    }
                    badkeys.Add(key, errorMessage);
                }
            }
            return badkeys;
        }

        /// <summary> Displays model state binding errors only in debug mode.
        /// Note: you can see these errors in the output window when running the debugger in VS
        /// </summary>
        /// <param name="mstate">The current model state (controller ModelState context)</param>
        public static void DebugDisplayErrors(this ModelStateDictionary mstate)
        {
            #if DEBUG
            var errors = GetErrors(mstate);
            foreach (var error in errors)
            {
                System.Diagnostics.Debug.WriteLine("ModelStateError[" + error.Key + "]: " + error.Value);
            }
            #endif
        }
    }
}




