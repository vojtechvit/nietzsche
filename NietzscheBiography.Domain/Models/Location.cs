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
    
    public partial class Location
    {
        public Location()
        {
            this.EventInvolvements = new HashSet<LocationEventInvolvement>();
            this.Organizations = new HashSet<Organization>();
        }
    
        public long Id { get; set; }
        protected LocationEntityType EntityType { get; set; }
        public System.Data.Entity.Spatial.DbGeography GeoLocation { get; set; }
    
        public virtual ICollection<LocationEventInvolvement> EventInvolvements { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
    }
}