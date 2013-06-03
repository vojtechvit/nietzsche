namespace NietzscheBiography.WebSite.ViewModels
{
    using System.Collections.Generic;

    public abstract class RelationshipInfo
    {
        public string Title;
        public IEnumerable<IntervalInfo> Intervals;
    }
}