namespace NietzscheBiography.Domain.Models
{
    public partial class MediaItem
    {
        public string DisplayTitle
        {
            get
            {
                return this.Title ?? this.OriginalTitle ?? this.Type.Label;
            }
        }
    }
}