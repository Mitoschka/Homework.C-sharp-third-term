using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MovieWEBApp.MovieModels
{
    public partial class MyMovieDBContext : DbContext
    {
        public MyMovieDBContext()
        {
        }

        public MyMovieDBContext(DbContextOptions<MyMovieDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieStaff> MovieStaffs { get; set; }
        public virtual DbSet<MovieStaff1> MovieStaff1s { get; set; }
        public virtual DbSet<MovieTag> MovieTags { get; set; }
        public virtual DbSet<SimilarMovie> SimilarMovies { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyMovieDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.AverageRating).HasColumnName("averageRating");

                entity.Property(e => e.Language).HasColumnName("language");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            modelBuilder.Entity<MovieStaff>(entity =>
            {
                entity.HasKey(e => new { e.ActorsStaffId, e.IsActorMovieId });

                entity.ToTable("MovieStaff");

                entity.HasIndex(e => e.IsActorMovieId, "IX_MovieStaff_isActorMovieID");

                entity.Property(e => e.ActorsStaffId).HasColumnName("actorsStaffID");

                entity.Property(e => e.IsActorMovieId).HasColumnName("isActorMovieID");

                entity.HasOne(d => d.ActorsStaff)
                    .WithMany(p => p.MovieStaffs)
                    .HasForeignKey(d => d.ActorsStaffId);

                entity.HasOne(d => d.IsActorMovie)
                    .WithMany(p => p.MovieStaffs)
                    .HasForeignKey(d => d.IsActorMovieId);
            });

            modelBuilder.Entity<MovieStaff1>(entity =>
            {
                entity.HasKey(e => new { e.DirectorsStaffId, e.IsDirectorMovieId });

                entity.ToTable("MovieStaff1");

                entity.HasIndex(e => e.IsDirectorMovieId, "IX_MovieStaff1_isDirectorMovieID");

                entity.Property(e => e.DirectorsStaffId).HasColumnName("directorsStaffID");

                entity.Property(e => e.IsDirectorMovieId).HasColumnName("isDirectorMovieID");

                entity.HasOne(d => d.DirectorsStaff)
                    .WithMany(p => p.MovieStaff1s)
                    .HasForeignKey(d => d.DirectorsStaffId);

                entity.HasOne(d => d.IsDirectorMovie)
                    .WithMany(p => p.MovieStaff1s)
                    .HasForeignKey(d => d.IsDirectorMovieId);
            });

            modelBuilder.Entity<MovieTag>(entity =>
            {
                entity.HasKey(e => new { e.MoviesMovieId, e.TagsTagId });

                entity.ToTable("MovieTag");

                entity.HasIndex(e => e.TagsTagId, "IX_MovieTag_tagsTagID");

                entity.Property(e => e.MoviesMovieId).HasColumnName("moviesMovieID");

                entity.Property(e => e.TagsTagId).HasColumnName("tagsTagID");

                entity.HasOne(d => d.MoviesMovie)
                    .WithMany(p => p.MovieTags)
                    .HasForeignKey(d => d.MoviesMovieId);

                entity.HasOne(d => d.TagsTag)
                    .WithMany(p => p.MovieTags)
                    .HasForeignKey(d => d.TagsTagId);
            });

            modelBuilder.Entity<SimilarMovie>(entity =>
            {
                entity.ToTable("SimilarMovie");

                entity.HasIndex(e => e.MovieId, "IX_SimilarMovie_MovieID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.SimilarMovies).HasColumnName("similarMovies");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.SimilarMovies)
                    .HasForeignKey(d => d.MovieId);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.FullName).HasColumnName("fullName");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
