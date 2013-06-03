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
    
    public partial class Relationship
    {
        public Relationship()
        {
            this.Intervals = new HashSet<Interval>();
        }
    
        public long Id { get; set; }
        public long DeterminantId { get; set; }
        public long ImmanentId { get; set; }
        public int TypeId { get; set; }
        public int InverseTypeId { get; set; }
    
        public virtual Participant Immanent { get; set; }
        public virtual Participant Determinant { get; set; }
        public virtual RelationshipType InverseType { get; set; }
        public virtual RelationshipType Type { get; set; }
        public virtual ICollection<Interval> Intervals { get; set; }
    }
}
