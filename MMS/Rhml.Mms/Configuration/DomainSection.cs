using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhml.Mms.Configuration
{
    public class DomainSection : ConfigurationSection
    {
        [ConfigurationProperty("domains")]
        public DomainElementCollection Domains
        {
            get { return ((DomainElementCollection)(base["domains"])); }
        }
    }
    public class DomainElement : ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }

        }

        [ConfigurationProperty("username")]
        public string UserName
        {
            get
            {
                return (string)this["username"];
            }
            set
            {
                this["username"] = value;
            }

        }


        [ConfigurationProperty("password")]
        public string Password
        {
            get
            {
                return (string)this["password"];
            }
            set
            {
                this["password"] = value;
            }

        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface", Justification = "Implemtation is specific to configurations sections"), ConfigurationCollection(typeof(DomainElement))]
    public class DomainElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "domain";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            if (string.IsNullOrWhiteSpace(elementName)) throw new ArgumentException("Element name is required: ", elementName);
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DomainElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DomainElement)(element)).Name;
        }

        public DomainElement this[int idx]
        {
            get { return (DomainElement)BaseGet(idx); }
        }

    }
}
