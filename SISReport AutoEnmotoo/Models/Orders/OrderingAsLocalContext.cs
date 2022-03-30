using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SISReport_AutoEnmotoo.Models.Orders
{
    public partial class OrderingAsLocalContext : DbContext
    {
        public OrderingAsLocalContext()
        {
        }

        public OrderingAsLocalContext(DbContextOptions<OrderingAsLocalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SpGetConsolidate> SpConsolidate { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=10.10.228.2\\SQL2014; Database=OrderingAsLocal; User=AppsContabilidad; Password=C@NTP4Q!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Oid);

                entity.ToTable("orders");

                entity.Property(e => e.Oid)
                    .ValueGeneratedNever()
                    .HasColumnName("oid");

                entity.Property(e => e.CantidadProductos).HasColumnName("cantidadProductos");

                entity.Property(e => e.Comision)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("comision");

                entity.Property(e => e.CorreoCliente)
                    .HasMaxLength(150)
                    .HasColumnName("correoCliente");

                entity.Property(e => e.DeliveryType)
                    .HasMaxLength(150)
                    .HasColumnName("deliveryType");

                entity.Property(e => e.Descuento)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("descuento");

                entity.Property(e => e.Estado)
                    .HasMaxLength(150)
                    .HasColumnName("estado");

                entity.Property(e => e.Factura)
                    .HasMaxLength(150)
                    .HasColumnName("factura");

                entity.Property(e => e.Fecha)
                    .HasMaxLength(150)
                    .HasColumnName("fecha");

                entity.Property(e => e.GastosEnvio)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("gastosEnvio");

                entity.Property(e => e.Hora)
                    .HasMaxLength(150)
                    .HasColumnName("hora");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdNegocio).HasColumnName("idNegocio");

                entity.Property(e => e.IdRanger).HasColumnName("idRanger");

                entity.Property(e => e.MetodoPago)
                    .HasMaxLength(150)
                    .HasColumnName("metodoPago");

                entity.Property(e => e.Negocio)
                    .HasMaxLength(150)
                    .HasColumnName("negocio");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(150)
                    .HasColumnName("nombreCliente");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.PayData)
                    .HasMaxLength(150)
                    .HasColumnName("payData");

                entity.Property(e => e.PorcentajeComision)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("porcentajeComision");

                entity.Property(e => e.PorcentajePropina)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("porcentajePropina");

                entity.Property(e => e.Productos).HasColumnName("productos");

                entity.Property(e => e.Propina)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("propina");

                entity.Property(e => e.Ranger)
                    .HasMaxLength(200)
                    .HasColumnName("ranger");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("subtotal");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("total");
                entity.Property(e => e.Telefono)
                    .HasMaxLength(150)
                    .HasColumnName("Telefono");
            });

            modelBuilder.Entity<SpGetConsolidate>(entity =>
            {
                entity.HasKey(e => e.NEGOCIO);
            });

                OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
