using SeoOpenGraph.ObjectTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using AngleSharp.Html.Parser;
using AngleSharp.Html.Dom;
using Humanizer;

namespace SeoOpenGraph
{
    public class Builder
    {
        string GetPrefix(Namespaces ns)
        {
            return DescriptionAttr(ns);
        }

        static NamespaceAttribute NamespaceAttr<T>(T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            var attributes = (NamespaceAttribute[])fi.GetCustomAttributes(
                typeof(NamespaceAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0];
            else return null;
        }

        string DescriptionAttr<T>(T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        //static int SumUsed(PropertyInfo prop, object obj)
        //{
        //    //var total = 0;
        //    //if (prop.PropertyType.FullName.StartsWith("System.Collections.Generic.List`"))
        //    //{
        //    //    var list = prop.GetValue(obj) as IList;
        //    //    foreach (var it in list)
        //    //        foreach (var pr in it.GetType().GetProperties())
        //    //            total += SumUsed(pr, it);
        //    //}
        //    //else
        //    //{
        //    //    total += prop.GetValue(obj) != null ? 1 : 0;
        //    //}

        //    //return total;
        //    return prop.GetValue(obj) != null ? 1 : 0;
        //}

        static int CountUsed(object obj)
        {
            var props = obj.GetType().GetProperties();
            return props.Count(w => w.GetValue(obj) != null);
        }



        public static string GeneratePrefix(params IObjectType[] objectTypes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in objectTypes)
            {
                var att = item.GetType().GetCustomAttribute<NamespaceAttribute>(false);
                if (att != null)
                {
                    sb.Append($"{att.Initials}:{att.NamespaceUrl} ");
                }
            }

            return sb.ToString().Trim();
        }

        static string Generator(object obj, string previousPrefix, bool removeSelfName)
        {
            CultureInfo ci = new CultureInfo("en-Us");
            var prefix = $"{(string.IsNullOrEmpty(previousPrefix) ? string.Empty : $"{previousPrefix}:")}{obj.GetType().Name.Humanize().Replace(' ', ':').ToLower().Trim()}";
            var parser = new HtmlParser();
            var document = parser.ParseDocument(string.Empty);

            StringBuilder sb = new StringBuilder();
            //sb.AppendLine(parent);

            var types = new Type[] {
                typeof(string),
                typeof(int?),
                typeof(uint?),
                typeof(Uri),
                typeof(bool?),
                typeof(System.Net.Mime.ContentType),
                typeof(Availability),
                typeof(Condition),
                typeof(Determiner?),
            };

            var numbers = new Type[] {
                typeof(double?),
            };

            var ogpObjs = new Type[] {
                typeof(List<Image>),
                typeof(List<Video>),
            };

            var audioObjs = new Type[] {
                typeof(List<Audio>),
                typeof(List<Currency>),
                typeof(List<string>)
            };

            var properties = obj.GetType().GetProperties().OrderBy(o => o.Name);
            foreach (PropertyInfo item in properties)
            {
                if (types.Contains(item.PropertyType))
                {
                    var value = item.GetValue(obj);
                    if (value != null)
                    {
                        string text = GetDescription(item) ?? item.Name.ToLower(); //item.Name.Humanize().Replace(' ', ':').ToLower().Trim();
                        text = ":" + text;

                        if (removeSelfName)
                        {
                            text = string.Empty;
                        }

                        sb.AppendLine(HtmlElement(document, $"{prefix}{text}", value.ToString()));
                    }
                }
                else if (numbers.Contains(item.PropertyType))
                {
                    double? value = Convert.ToDouble(item.GetValue(obj));
                    if (value.HasValue)
                    {
                        string text = GetDescription(item) ?? item.Name.ToLower(); //item.Name.Humanize().Replace(' ', ':').ToLower().Trim();
                        text = ":" + text;

                        if (removeSelfName)
                        {
                            text = string.Empty;
                        }

                        sb.AppendLine(HtmlElement(document, $"{prefix}{text}", string.Format(ci, "{0:0.00}", value.Value)));
                    }
                }
                else if (ogpObjs.Contains(item.PropertyType))
                {
                    var list = item.GetValue(obj) as IList;

                    if (list.Count == 1)
                    {
                        var used = CountUsed(list[0]); // count used
                        sb.Append(Generator(list[0], prefix, used == 1));
                    }
                    else
                    {
                        foreach (var it in list)
                            sb.Append(Generator(it, prefix, false));
                    }
                }
                else if (audioObjs.Contains(item.PropertyType))
                {
                    var list = item.GetValue(obj) as IList;
                    foreach (var it in list)
                        sb.Append(Generator(it, prefix, false));
                }
                else
                {
                    throw new NotImplementedException($"PropertyType: {item.PropertyType}");
                }
            }

            return sb.ToString();
        }

        public static string GenerateTags(params IObjectType[] objectTypes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in objectTypes)
            {
                sb.Append(Generator(item, null, false));
            }

            return sb.ToString();
        }

        public static (string, string) Generate(params IObjectType[] objectTypes)
        {
            return (GeneratePrefix(objectTypes), GenerateTags(objectTypes));
        }

        static string GetDescription(PropertyInfo item)
        {
            var attributes = item.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attributes.Length > 0)
            {
                var description = (DescriptionAttribute)attributes[0];
                var text = description.Description;
                return text;
            }

            return null;
        }

        static string HtmlElement(IHtmlDocument document, string property, string content)
        {
            var el = document.CreateElement("meta");
            el.SetAttribute("property", property);
            el.SetAttribute("content", content);

            return el.OuterHtml;
        }

        public static string GeneratePrefixAttribute(params IObjectType[] objectTypes)
        {
            var prefix = GeneratePrefix(objectTypes);

            if (!string.IsNullOrEmpty(prefix))
                return $@"prefix=""{prefix}""";

            return null;
        }
    }
}
