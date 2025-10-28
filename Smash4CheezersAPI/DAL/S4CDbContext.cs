using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL;

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
            entity.Navigation(x => x.Serie).AutoInclude();
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Username).IsRequired();
            entity.Property(x => x.Password).IsRequired();
            entity.Property(x => x.Email).IsRequired();
            entity.HasAlternateKey(x => x.Email);
            entity.Navigation(x => x.Character);
        });
        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("sessions");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Token).IsRequired();
            entity.Property(x => x.Expiration).IsRequired();
            entity.Property(x => x.UserId).IsRequired();
            entity.Navigation(x => x.User);
        });
        modelBuilder.Entity<Serie>(entity =>
        {
            entity.ToTable("series");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Name).IsRequired();
        });
        modelBuilder.Entity<Challenge>(entity =>
        {
            entity.ToTable("challenges");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Description).IsRequired();
            entity.Property(x => x.WeightCategory).IsRequired();
            entity.Navigation(x => x.Serie);
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