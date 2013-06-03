namespace NietzscheBiography.WebSite.ViewModels.Events
{
    using NietzscheBiography.Domain.Models;
    using System.Collections.Generic;

    public class Detail
    {
        /// <summary>
        /// Included Navigation Properties:
        /// - Type
        /// - Type.Category
        /// </summary>
        public Event Event;

        public IEnumerable<ParticipantInfo> RelatedParticipants;
        public IEnumerable<LocationInfo> RelatedLocations;
        public IEnumerable<MediaItemInfo> RelatedMediaItems;
        public IEnumerable<IntervalInfo> InitializedIntervals;
        public IEnumerable<IntervalInfo> ConcludedIntervals;
        public IEnumerable<CitationInfo> Citations;
    }
}