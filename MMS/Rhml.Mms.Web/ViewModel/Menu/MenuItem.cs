using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhml.Mms.Web.ViewModel.Menu
{
    /// <summary> Represents a menu item (menu, and navigation information) 
    /// for a simple page redirect within the application.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DisplayName} - ({Controller}/{Action})")]
    public class MenuItem
    {
        public string DisplayName { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool Authorize { get; set; }
        public string Area { get; set; }
    }
}