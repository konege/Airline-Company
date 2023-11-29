using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace midterm_project.Model;

public partial class KonegeContext : DbContext
{
    public KonegeContext()
    {
    }

    public KonegeContext(DbContextOptions<KonegeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientFlight> ClientFlights { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:konege-midterm.database.windows.net,1433;Initial Catalog=konege;Persist Security Info=False;User ID=20070006024@stu.yasar.edu.tr;Password=Ege662001*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Password;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Client__E67E1A04674277A2");

            entity.ToTable("Client");

            entity.HasIndex(e => e.Username, "UQ__Client__536C85E46C7203D8").IsUnique();

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientPassword).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<ClientFlight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ClientFl__3214EC27A1C45174");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientFlights)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__ClientFli__Clien__75A278F5");

            entity.HasOne(d => d.Flight).WithMany(p => p.ClientFlights)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK__ClientFli__Fligh__76969D2E");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flight__8A9E148EF3CECB90");

            entity.ToTable("Flight");

            entity.HasIndex(e => e.FlightNumber, "UQ__Flight__2EAE6F501150E9BA").IsUnique();

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.Departure).HasMaxLength(50);
            entity.Property(e => e.Destination).HasMaxLength(50);
            entity.Property(e => e.FlightDate).HasColumnType("date");
            entity.Property(e => e.FlightNumber).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
