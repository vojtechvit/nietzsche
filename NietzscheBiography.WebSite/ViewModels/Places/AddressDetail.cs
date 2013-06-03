namespace NietzscheBiography.WebSite.ViewModels.Places
{
    using NietzscheBiography.Domain.Models;

    public class AddressDetail : Detail
    {
        /// <summary>
        /// Included Navigation Properties:
        /// - PopulatedPlace
        /// - PopulatedPlace.Country
        /// </summary>
        public Address Address;
    }
}