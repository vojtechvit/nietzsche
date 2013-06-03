namespace NietzscheBiography.WebSite.ViewModels.Places
{
    using System.Collections.Generic;

    public abstract class Detail
    {
        public IEnumerable<EventInfo> RelatedEvents;
        public IEnumerable<OrganizationInfo> LocalOrganizations;
    }
}