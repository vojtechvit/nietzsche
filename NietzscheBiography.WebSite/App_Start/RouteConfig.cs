using AttributeRouting.Web.Mvc;
using NavigationRoutes;
using NietzscheBiography.Domain.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace NietzscheBiography.WebSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapAttributeRoutes();

            routes.MapNavigationRoute(
                "Info", "info/{participantId}",
                new { Controller = "Connections", Action = "Detail", ParticipantId = "" },
                new { ParticipantId = @"\d*" });
            
            routes.MapNavigationRoute(
                "Timeline", "timeline/{participantId}",
                new { Controller = "Events", Action = "Timeline", ParticipantId = "" },
                new { ParticipantId = @"\d*" });
            
            routes.MapNavigationRoute(
                "Connections", "connections/{participantId}",
                new { Controller = "Connections", Action = "Index", ParticipantId = "" },
                new { ParticipantId = @"\d*" });
            
            routes.MapNavigationRoute(
                "Work", "work/{participantId}",
                new { Controller = "MediaItems", Action = "Work", ParticipantId = "" },
                new { ParticipantId = @"\d*" });
            
            routes.MapNavigationRoute(
                "Places", "places/{participantId}",
                new { Controller = "Places", Action = "Index", ParticipantId = "" },
                new { ParticipantId = @"\d*" });
            
            routes.MapNavigationRoute(
                "Sources", "sources/{participantId}",
                new { Controller = "MediaItems", Action = "Sources", ParticipantId = "" },
                new { ParticipantId = @"\d*" });

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
    }
}