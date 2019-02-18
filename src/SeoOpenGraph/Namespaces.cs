using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SeoOpenGraph
{
    public enum Namespaces
    {
        [Description("http://ogp.me/ns#")]
        Og,

        [Description("http://ogp.me/ns/music#")]
        Music,

        [Description("http://ogp.me/ns/video#")]
        Video,

        [Description("http://ogp.me/ns/article#")]
        Article,

        [Description("http://ogp.me/ns/book#")]
        Book,

        [Description("http://ogp.me/ns/profile#")]
        Profile,

        [Description("http://ogp.me/ns/website#")]
        Website,

        [Description("http://ogp.me/ns/product#")]
        Product
    }
}
