using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Models;

public partial class DbcrudHoteleriaContext : DbContext
{
    public DbcrudHoteleriaContext()
    {
    }

    public DbcrudHoteleriaContext(DbContextOptions<DbcrudHoteleriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Habitacion> Habitaciones { get; set; }

    public virtual DbSet<Hotel> Hoteles { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__8BBBF901DA18AD8A");

            entity.ToTable("Habitacion");

            entity.Property(e => e.Estado).HasColumnType("text");
            entity.Property(e => e.NumeroHabitacion)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("Numero_Habitacion");

            entity.HasOne(d => d.IdHotelNavigation).WithMany(p => p.Habitaciones)
                .HasForeignKey(d => d.IdHotel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__IdHot__4E88ABD4");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.IdHotel).HasName("PK__Hotel__653BCCC43038B0DD");

            entity.ToTable("Hotel");

            entity.Property(e => e.Activo).HasColumnType("text");
            entity.Property(e => e.CantHabitaciones)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("Cant_Habitaciones");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Latitud).HasColumnType("decimal(8, 6)");
            entity.Property(e => e.Longitud).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reserva__0E49C69D3A7E69B0");

            entity.ToTable("Reserva");

            entity.Property(e => e.Estado).HasColumnType("text");
            entity.Property(e => e.FechaEntrada)
                .HasColumnType("date")
                .HasColumnName("Fecha_Entrada");
            entity.Property(e => e.FechaReserva)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("Fecha_Reserva");
            entity.Property(e => e.FechaSalida)
                .HasColumnType("date")
                .HasColumnName("Fecha_Salida");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reserva__IdHabit__534D60F1");

            entity.HasOne(d => d.IdHotelNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdHotel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reserva__IdHotel__52593CB8");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reserva__IdUsuar__5165187F");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9711B62BD1");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Mail, "UQ__Usuario__2724B2D10837D141").IsUnique();

            entity.Property(e => e.Apellidos)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Mail)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
