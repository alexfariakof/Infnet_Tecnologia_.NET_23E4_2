using Domain.Core.AggreggatesBase;

namespace Domain.Streaming.Agreggates
{
    public class Album : Base
    {
        public string Name { get; set; }
        public List<Music> Music { get; set; } = new List<Music>();

        public void AddMusic(Music music) => this.Music.Add(music);
        public void AddMusic(List<Music> music) => this.Music.AddRange(music);
    }
}
