using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SeoOpenGraph.ObjectTypes
{
    public class Image : IObjectType
    {
        public Image(Uri url)
        {
            this.Url = url;
        }

        public Uri Url { get; set; }
        public Uri Secure_Url { get; set; }
        public System.Net.Mime.ContentType Type { get; set; }
        public uint? Width { get; set; }
        public uint? Height { get; set; }
        public string Alt { get; set; }

        /// <summary>
        /// <see cref="https://developers.facebook.com/docs/reference/opengraph/object-type/website/"/>
        /// </summary>
        public bool? User_Generated { get; set; }
    }
}