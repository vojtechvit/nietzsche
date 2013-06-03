namespace NietzscheBiography.WebSite.ViewModels.Places
{
    using NietzscheBiography.Domain.Models;
    using System.Collections.Generic;

    public class PopulatedPlaceDetail : Detail
    {
        /// <summary>
        /// Included Navigation Properties:
        /// - Country
        /// </summary>
        public PopulatedPlace PopulatedPlace;

        public IEnumerable<LocationInfo> Addresses;
    }
}