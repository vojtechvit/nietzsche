namespace NietzscheBiography.WebSite.Controllers
{
    using AttributeRouting.Web.Mvc;
using NietzscheBiography.Domain.Models;
using NietzscheBiography.WebSite.ViewModels;
using NietzscheBiography.WebSite.ViewModels.Places;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

    public class PlacesController : BootstrapBaseController
    {
        private IQueryable<Participant> participantRepository;
        private IQueryable<Location> locationRepository;

        public PlacesController(
            IQueryable<Participant> participantRepository,
            IQueryable<Location> locationRepository)
        {
            this.participantRepository = participantRepository;
            this.locationRepository = locationRepository;
        }

        [HttpGet]
        [OutputCache(CacheProfile = "LongCache")]
        public async Task<ActionResult> Index(long? participantId = null)
        {
            if (!participantId.HasValue)
            {
                participantId = NietzscheConstants.NietzscheId;
            }

            var participant = await (from p in this.participantRepository
                                     where p.Id == participantId
                                     select p)
                                    .FirstOrDefaultAsync();

            if (participant == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = new Index
            {
                Participant = participant
            };

            return View(viewModel);
        }

        [GET("places/data", RouteName = "PlacesData")]
        [OutputCache(CacheProfile = "LongCache")]
        public async Task<ActionResult> PlacesData(
            long? participantId = null,
            long? eventTypeId = null)
        {
            if (!participantId.HasValue)
            {
                participantId = NietzscheConstants.NietzscheId;
            }

            var locations = from l in this.locationRepository
                            where l.EventInvolvements.Any(li =>
                                li.Event.ParticipantInvolvements.Any(pi =>
                                    pi.ParticipantId == participantId)
                                && (eventTypeId.HasValue ? li.Event.TypeId == eventTypeId.Value : true))
                            let c = l as Country
                            let pp = l as PopulatedPlace
                            let a = l as Address
                            select new LocationDto
                            {
                                location_id = l.Id,
                               
                                title = c != null ? c.Name
                                      : pp != null ? pp.Name 
                                          + (pp.Country != null ? ", " + pp.Country.Name : "")
                                      : a != null ? a.StreetName
                                          + (a.BuildingNumber != null ? " " + a.BuildingNumber + ", " : ", ")
                                          + a.PopulatedPlace.Name
                                          + (a.PopulatedPlace.Country != null ? ", " + a.PopulatedPlace.Country.Name : "")
                                      : "",
                               
                                latitude = l.GeoLocation.Latitude,
                               
                                longitude = l.GeoLocation.Longitude,
                               
                                events = from li in l.EventInvolvements
                                         let e = li.Event
                                         where e.ParticipantInvolvements.Any(pi => pi.ParticipantId == participantId)
                                             && (eventTypeId.HasValue ? e.TypeId == eventTypeId.Value : true)
                                         //group e by e.Id into events
                                         //let e = events.First()
                                         select new EventDto
                                         {
                                             event_id = e.Id,
                                             event_type = e.Type.Label,
                                             event_type_id = e.Type.Id
                                         }
                            };

            var locations2 = await locations.ToListAsync();

            var eventTypeOccurences = from l in locations2
                                      from e in l.events
                                      group e by e.event_type_id into typeGroup
                                      let occurenceCount = typeGroup.Count()
                                      orderby occurenceCount descending
                                      select new
                                      {
                                          Label = typeGroup.First().event_type,
                                          Count = occurenceCount
                                      };

            foreach (var l in locations2)
            {
                l.title = HttpUtility.HtmlEncode(l.title);
            }
            
            var data = new
            {
                locations = locations2,
                event_type_occurrences = eventTypeOccurences.ToDictionary(g => g.Label, g => g.Count)
            };

            return this.Json(data, JsonRequestBehavior.AllowGet);
        }

        [GET(NietzscheConstants.LocationDetailUrl, RouteName = "PlaceDetail")]
        [OutputCache(CacheProfile = "LongCache")]
        public async Task<ActionResult> Detail(long id)
        {
            var location = await (from l in this.locationRepository
                                  let c = l as Country
                                  let pp = l as PopulatedPlace
                                  let a = l as Address
                                  where l.Id == id
                                  select new
                                  {
                                      Address = a != null
                                              ? a
                                              : null,
                                      PopulatedPlace = a != null
                                                     ? a.PopulatedPlace
                                                     : pp != null
                                                     ? pp
                                                     : null,
                                      Country = a != null
                                              ? a.PopulatedPlace.Country
                                              : pp != null
                                              ? pp.Country
                                              : c
                                  })
                                  .FirstOrDefaultAsync();

            if (location == null)
            {
                return HttpNotFound();
            }

            Detail viewModel;
            string viewName;

            if (location.Address != null)
            {
                location.Address.PopulatedPlace = location.PopulatedPlace;
                location.Address.PopulatedPlace.Country = location.Country;
                viewName = "AddressDetail";

                viewModel = new AddressDetail
                {
                    Address = location.Address
                };
            }
            else if (location.PopulatedPlace != null)
            {
                location.PopulatedPlace.Country = location.Country;
                viewName = "PopulatedPlaceDetail";

                viewModel = new PopulatedPlaceDetail
                {
                    PopulatedPlace = location.PopulatedPlace
                };
            }
            else if (location.Country != null)
            {
                viewName = "CountryDetail";

                viewModel = new CountryDetail
                {
                    Country = location.Country
                };
            }
            else
            {
                return this.HttpNotFound();
            }

            //// Assign remaining viewmodel properties

            return View(viewName, viewModel);
        }

        private class LocationDto
        {
            public long location_id;
                               
            public string title;

            public double? latitude;
            public double? longitude;
            public IEnumerable<EventDto> events;
        }

        private class EventDto
        {
            public long event_id;
            public string event_type;
            public long event_type_id;
        }
    }
}