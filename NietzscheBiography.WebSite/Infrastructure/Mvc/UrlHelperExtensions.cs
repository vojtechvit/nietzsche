using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NietzscheBiography.WebSite.Infrastructure.Mvc
{
    public static class UrlHelperExtensions
    {
        public static string Connection(this UrlHelper helper, long id)
        {
            return helper.RouteUrl("ConnectionDetail", new { participantId = id });
        }

        public static string Connections(this UrlHelper helper, long participantId)
        {
            return helper.RouteUrl("Connections", new { participantId = participantId });
        }

        public static string MediaItem(this UrlHelper helper, long id)
        {
            return helper.RouteUrl("MediaItemDetail", new { id = id });
        }

        public static string Work(this UrlHelper helper, long participantId)
        {
            return helper.RouteUrl("Work", new { participantId = participantId });
        }

        public static string Event(this UrlHelper helper, long id)
        {
            return helper.RouteUrl("EventDetail", new { id = id });
        }

        public static string Timeline(this UrlHelper helper, long participantId)
        {
            return helper.RouteUrl("Timeline", new { participantId = participantId });
        }

        public static string Place(this UrlHelper helper, long id)
        {
            return helper.RouteUrl("PlaceDetail", new { id = id });
        }

        public static string Places(this UrlHelper helper, long participantId)
        {
            return helper.RouteUrl("Places", new { participantId = participantId });
        }
    }
}