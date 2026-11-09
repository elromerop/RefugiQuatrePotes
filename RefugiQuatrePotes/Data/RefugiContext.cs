using Microsoft.EntityFrameworkCore;
using RefugiQuatrePotes.Models;

namespace RefugiQuatrePotes.Data;

public class RefugiContext : DbContext
{
    public RefugiContext(){}
    
    public DbSet<Animal> Animal { get; set; }
    public DbSet<Xip> Xip { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlite("Data Source=refugi.db").UseLazyLoadingProxies();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>()
            .HasOne(a => a.Xip)
            .WithOne(x => x.Animal)
            .HasForeignKey<Animal>("XipID")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}