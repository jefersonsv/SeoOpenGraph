using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SeoOpenGraph.ObjectTypes
{
    public class Audio : IObjectType
    {
        [Description("secure_url")]
        public string Secure_Url { get; set; }
        public string Type { get; set; }
    }
}