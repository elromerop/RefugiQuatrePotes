using Microsoft.EntityFrameworkCore;
using RefugiQuatrePotes.Models;

namespace RefugiQuatrePotes.Data;

public class RefugiContext : DbContext
{
    public RefugiContext(){}
    
    public DbSet<Animal> Animal { get; set; }
    public DbSet<Xip> Xip { get; set; }
    public DbSet<Adoptant> Adoptant { get; set; }
    public DbSet<Voluntari> Voluntari { get; set; }
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
        
        modelBuilder.Entity<Animal>()
            .HasOne(an => an.Adoptant)
            .WithMany(ad => ad.Animals)
            .HasForeignKey("AdoptantID")
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Voluntari>()
            .HasMany(v => v.Animals)
            .WithMany(a => a.Voluntaris)
            .UsingEntity(c => c.ToTable("Cura"));

        modelBuilder.Entity<Animal>()
            .Property(a => a.Especie)
            .HasConversion<string>();
        
        modelBuilder.Entity<Voluntari>()
            .Property(v => v.Torn)
            .HasConversion<string>();
    }
}