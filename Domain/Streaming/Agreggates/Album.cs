using Domain.Core.Aggreggates;

namespace Domain.Streaming.Agreggates
{
    public class Album : BaseModel
    {
        public string Name { get; set; }
        public List<Music<Playlist>> Music { get; set; } = new List<Music<Playlist>>();
        public void AddMusic(Music<Playlist> music) => this.Music.Add(music);
        public void AddMusic(List<Music<Playlist>> music) => this.Music.AddRange(music);
    }
}
