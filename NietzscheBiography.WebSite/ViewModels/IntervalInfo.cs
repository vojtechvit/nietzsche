using System.Collections.Generic;

namespace NietzscheBiography.WebSite.ViewModels
{
    public class IntervalInfo
    {
        public string Description;
        public EventInfo InitiatingEvent;
        public EventInfo ConcludingEvent;
        public IEnumerable<CitationInfo> Citations;
    }
}