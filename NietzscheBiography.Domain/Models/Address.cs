//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NietzscheBiography.Domain.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Address : Location
    {
        public long PopulatedPlaceId { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string BuildingNumber { get; set; }
        public string Entrance { get; set; }
        public Nullable<short> Floor { get; set; }
        public string ApartmentNumber { get; set; }
    
        public virtual PopulatedPlace PopulatedPlace { get; set; }
    }
}
