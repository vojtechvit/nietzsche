namespace NietzscheBiography.WebSite.ViewModels.Places
{
    using NietzscheBiography.Domain.Models;
    using System.Collections.Generic;

    public class CountryDetail : Detail
    {
        /// <summary>
        /// Included Navigation Properties:
        ///
        /// </summary>
        public Country Country;

        public IEnumerable<IndividualInfo> Nationals;
        public IEnumerable<LocationInfo> PopulatedPlaces;
    }
}