using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL;

/// <summary>
/// Represents the Entity Framework Core database context for the application.
/// </summary>
/// <remarks>
/// This class is responsible for configuring and managing the database connection, entity relationships, and
/// database schema mappings for the application. It inherits from the <see cref="DbContext"/> class.
/// </remarks> 
public class S4CDbContext : DbContext
{
    public S4CDbContext(DbContextOptions<S4CDbContext> options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Character>(entity =>
        {
            entity.ToTable("characters");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Weight).IsRequired();
            entity.Property(x => x.WeightCategory).IsRequired();
            entity.Property(x => x.SerieId).IsRequired();
            entity.HasOne(x => x.Serie).WithMany().HasForeignKey(x => x.SerieId).IsRequired();
            entity.HasIndex(x => x.SerieId);
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Username).IsRequired();
            entity.HasAlternateKey(x => x.Username);
            entity.Property(x => x.Password).IsRequired();
            entity.Property(x => x.Email).IsRequired();
            entity.HasAlternateKey(x => x.Email);
            entity.Property(x => x.CharacterId).IsRequired(false);
            entity.HasOne(c => c.Character)
                .WithMany()
                .HasForeignKey(x => x.CharacterId)
                .IsRequired(false);
            entity.HasIndex(x => x.CharacterId);
        });
        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("sessions");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Token).IsRequired();
            entity.Property(x => x.Expiration).IsRequired();
            entity.Property(x => x.UserId).IsRequired();
            entity.HasOne(x => x.User).WithMany(u => u.Sessions).HasForeignKey(x => x.UserId).IsRequired(false);
            entity.HasIndex(x => x.UserId);
        });
        modelBuilder.Entity<Serie>(entity =>
        {
            entity.ToTable("series");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Name).IsRequired();
            entity.HasIndex(x => x.Name);
        });
        modelBuilder.Entity<Challenge>(entity =>
        {
            entity.ToTable("challenges");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Description).IsRequired();
            entity.Property(x => x.WeightCategory).IsRequired();
            entity.Property(x => x.SerieId).IsRequired(false);
            entity.Property(x => x.CharacterId).IsRequired(false);
            entity.HasOne(x => x.Serie).WithMany(s => s.Challenges).HasForeignKey(x => x.SerieId).IsRequired(false);
            entity.HasOne(x => x.Character).WithMany(ch => ch.Challenges).HasForeignKey(x => x.CharacterId).IsRequired(false);
            entity.HasIndex(x => x.CharacterId);
            entity.HasIndex(x => x.SerieId);
        });
    }

    #region Recognition

    public DbSet<Character> Characters { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Serie> Series { get; set; }
    public DbSet<Challenge> Challenges { get; set; }
    #endregion
}