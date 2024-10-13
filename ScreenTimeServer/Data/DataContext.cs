using Microsoft.EntityFrameworkCore;

namespace ScreenTimeServer.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<TicketEntity> Tickets { get; set; }
    public DbSet<StarsEntity> Stars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TicketEntity>(entity => {
            entity.ToTable("tickets");
            entity.HasKey(t => t.Id);
        });

        modelBuilder.Entity<StarsEntity>(entity =>
        {
            entity.ToTable("stars");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).ValueGeneratedOnAdd();
        });
    }
}