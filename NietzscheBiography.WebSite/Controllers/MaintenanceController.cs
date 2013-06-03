namespace NietzscheBiography.WebSite.Controllers
{
    using AttributeRouting.Web.Mvc;
    using NietzscheBiography.Domain.Models;
    using NietzscheBiography.WebSite.Infrastructure;
    using NietzscheBiography.WebSite.Infrastructure.Dal;
    using NietzscheBiography.WebSite.Infrastructure.Mvc;
    using NietzscheBiography.WebSite.Infrastructure.Services;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class MaintenanceController : Controller
    {
        private NietzscheBiographyDbContext context;
        private ISentenceSynthesizer sentenceSynthetizer;

        public MaintenanceController(
            NietzscheBiographyDbContext context,
            ISentenceSynthesizer sentenceSynthetizer)
        {
            this.context = context;
            this.sentenceSynthetizer = sentenceSynthetizer;
        }

        [GET("maintenance/update-event-titles")]
        [OutputCache(CacheProfile = "LongCache")]
        public async Task<ActionResult> UpdateTitleCache()
        {
            var events = from e in this.context.Events
                         where e.Occurrence.Start.Date != null
                         select new
                         {
                             Id = e.Id,
                             TypeName = e.Type.Label,

                             Participants = from pi in e.ParticipantInvolvements
                                            let p = pi.Participant
                                            select new
                                            {
                                                ThematicRole = pi.ThematicRole.Name,
                                                Id = pi.ParticipantId,
                                                Name = p.Name
                                            },

                             Locations = from li in e.LocationInvolvements
                                         let l = li.Location
                                         let c = l as Country
                                         let pp = l as PopulatedPlace
                                         let a = l as Address
                                         select new
                                         {
                                             ThematicRole = li.ThematicRole.Name,
                                             Id = li.LocationId,
                                             Label = c != null ? c.Name
                                                   : pp != null ? pp.Name
                                                       + (pp.Country != null ? ", " + pp.Country.Name : "")
                                                   : a != null ? a.StreetName
                                                       + (a.BuildingNumber != null ? " " + a.BuildingNumber + ", " : ", ")
                                                       + a.PopulatedPlace.Name
                                                       + (a.PopulatedPlace.Country != null ? ", " + a.PopulatedPlace.Country.Name : "")
                                                   : "",
                                         },

                             MediaItems = from mi in e.RelatedMediaItems
                                          let isTypeLabel = mi.Title == null && mi.OriginalTitle == null
                                          select new
                                          {
                                              Id = mi.Id,
                                              Label = mi.Title ?? mi.OriginalTitle ?? mi.Type.Label,
                                              IsTypeLabel = isTypeLabel,
                                              Authors = from e2 in mi.RelatedEvents
                                                        where isTypeLabel
                                                            && e2.TypeId == NietzscheConstants.WritingEventTypeId
                                                        from pi in e2.ParticipantInvolvements
                                                        where pi.ThematicRoleId == NietzscheConstants.AgentThematicRoleId
                                                            // && pi.ParticipantId != NietzscheConstants.NietzscheId
                                                        group pi.Participant by pi.Participant.Id into ps
                                                        let p = ps.FirstOrDefault()
                                                        let i = p as Individual
                                                        select i != null
                                                            ? i.FullName.LastName ?? i.Name
                                                            : p.Name
                                          }
                         };

            var events2 = await events.ToListAsync();

            var eventsData = from e in events2
                             let participantNouns = from p in e.Participants
                                                    select new NounPhrase
                                                    {
                                                        ThematicRole = p.ThematicRole,
                                                        Text = Hyperlink(Url.Connection(p.Id), p.Name)
                                                    }
                             let locationNouns = from l in e.Locations
                                                 select new NounPhrase
                                                 {
                                                     ThematicRole = l.ThematicRole,
                                                     Text = Hyperlink(Url.Place(l.Id), l.Label)
                                                 }
                             let mediaItemNouns = from mi in e.MediaItems
                                                  let label = mi.IsTypeLabel
                                                      ? mi.Label + " by " + Util.TextEnum(mi.Authors)
                                                      : mi.Label
                                                  select new NounPhrase
                                                  {
                                                      ThematicRole = "Object",
                                                      Text = Hyperlink(Url.MediaItem(mi.Id), label)
                                                  }
                             let nouns = participantNouns.Union(locationNouns).Union(mediaItemNouns)

                             select new
                             {
                                 Id = e.Id,
                                 Title = this.sentenceSynthetizer.Synthetize(e.TypeName, nouns)
                             };

            var connectionString = ConfigurationManager.ConnectionStrings["NietzscheBiography"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    foreach (var ed in eventsData.ToList())
                    {
                        var sqlCommand = new SqlCommand();
                        sqlCommand.CommandText = "UPDATE [nietzschebiography].[event_title_cache] SET [title] = @title WHERE [event_id] = @id "
                                               + "IF @@ROWCOUNT=0 "
                                               + "INSERT INTO [nietzschebiography].[event_title_cache] ([event_id], [title]) VALUES (@id, @title)";

                        var idParam = new SqlParameter("id", ed.Id);
                        var titleParam = new SqlParameter("title", ed.Title);
                            
                        sqlCommand.Parameters.Add(idParam);
                        sqlCommand.Parameters.Add(titleParam);

                        sqlCommand.Connection = connection;
                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(500, e.Message);
                }
            }

            return new HttpStatusCodeResult(200);
        }

        private string Hyperlink(string url, string content, string title = null)
        {
            const string template = "<a href=\"{0}\" title=\"{2}\">{1}</a>";

            return string.Format(template, url, content, title ?? "More on " + content);
        }
    }
}
