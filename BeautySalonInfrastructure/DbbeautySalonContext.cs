using System;
using System.Collections.Generic;
using BeautySalonDomain.Model;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonInfrastructure;

public partial class DbbeautySalonContext : DbContext
{
    public DbbeautySalonContext()
    {
    }

    public DbbeautySalonContext(DbContextOptions<DbbeautySalonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeService> EmployeeServices { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<TypeService> TypeServices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VLOMFU1\\SQLEXPRESS; Database=DBBeautySalon; Trusted_Connection=True; TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Positions).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Positions");
        });

        modelBuilder.Entity<EmployeeService>(entity =>
        {
            entity.HasOne(d => d.Employees).WithMany(p => p.EmployeeServices)
                .HasForeignKey(d => d.EmployeesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeServices_Employees");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.Property(e => e.ClientsId).HasColumnName("ClientsID");
            entity.Property(e => e.Info).HasColumnType("ntext");
            entity.Property(e => e.ServicesId).HasColumnName("ServicesID");

            entity.HasOne(d => d.Clients).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ClientsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_Clients");

            entity.HasOne(d => d.Schedules).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.SchedulesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_Schedules");

            entity.HasOne(d => d.Services).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ServicesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_Services");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.Property(e => e.EmployeesId).HasColumnName("EmployeesID");
            entity.Property(e => e.EndTime).HasPrecision(0);
            entity.Property(e => e.StartTime).HasPrecision(0);

            entity.HasOne(d => d.Employees).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.EmployeesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedules_Employees");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Favors");

            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(5, 0)");

            entity.HasOne(d => d.TypeService).WithMany(p => p.Services)
                .HasForeignKey(d => d.TypeServiceId)
                .OnDelete(DeleteBehavior.Cascade) // Changed to Cascade
                .HasConstraintName("FK_Services_TypeServices");
        });

        modelBuilder.Entity<TypeService>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
