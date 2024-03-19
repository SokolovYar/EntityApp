using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1;

public partial class CountriesContext : DbContext
{
    public CountriesContext()
    {
    }

    public CountriesContext(DbContextOptions<CountriesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB;initial catalog=Countries;integrated security=True;MultipleActiveResultSets=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3214EC2746B098D9");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idcountry).HasColumnName("IDcountry");
            entity.Property(e => e.NameCity).HasMaxLength(255);

            entity.HasOne(d => d.IdcountryNavigation).WithMany(p => p.Cities)
                .HasForeignKey(d => d.Idcountry)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cities__IDcountr__300424B4");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC2762B1896F");

            entity.ToTable("Country");

            entity.HasIndex(e => e.NameCountry, "UQ__Country__3369B17A79B2EAB6").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idregion).HasColumnName("IDregion");
            entity.Property(e => e.NameCountry).HasMaxLength(255);
            entity.Property(e => e.Square).HasColumnName("Square_");

            entity.HasOne(d => d.IdregionNavigation).WithMany(p => p.Countries)
                .HasForeignKey(d => d.Idregion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Country__Square___286302EC");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Region__3214EC2754E23E12");

            entity.ToTable("Region");

            entity.HasIndex(e => e.NameRegion, "UQ__Region__1C151AC7ACB6DD2D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NameRegion).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
