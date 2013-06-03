using AttributeRouting;
using AttributeRouting.Web.Mvc;
using NietzscheBiography.Domain.Models;
using NietzscheBiography.WebSite.Infrastructure.Dal;
using NietzscheBiography.WebSite.ViewModels;
using NietzscheBiography.WebSite.ViewModels.Connections;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;

namespace NietzscheBiography.WebSite.Controllers
{
    public class ConnectionsController : BootstrapBaseController
    {
        private IQueryable<Participant> participantRepository;
        private IPluralizationService pluralizationService;

        public ConnectionsController(
            IQueryable<Participant> participantRepository,
            IPluralizationService pluralizationService)
        {
            this.participantRepository = participantRepository;
            this.pluralizationService = pluralizationService;
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

            var individuals = from i in this.participantRepository.OfType<Individual>()
                              where i.Id != participantId
                              && (i.EventInvolvements.Any(pi =>
                                    pi.Event.ParticipantInvolvements.Any(pi2 =>
                                        pi2.ParticipantId == participantId))
                                    || i.DeterminantRelationships.Any(dr =>
                                        dr.ImmanentId == participantId)
                                    || i.ImmanentRelationships.Any(ir =>
                                        ir.DeterminantId == participantId))
                              select new IndividualInfo
                              {
                                  Id = i.Id,
                                  Name = i.Name,
                                  FullName = i.FullName,
                                  Profession = i.Profession,
                                  Nationality = i.Nationality.Nationality.Singular,

                                  // Nietzsche -> sister -> his sister
                                  // immanent -> sister -> determinant
                                  Relationships = from r in i.DeterminantRelationships.Union(i.ImmanentRelationships)
                                                  where r.ImmanentId == participantId || r.DeterminantId == participantId
                                                  select r.ImmanentId == participantId ? r.Type.Label : r.InverseType.Label,
                              };

            var individuals2 = await individuals.ToListAsync();

            var relatives = from i in individuals2
                            where i.Relationships.Count() > 0
                            orderby i.FullName.LastName ?? i.FullName.GivenNames ?? i.Name
                            from r in i.Relationships
                            group i by this.pluralizationService.Pluralize(r) into people
                            orderby people.Key
                            select people;

            var others = from i in individuals2
                         where i.Relationships.Count() == 0
                         orderby i.FullName.LastName ?? i.FullName.GivenNames ?? i.Name
                         select i;

            var organizations = from o in this.participantRepository.OfType<Organization>()
                                where o.Id != participantId
                                && (o.EventInvolvements.Any(pi =>
                                    pi.Event.ParticipantInvolvements.Any(pi2 =>
                                        pi2.ParticipantId == participantId))
                                    || o.DeterminantRelationships.Any(dr =>
                                        dr.ImmanentId == participantId)
                                    || o.ImmanentRelationships.Any(ir =>
                                        ir.DeterminantId == participantId))
                                orderby o.Name
                                let l = o.Location
                                let c = l as Country
                                let pp = l as PopulatedPlace
                                let a = l as Address
                                select new OrganizationInfo
                                {
                                    Id = o.Id,
                                    Name = o.Name
                                };

            var viewModel = new Index
            {
                Participant = participant,
                Relatives = relatives.ToDictionary(
                    g => g.Key,
                    g => (IEnumerable<IndividualInfo>)g),
                Others = others.ToList(),
                Organizations = await organizations.ToListAsync()
            };

            return View(viewModel);
        }

        [GET(NietzscheConstants.ConnectionDetailUrl, RouteName = "ConnectionDetail")]
        [OutputCache(CacheProfile = "LongCache")]
        public async Task<ActionResult> Detail(long? participantId)
        {
            if (!participantId.HasValue)
            {
                participantId = NietzscheConstants.NietzscheId;
            }

            var viewModel = await (from p in this.participantRepository
                                   where p.Id == participantId
                                   let i = p as Individual
                                   let o = p as Organization
                                   select new Detail
                                   {
                                       Participant = p,

                                       AlternativeNames = from an in p.AlternativeNames select an.Name,

                                       Nationality = i != null ? i.Nationality.Nationality.Singular : null,

                                       Location = null,

                                       Images = from pi in p.EventInvolvements
                                                let e = pi.Event
                                                where pi.ThematicRoleId == NietzscheConstants.BeneficiaryThematicRoleId
                                                   && e.TypeId == NietzscheConstants.PhotographEventTypeId
                                                from mi in e.RelatedMediaItems
                                                let displayedTitle = mi.Title ?? mi.OriginalTitle ?? null
                                                orderby displayedTitle
                                                select new ImageInfo
                                                {
                                                    Title = displayedTitle,
                                                    Url = mi.Url
                                                }
                                   })
                                   .FirstOrDefaultAsync();

            if (viewModel.Participant == null)
            {
                return HttpNotFound();
            }

            string viewName = viewModel.Participant is Organization
                            ? "OrganizationDetail"
                            : "IndividualDetail";
          
            return View(viewName, viewModel);
        }

        /* Unused
            Intervals = from iv in r.Intervals
                        select new IntervalInfo
                        {
                            Description = iv.Description,

                            ConcludingEvent = new EventInfo
                            {
                                Id = iv.ConcludingEvent.Id,
                                Occurence = iv.ConcludingEvent.Occurence
                            },

                            InitiatingEvent = new EventInfo
                            {
                                Id = iv.InitiatingEvent.Id,
                                Occurence = iv.InitiatingEvent.Occurence
                            },

                            Citations = from c in iv.Citations

                                        let displayedTitle = c.Source.Title ?? c.Source.OriginalTitle ?? c.Source.Type.Label

                                        let authors = (from e in c.Source.RelatedEvents
                                                        where e.TypeId == NietzscheConstants.WritingEventTypeId
                                                        from pi in e.ParticipantInvolvements
                                                        where pi.ThematicRoleId == NietzscheConstants.AgentThematicRoleId
                                                        group pi.Participant by pi.Participant.Id into ps
                                                        select ps.FirstOrDefault())

                                        select new CitationInfo
                                        {
                                            Title = c.Title,
                                            Text = c.Text,
                                            Source = new MediaItemInfo
                                            {
                                                Id = c.Source.Id,
                                                Title = displayedTitle,
                                                Authors = authors
                                            }
                                        }
                        }*/
    }
}