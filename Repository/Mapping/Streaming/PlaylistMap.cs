﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Streaming.Agreggates;
using Domain.Account.Agreggates;

namespace Repository.Mapping.Streaming
{
    public class PlaylistMap : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.ToTable(nameof(Playlist));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.HasMany(x => x.Musics)
                    .WithMany(x => x.Playlists)
                    .UsingEntity<Dictionary<string, object>>(
                    "MusicPlayList",
                    j => j
                        .HasOne<Music<Playlist>>()
                        .WithMany()
                        .HasForeignKey("MusicId"),
                    j => j
                        .HasOne<Playlist>()
                        .WithMany()
                        .HasForeignKey("PlaylistPersonalId"),
                    j =>
                    {
                        j.HasKey("MusicId", "PlaylistPersonalId");
                        j.Property<DateTime>("DtAdded");
                    });
        }
    }
}