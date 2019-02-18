using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace SeoOpenGraph.ObjectTypes
{
    /// <summary>
    /// The Open Graph protocol enables any web page to become a rich object in a social graph
    /// <see cref="http://ogp.me"/>
    /// </summary>
    [Namespace("og", "http://ogp.me/ns#")]
    public class Og : IObjectType
    {
        /// <summary>
        /// The title of your object as it should appear within the graph, e.g., "The Rock".
        /// </summary>
        public string Title { get; set; }
        public string Type { get; set; }
        public Uri Url { get; set; }
        public string Description { get; set; }
        public Determiner? Determiner { get; set; }
        public string Locale { get; set; }
        public List<string> LocaleAlternate { get; set; }
        public string SiteName { get; set; }
        public List<Image> Image { get; set; }
        public List<Video> Video { get; set; }
        public List<Audio> Audio { get; set; }
        public Og(string title, string type, Uri image, Uri url)
        {
            this.Title = title;
            this.Type = type;
            this.Url = url;

            this.LocaleAlternate = new List<string>();
            this.Image = new List<Image>();
            this.Image.Add(new Image(image));
            this.Video = new List<Video>();
            this.Audio = new List<Audio>();
        }
    }
}
