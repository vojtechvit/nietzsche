namespace NietzscheBiography.WebSite.ViewModels.Events
{
    using NietzscheBiography.Domain.Models;
    using System.Collections.Generic;

    public class Timeline
    {
        public Participant Participant;
        public int? FilterMinYear;
        public int? FilterMaxYear;
        public IList<EventTypeInfo> EventTypes;
    }
}