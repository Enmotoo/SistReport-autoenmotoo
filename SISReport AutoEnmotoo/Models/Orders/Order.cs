using System;
using System.Collections.Generic;

#nullable disable

namespace SISReport_AutoEnmotoo.Models.Orders
{
    public partial class Order
    {
        public int Oid { get; set; }
        public int? OrderId { get; set; }
        public string DeliveryType { get; set; }
        public string Factura { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public int? IdNegocio { get; set; }
        public string Negocio { get; set; }
        public decimal? PorcentajeComision { get; set; }
        public decimal? Comision { get; set; }
        public int? IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string CorreoCliente { get; set; }
        public string MetodoPago { get; set; }
        public string PayData { get; set; }
        public string Estado { get; set; }
        public int? IdRanger { get; set; }
        public string Ranger { get; set; }
        public string Productos { get; set; }
        public int? CantidadProductos { get; set; }
        public decimal? GastosEnvio { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? PorcentajePropina { get; set; }
        public decimal? Propina { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Total { get; set; }
        public string Telefono { get; set; }
    }
}
