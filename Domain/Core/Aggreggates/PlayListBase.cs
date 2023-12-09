using Domain.Core.AggreggatesBase;
using Domain.Streaming.Agreggates;

namespace Domain.Core.Aggreggates
{
    public abstract class PlayListBase : Base
    {
        public string Name { get; set; }
        public List<Music> Musics { get; set; }
    }
}
