namespace NietzscheBiography.WebSite.ViewModels.Connections
{
    using NietzscheBiography.Domain.Models;
    using System.Collections.Generic;

    public class Detail
    {
        public Participant Participant;
        public string Nationality;
        public string Location;

        public IEnumerable<string> AlternativeNames;
        public IEnumerable<ImageInfo> Images;

        public Individual Individual
        {
            get { return this.Participant as Individual; }
        }

        public Organization Organization
        {
            get { return this.Participant as Organization; }
        }
    }
}