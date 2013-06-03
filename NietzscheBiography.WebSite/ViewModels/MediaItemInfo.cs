namespace NietzscheBiography.WebSite.ViewModels
{
    using NietzscheBiography.Domain.Models;
    using System.Collections.Generic;

    public class MediaItemInfo
    {
        public long Id;
        public string Title;
        public string TypeLabel;
        public string Comment;
        public string Isbn;
        public string Url;
        public ImpreciseDate DatePublished;
        public LocationInfo LocationPublished;
        public bool TitleIsType;

        /// <summary>
        /// Included Navigation Properties:
        /// 
        /// </summary>
        public IEnumerable<Participant> Authors;
        public IEnumerable<Participant> Beneficiaries;
    }
}