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
    
    public partial class MediaItem
    {
        public MediaItem()
        {
            this.Citations = new HashSet<Citation>();
            this.Editions = new HashSet<MediaItem>();
            this.Parts = new HashSet<MediaItem>();
            this.RelatedEvents = new HashSet<Event>();
        }
    
        public long Id { get; set; }
        public Nullable<long> OriginalId { get; set; }
        public Nullable<long> CompositeId { get; set; }
        public Nullable<int> TypeId { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Url { get; set; }
        public string Isbn { get; set; }
        public string Comment { get; set; }
    
        public virtual ICollection<Citation> Citations { get; set; }
        public virtual ICollection<MediaItem> Editions { get; set; }
        public virtual MediaItem Original { get; set; }
        public virtual MediaItemType Type { get; set; }
        public virtual ICollection<MediaItem> Parts { get; set; }
        public virtual MediaItem Composite { get; set; }
        public virtual ICollection<Event> RelatedEvents { get; set; }
    }
}
