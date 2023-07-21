using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PGM_ORM.Models;

public partial class OrmcrudContext : DbContext
{
    public OrmcrudContext()
    {
    }

    public OrmcrudContext(DbContextOptions<OrmcrudContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Solicitude> Solicitudes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost; database=ORMCRUD; integrated security=true; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento);

            entity.ToTable("departamento");

            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.JefeDepartamento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Jefe_departamento");
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_departamento");
        });

        modelBuilder.Entity<Solicitude>(entity =>
        {
            entity.HasKey(e => e.IdSolicitud).HasName("PK_Solicitudes");

            entity.ToTable("solicitudes");

            entity.Property(e => e.IdSolicitud)
                .ValueGeneratedNever()
                .HasColumnName("Id_solicitud");
            entity.Property(e => e.ApellidosSolicitud)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Apellidos_solicitud");
            entity.Property(e => e.CorreoSolicitud)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Correo_solicitud");
            entity.Property(e => e.DetalleSolicitud)
                .HasColumnType("text")
                .HasColumnName("Detalle_solicitud");
            entity.Property(e => e.FechaSolicitud)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_solicitud");
            entity.Property(e => e.NombreSolicitud)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_solicitud");
            entity.Property(e => e.RunSolicitud)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Run_solicitud");
            entity.Property(e => e.Servicio)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SolicitudUsuario).HasColumnName("Solicitud_usuario");
            entity.Property(e => e.TelefonoSolicitud)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Telefono_solicitud");

            entity.HasOne(d => d.SolicitudUsuarioNavigation).WithMany(p => p.Solicitudes)
                .HasForeignKey(d => d.SolicitudUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Solicitudes_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3213E83F4EE965A1");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Clave)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("date")
                .HasColumnName("Fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuariosDepartamento).HasColumnName("usuarios_departamento");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usuarios_departamento");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
