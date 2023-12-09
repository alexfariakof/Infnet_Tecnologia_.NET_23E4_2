using Domain.Core.AggreggatesBase;

namespace Domain.Streaming.Agreggates
{
    public class Band : Base
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String Backdrop { get; set; }
        public List<Album> Albums { get; set; } = new List<Album>();
        public void AddAlbum(Album album) => this.Albums.Add(album);
    }
}
