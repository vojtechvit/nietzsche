using AttributeRouting.Web.Mvc;
using NietzscheBiography.Domain.Models;
using NietzscheBiography.WebSite.Infrastructure.Dal;
using NietzscheBiography.WebSite.ViewModels;
using NietzscheBiography.WebSite.ViewModels.MediaItems;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NietzscheBiography.WebSite.Controllers
{
    public class MediaItemsController : BootstrapBaseController
    {
        private IQueryable<Participant> participantRepository;
        private IQueryable<MediaItem> mediaItemRepository;
        private IPluralizationService pluralizationService;

        public MediaItemsController(
            IQueryable<Participant> participantRepository, 
            IQueryable<MediaItem> mediaItemRepository,
            IPluralizationService pluralizationService)
        {
            this.participantRepository = participantRepository;
            this.mediaItemRepository = mediaItemRepository;
            this.pluralizationService = pluralizationService;
        }

        [HttpGet]
        [OutputCache(CacheProfile = "LongCache")]
        public async Task<ActionResult> Work(long? participantId = null)
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

            var mediaItems = from mi in this.mediaItemRepository
                             where mi.RelatedEvents.Any(e => 
                                 (e.TypeId == NietzscheConstants.WritingEventTypeId
                               || e.TypeId == NietzscheConstants.ComposingEventTypeId)
                              && e.ParticipantInvolvements.Any(pi =>
                                  pi.ParticipantId == participantId
                               && pi.ThematicRoleId == NietzscheConstants.AgentThematicRoleId))
                             
                             let displayedTitle = mi.Title ?? mi.OriginalTitle ?? mi.Type.Label

                             let authors = (from e in mi.RelatedEvents
                                            where (e.TypeId == NietzscheConstants.WritingEventTypeId
                                                || e.TypeId == NietzscheConstants.ComposingEventTypeId)
                                            from pi in e.ParticipantInvolvements
                                            where pi.ThematicRoleId == NietzscheConstants.AgentThematicRoleId
                                            group pi.Participant by pi.Participant.Id into ps
                                            select ps.FirstOrDefault())

                             let beneficiaries = (from e in mi.RelatedEvents
                                                  where mi.Title == null && mi.OriginalTitle == null
                                                     && (e.TypeId == NietzscheConstants.WritingEventTypeId
                                                      || e.TypeId == NietzscheConstants.ComposingEventTypeId)
                                                  from pi in e.ParticipantInvolvements
                                                  where pi.ThematicRoleId == NietzscheConstants.BeneficiaryThematicRoleId
                                                  group pi.Participant by pi.Participant.Id into ps
                                                  select ps.FirstOrDefault())

                             orderby displayedTitle
                             select new MediaItemInfo
                             {
                                 Id = mi.Id,
                                 TypeLabel = mi.Type.Label,
                                 Title = displayedTitle,
                                 Comment = mi.Comment,
                                 Isbn = mi.Isbn,
                                 DatePublished = (from e in mi.RelatedEvents
                                                  where e.TypeId == NietzscheConstants.PublishingEventTypeId
                                                  select e.Occurrence.Start)
                                                 .FirstOrDefault(),
                                 LocationPublished = (from e in mi.RelatedEvents
                                                      from li in e.LocationInvolvements
                                                      let l = li.Location
                                                      let pp = l as PopulatedPlace
                                                      let a = l as Address
                                                      select new LocationInfo
                                                      {
                                                          Id = l.Id,
                                                          Title = a != null
                                                                ? a.PopulatedPlace.Name
                                                                : pp != null
                                                                ? pp.Name
                                                                : ""
                                                      })
                                                     .FirstOrDefault(),
                                 Authors = authors,
                                 TitleIsType = mi.Title == null && mi.OriginalTitle == null,
                                 Beneficiaries = beneficiaries
                             };

            var mediaItems2 = from mi in await mediaItems.ToListAsync()
                              orderby mi.Title
                              group mi by this.pluralizationService.Pluralize(mi.TypeLabel) into items
                              orderby items.Key
                              select items;

            var viewModel = new Index
            {
                Participant = participant,
                MediaItems = mediaItems2.ToDictionary(
                    g => g.Key, 
                    g => (IEnumerable<MediaItemInfo>)g)
            };

            return View(viewModel);
        }

        [HttpGet]
        [OutputCache(CacheProfile = "LongCache")]
        public async Task<ActionResult> Sources(long? participantId = null)
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

            var mediaItems = from mi in this.mediaItemRepository
                             where mi.RelatedEvents.Any(e =>
                                 e.TypeId == NietzscheConstants.WritingEventTypeId
                              && e.ParticipantInvolvements.Any(pi =>
                                  pi.ParticipantId == participantId
                               && pi.ThematicRoleId != NietzscheConstants.AgentThematicRoleId))

                             let authors = (from e in mi.RelatedEvents
                                            where e.TypeId == NietzscheConstants.WritingEventTypeId
                                            from pi in e.ParticipantInvolvements
                                            where pi.ThematicRoleId == NietzscheConstants.AgentThematicRoleId
                                            group pi.Participant by pi.Participant.Id into ps
                                            select ps.FirstOrDefault())

                             let beneficiaries = (from e in mi.RelatedEvents
                                                  where mi.Title == null && mi.OriginalTitle == null
                                                     && e.TypeId == NietzscheConstants.WritingEventTypeId
                                                  from pi in e.ParticipantInvolvements
                                                  where pi.ThematicRoleId == NietzscheConstants.BeneficiaryThematicRoleId
                                                  group pi.Participant by pi.Participant.Id into ps
                                                  select ps.FirstOrDefault())

                             let displayedTitle = mi.Title ?? mi.OriginalTitle ?? mi.Type.Label

                             orderby displayedTitle
                             select new MediaItemInfo
                             {
                                 Id = mi.Id,
                                 Title = displayedTitle,
                                 TitleIsType = mi.Title == null && mi.OriginalTitle == null,
                                 Comment = mi.Comment,
                                 Isbn = mi.Isbn,
                                 DatePublished = (from e in mi.RelatedEvents
                                                  where e.TypeId == NietzscheConstants.PublishingEventTypeId
                                                  select e.Occurrence.Start)
                                                 .FirstOrDefault(),
                                 LocationPublished = (from e in mi.RelatedEvents
                                                      from li in e.LocationInvolvements
                                                      let l = li.Location
                                                      let pp = l as PopulatedPlace
                                                      let a = l as Address
                                                      select new LocationInfo
                                                      {
                                                          Id = l.Id,
                                                          Title = a != null
                                                                ? a.PopulatedPlace.Name
                                                                : pp != null
                                                                ? pp.Name
                                                                : ""
                                                      })
                                                     .FirstOrDefault(),
                                 Authors = authors,
                                 Beneficiaries = beneficiaries
                             };

            var mediaItems2 = from mi in await mediaItems.ToListAsync()
                              orderby mi.Title
                              group mi by mi.Comment != null ? this.pluralizationService.Pluralize(mi.Comment) : "Others" into items
                              orderby items.Key
                              select items;

            var viewModel = new Index
            {
                Participant = participant,
                MediaItems = mediaItems2.ToDictionary(
                    g => g.Key,
                    g => (IEnumerable<MediaItemInfo>)g)
            };

            return View(viewModel);
        }

        [GET(NietzscheConstants.MediaItemDetailUrl, RouteName = "MediaItemDetail")]
        [OutputCache(CacheProfile = "LongCache")]
        public async Task<ActionResult> Detail(long id)
        {
            var mediaItem = await (from mi in this.mediaItemRepository
                                       .Include(mi => mi.Type)
                                   where mi.Id == id
                                   select mi)
                                   .FirstOrDefaultAsync();

            if (mediaItem == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = new Detail
            {
                MediaItem = mediaItem
            };

            return View(viewModel);
        }
    }
}