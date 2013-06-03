namespace NietzscheBiography.WebSite.ViewModels
{
    using NietzscheBiography.Domain.Models;
    using System.Collections.Generic;

    public class IndividualInfo : ParticipantInfo
    {
        public Name FullName;
        public string Profession;
        public string Nationality;
        public IEnumerable<string> Relationships;
    }
}