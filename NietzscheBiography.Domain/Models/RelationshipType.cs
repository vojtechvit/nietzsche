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
    
    public partial class RelationshipType
    {
        public RelationshipType()
        {
            this.RelationshipsWhereInverseType = new HashSet<Relationship>();
            this.RelationshipsWhereType = new HashSet<Relationship>();
        }
    
        public int Id { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Label { get; set; }
    
        public virtual ICollection<Relationship> RelationshipsWhereInverseType { get; set; }
        public virtual ICollection<Relationship> RelationshipsWhereType { get; set; }
        public virtual RelationshipCategory Category { get; set; }
    }
}