namespace NietzscheBiography.WebSite.ViewModels
{
    using NietzscheBiography.Domain.Models;
    using System.Collections.Generic;
    using System.Web;

    public class EventInfo
    {
        public long Id;
        public EventOccurrence Occurrence;
        public string Title;
        public IEnumerable<CitationInfo> Citations;
    }
}