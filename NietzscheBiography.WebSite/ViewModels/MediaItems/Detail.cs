namespace NietzscheBiography.WebSite.ViewModels.MediaItems
{
    using NietzscheBiography.Domain.Models;
    using System.Collections.Generic;

    public class Detail
    {
        /// <summary>
        /// Included Navigation Properties:
        /// - Type
        /// </summary>
        public MediaItem MediaItem;

        public MediaItemInfo Original;
        public MediaItemInfo Composite;
        public IEnumerable<MediaItemInfo> Parts;
        public IEnumerable<MediaItemInfo> Editions;
        public IEnumerable<IndividualInfo> RelatedIndividuals;
        public IEnumerable<OrganizationInfo> RelatedOrganizations;
        public IEnumerable<CitationInfo> Citations;
    }
}