namespace NietzscheBiography.WebSite.ViewModels.Connections
{
    using NietzscheBiography.Domain.Models;
    using System.Collections.Generic;

    public class Index
    {
        public Participant Participant;
        public IDictionary<string, IEnumerable<IndividualInfo>> Relatives;
        public IList<IndividualInfo> Others;
        public IList<OrganizationInfo> Organizations;
    }
}