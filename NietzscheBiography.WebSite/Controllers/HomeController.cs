using AttributeRouting.Web.Mvc;
using NietzscheBiography.Domain.Models;
using NietzscheBiography.WebSite.ViewModels;
using NietzscheBiography.WebSite.ViewModels.Home;
using System.Linq;
using System.Web.Mvc;

namespace NietzscheBiography.WebSite.Controllers
{
    public class HomeController : BootstrapBaseController
    {
        private IQueryable<Participant> participantRepository;
        private IQueryable<Event> eventRepository;
        private IQueryable<MediaItem> mediaItemRepository;

        public HomeController(
            IQueryable<Participant> participantRepository,
            IQueryable<Event> eventRepository,
            IQueryable<MediaItem> mediaItemRepository)
        {
            this.participantRepository = participantRepository;
            this.eventRepository = eventRepository;
            this.mediaItemRepository = mediaItemRepository;
        }

        [GET("")]
        [OutputCache(CacheProfile = "LongCache")]
        public ActionResult Index()
        {
            var nietzsche = (from i in this.participantRepository
                                .OfType<Individual>()
                             where i.Id == NietzscheConstants.NietzscheId
                             select new IndividualInfo
                             {
                                 Id = i.Id,
                                 Name = i.Name,
                                 Nationality = i.Nationality.Nationality.Singular,
                                 Profession = i.Profession
                             })
                             .FirstOrDefault();

            var viewModel = new Index
            {
                Nietzsche = nietzsche
            };

            return View(viewModel);
        }
    }
}