using Domain.Core.Aggreggates;

namespace Domain.Streaming.Agreggates
{
    public class Playlist : BaseModel
    {
        public string Name { get; set; }
        Flat Flat { get; set; }
        public List<Music<Playlist>> Musics { get; set; }
    }
}
