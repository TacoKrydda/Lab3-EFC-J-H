using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Lab3_EFC_J_H.Models;

namespace Lab3_EFC_J_H.Data
{
    public partial class everyloopContext : DbContext
    {
        public everyloopContext()
        {
        }

        public everyloopContext(DbContextOptions<everyloopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; } = null!;
        public virtual DbSet<Artist> Artists { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<MediaType> MediaTypes { get; set; } = null!;
        public virtual DbSet<Playlist> Playlists { get; set; } = null!;
        public virtual DbSet<PlaylistTrack> PlaylistTracks { get; set; } = null!;
        public virtual DbSet<Track> Tracks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=everyloop;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.Property(e => e.AlbumId).ValueGeneratedNever();

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_albums_artists");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(e => e.ArtistId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId).ValueGeneratedNever();
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.Property(e => e.MediaTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.Property(e => e.PlaylistId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.Property(e => e.TrackId).ValueGeneratedNever();

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.AlbumId)
                    .HasConstraintName("FK_tracks_albums");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_tracks_genres");

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.MediaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tracks_media_types");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
