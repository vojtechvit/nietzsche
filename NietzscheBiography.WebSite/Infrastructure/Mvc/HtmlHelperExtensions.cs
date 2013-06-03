namespace NietzscheBiography.WebSite.Infrastructure.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public static class HtmlHelperExtensions
    {
        public static string ArticleFor(this HtmlHelper helper, string noun, bool definite = false, bool plural = false)
        {
            var vovels = new string[] { "a", "e", "i", "o", "u" };

            if (!definite)
            {
                if (plural)
                {
                    return "";
                }
                else if (vovels.Contains(noun[0].ToString(), StringComparer.CurrentCultureIgnoreCase))
                {
                    return "an";
                }
                else
                {
                    return "a";
                }
            }
            else
            {
                return "the";
            }
        }

        public static HtmlString TextEnum(
            this HtmlHelper helper,
            IEnumerable<object> values,
            ListConjunction conjunction = ListConjunction.And,
            bool serialComma = false)
        {
            return new HtmlString(Util.TextEnum(values, conjunction, serialComma));
        }

        public static HtmlString Link(
            this HtmlHelper helper, 
            string url, 
            string name, 
            string title = null)
        {
            const string template = "<a href=\"{0}\" title=\"{2}\">{1}</a>";

            return new HtmlString(string.Format(template, url, name, title ?? name));
        }

        public static HtmlString LinkForConnection(
            this HtmlHelper helper,
            long id,
            string name,
            string title = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            return helper.Link(urlHelper.Connection(id), name, title ?? "More on " + name);
        }

        public static HtmlString LinkForEvent(
            this HtmlHelper helper,
            long id,
            string name,
            string title = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            return helper.Link(urlHelper.Event(id), name, title ?? "More on " + name);
        }

        public static HtmlString LinkForMediaItem(
            this HtmlHelper helper,
            long id,
            string name,
            string title = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            return helper.Link(urlHelper.MediaItem(id), name, title ?? "More on " + name);
        }

        public static HtmlString LinkForPlace(
            this HtmlHelper helper,
            long id,
            string name,
            string title = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            return helper.Link(urlHelper.Place(id), name, title ?? "More on " + name);
        }
    }
}