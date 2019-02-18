using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SeoOpenGraph.ObjectTypes
{
    public class Video : IObjectType
    {
        public Video(Uri url)
        {
            this.Url = url;
        }

        public Uri Url { get; set; }

        public Uri Secure_Url { get; set; }
        public System.Net.Mime.ContentType Type { get; set; }
        public uint? Width { get; set; }
        public uint? Height { get; set; }
    }
}