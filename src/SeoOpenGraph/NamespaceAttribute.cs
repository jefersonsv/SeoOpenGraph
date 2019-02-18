using System;
using System.Collections.Generic;
using System.Text;

namespace SeoOpenGraph
{
    public class NamespaceAttribute : Attribute
    {
        public NamespaceAttribute(string initials, string namespaceUrl)
        {
            this.Initials = initials;
            this.NamespaceUrl = namespaceUrl;
        }

        public string Initials { get; set; }

        public string NamespaceUrl { get; set; }
    }
}
