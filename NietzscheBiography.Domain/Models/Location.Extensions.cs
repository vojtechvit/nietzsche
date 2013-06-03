namespace NietzscheBiography.Domain.Models
{
    public partial class Location
    {
        public string Title
        {
            get
            {
                if (this is Address)
                {
                    var address = this as Address;

                    return string.Join(
                        ", ",
                        address.StreetName,
                        address.PopulatedPlace.Name);
                }
                else if (this is PopulatedPlace)
                {
                    var populatedPlace = this as PopulatedPlace;

                    return populatedPlace.Name;
                }
                else if (this is Country)
                {
                    var country = this as Country;

                    return country.Name;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}