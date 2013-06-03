namespace NietzscheBiography.WebSite.Controllers
{
    using AttributeRouting.Web.Mvc;
    using NietzscheBiography.Domain.Models;
    using NietzscheBiography.WebSite.ViewModels;
    using NietzscheBiography.WebSite.ViewModels.Events;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    public class EventsController : BootstrapBaseController
    {
        private IQueryable<Participant> participantRepository;
        private IQueryable<Event> eventRepository;
        private IQueryable<EventType> eventTypeRepository;

        public EventsController(
            IQueryable<Participant> participantRepository,
            IQueryable<Event> eventRepository,
            IQueryable<EventType> eventTypeRepository)
        {
            this.participantRepository = participantRepository;
            this.eventRepository = eventRepository;
            this.eventTypeRepository = eventTypeRepository;
        }

        [HttpGet]
        [OutputCache(CacheProfile = "LongCache")]
        public async Task<ActionResult> Timeline(long? participantId = null)
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

            var eventTypes = await (from et in this.eventTypeRepository
                                    where et.Events.Any(e =>
                                        e.Occurrence.Start.Date != null
                                        && e.ParticipantInvolvements.Any(pi =>
                                            pi.ParticipantId == participantId))
                                    orderby et.Label
                                    select new EventTypeInfo
                                    {
                                        Id = et.Id,
                                        Name = et.Label
                                    })
                                    .ToListAsync();

            var filterDateLimits = await (from e in this.eventRepository
                                          where e.Occurrence.Start.Date != null
                                              && e.ParticipantInvolvements.Any(pi =>
                                                  pi.ParticipantId == participantId)
                                          group e by true into events
                                          select new
                                          {
                                              MinYear = events.Min(e => e.Occurrence.Start.Date),
                                              MaxYear = events.Max(e => e.Occurrence.Start.Date)
                                          })
                                          .FirstOrDefaultAsync();

            return View(new Timeline
                {
                    Participant = participant,
                    EventTypes = eventTypes,
                    FilterMinYear = filterDateLimits != null 
                                  ? (int?)filterDateLimits.MinYear.Value.Year
                                  : null,
                    FilterMaxYear = filterDateLimits != null 
                                  ? (int?)filterDateLimits.MaxYear.Value.Year
                                  : null
                });
        }

        [GET("timeline/data", RouteName="TimelineData")]
        [OutputCache(CacheProfile = "LongCache")]
        public async Task<ActionResult> TimelineData(
            long? participantId = null, 
            long? locationId = null,
            long? typeId = null, 
            long? importance = null,
            int? startYear = null,
            int? endYear = null,
            string format = "json")
        {
            if (!participantId.HasValue)
            {
                participantId = NietzscheConstants.NietzscheId;
            }

            var startDate = startYear != null ? (DateTime?)new DateTime(startYear.Value, 1, 1) : null;
            var endDate = endYear != null ? (DateTime?)new DateTime(endYear.Value, 1, 1) : null;

            var events = from e in this.eventRepository
                         where e.Occurrence.Start.Date != null
                            && e.ParticipantInvolvements.Any(pi => pi.ParticipantId == participantId)
                            && (locationId.HasValue ? e.LocationInvolvements.Any(l => l.LocationId == locationId) : true)
                            && (typeId.HasValue     ? e.TypeId == typeId.Value : true)
                            && (importance.HasValue ? e.Importance >= importance.Value : true)
                            && (startDate.HasValue  ? e.Occurrence.Start.Date >= startDate : true)
                            && (endDate.HasValue    ? e.Occurrence.Start.Date <= endDate : true)
                         orderby e.Occurrence.Start.Date, e.Occurrence.End.Date ascending
                         select e;

            switch (format)
            {
                case "html":

                    var events3 = await (from e in events
                                         select new EventInfo
                                         {
                                             Occurrence = e.Occurrence,
                                             Title = e.Title.Title
                                         })
                                        .ToListAsync();

                    return PartialView("_EventList", events3);

                default:

                    var events4 = await (from e in events
                                         select new EventDto
                                         {
                                             OccurrenceObject = e.Occurrence,
                                             Title = e.Title.Title,
                                             Citations = from c in e.Citations
                                                         orderby c.Source.OriginalTitle
                                                         select new CitationDto
                                                         {
                                                             Title = c.Title ?? "",
                                                             Text = c.Text ?? "",
                                                             SourceTitle = c.Source.OriginalTitle ?? c.Source.Title ?? ""
                                                         }
                                         })
                                        .ToListAsync();

                    foreach (var e in events4)
                    {
                        e.Occurrence = e.OccurrenceObject.ToString();
                        e.StartDate = e.OccurrenceObject.Start.Date.Value.ToString("yyyy-MM-dd");

                        foreach (var c in e.Citations)
                        {
                            c.Title = HttpUtility.HtmlEncode(c.Title);
                            c.Text = HttpUtility.HtmlEncode(c.Text);
                            c.SourceTitle = HttpUtility.HtmlEncode(c.SourceTitle);
                        }
                    }

                    return Json(events4, JsonRequestBehavior.AllowGet);
            }
        }

        [GET(NietzscheConstants.EventDetailUrl, RouteName = "EventDetail")]
        [OutputCache(CacheProfile = "LongCache")]
        public async Task<ActionResult> Detail(long id)
        {
            var bioEvent = await (from ev in eventRepository
                                  where ev.Id == id
                                  select new Detail
                                  {
                                      Event = ev
                                  })
                                  .FirstOrDefaultAsync();

            if (bioEvent == null)
            {
                return HttpNotFound();
            }

            return View(bioEvent);
        }

        private class EventDto
        {
            [ScriptIgnore]
            public EventOccurrence OccurrenceObject;
            public string Occurrence;
            public string StartDate;
            public string Title;
            public IEnumerable<CitationDto> Citations;
        }

        private class CitationDto
        {
            public string Title;
            public string Text;
            public string SourceTitle;
        }
    }
}