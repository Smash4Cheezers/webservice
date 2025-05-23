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
            entity.Property(x => x.Serie).IsRequired();
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Username).IsRequired();
            entity.Property(x => x.Password).IsRequired();
            entity.Property(x => x.Email).IsRequired();
        });
    }

    #region Recognition

    public DbSet<Character> Characters { get; set; }
    public DbSet<User> Users { get; set; }

        #endregion
    
    
}