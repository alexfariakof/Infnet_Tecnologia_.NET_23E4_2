using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;
using Domain.Streaming.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping.Streaming
{
    public class MusicMap : IEntityTypeConfiguration<Music<Playlist>>
    {
        public void Configure(EntityTypeBuilder<Music<Playlist>> builder)
        {
            ConfigureCommon(builder);
            builder.HasMany(x => x.Playlists)
                .WithMany(m => m.Musics)
                .UsingEntity(j => j.ToTable("MusicPlaylists"));
        }

        private void ConfigureCommon(EntityTypeBuilder<Music<Playlist>> builder)
        {
            builder.ToTable(nameof(Music<Playlist>));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.OwnsOne<Duration>(x => x.Duration, c =>
            {
                c.Property(d => d.Value).HasColumnName("DurationValue").IsRequired().HasMaxLength(50);
            });
        }
    }

    public class MusicPersonalMap : IEntityTypeConfiguration<Music<PlaylistPersonal>>
    {
        public void Configure(EntityTypeBuilder<Music<PlaylistPersonal>> builder)
        {
            ConfigureCommon(builder);
            builder.HasMany(x => x.Playlists)
                .WithMany(m => m.Musics)
                .UsingEntity(j => j.ToTable("MusicPersonalPlaylists"));
        }

        private void ConfigureCommon(EntityTypeBuilder<Music<PlaylistPersonal>> builder)
        {
            builder.ToTable(nameof(Music<PlaylistPersonal>));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.OwnsOne<Duration>(x => x.Duration, c =>
            {
                c.Property(d => d.Value).HasColumnName("DurationValue").IsRequired().HasMaxLength(50);
            });
        }
    }
}
