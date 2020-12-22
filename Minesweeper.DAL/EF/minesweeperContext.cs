using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Minesweeper.DAL.Entities;

namespace Minesweeper.DAL.EF
{
    public partial class minesweeperContext : DbContext
    {
        public minesweeperContext()
        {
        }

        public minesweeperContext(DbContextOptions<minesweeperContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Info> Infos { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile("DBSettings.json");
                var config = builder.Build();
                optionsBuilder.UseNpgsql(config.GetConnectionString("RemoteConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("game");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.IdLevel).HasColumnName("id_level");

                entity.Property(e => e.IdPlayer).HasColumnName("id_player");

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.IdLevelNavigation)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.IdLevel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_fk0");

                entity.HasOne(d => d.IdPlayerNavigation)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.IdPlayer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_fk1");
            });

            modelBuilder.Entity<Info>(entity =>
            {
                entity.ToTable("info");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.ToTable("level");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Complexity)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdInfo).HasColumnName("id_info");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.IdInfoNavigation)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.IdInfo)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                    //.HasConstraintName("player_fk0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
