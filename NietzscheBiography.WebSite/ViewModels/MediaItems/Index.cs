namespace NietzscheBiography.WebSite.ViewModels.MediaItems
{
    using NietzscheBiography.Domain.Models;
    using System.Collections.Generic;

    public class Index
    {
        public Participant Participant;
        public IDictionary<string, IEnumerable<MediaItemInfo>> MediaItems;
    }
}