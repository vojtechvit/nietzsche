namespace NietzscheBiography.WebSite.ViewModels.Home
{
    using System.Collections.Generic;

    public class Index
    {
        public IndividualInfo Nietzsche;
        public IList<RelationshipInfo> RandomConnections;
        public IList<EventInfo> RandomEvents;
        public IList<MediaItemInfo> RandomWork;
        public IList<MediaItemInfo> RandomLetters;
    }
}