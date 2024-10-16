using Microsoft.EntityFrameworkCore;

namespace ScreenTimeServer.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<TicketEntity> Tickets { get; set; }
    public DbSet<StarGroupEntity> StarGroups { get; set; }

    public DbSet<StarEntity> Stars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TicketEntity>(entity => {
            entity.ToTable("tickets");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.earnedDate)
                .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            entity.Property(t => t.UsedDate)
                .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        });

        modelBuilder.Entity<StarGroupEntity>(entity =>
        {
            entity.ToTable("star_groups");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).ValueGeneratedOnAdd();
            entity.Property(t => t.Earned).HasDefaultValue(0);
            entity.Property(t => t.Id).ValueGeneratedOnAdd();
            entity.HasMany(t => t.Stars).WithOne(t => t.Group).HasForeignKey(t => t.GroupId);
        });

        modelBuilder.Entity<StarEntity>(entity =>
        {
            entity.ToTable("stars");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).ValueGeneratedOnAdd();
        });
    }
}