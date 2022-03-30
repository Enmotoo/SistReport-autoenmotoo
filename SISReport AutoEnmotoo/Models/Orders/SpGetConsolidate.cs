using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISReport_AutoEnmotoo.Models.Orders
{
    public partial class SpGetConsolidate
    {
        public int ID_NEGOCIO { get; set; }
        public string NEGOCIO { get; set; }
        public int? NPEDIDO { get; set; }
        public decimal? VENTAS { get; set;}
        public decimal? COMISION { get; set; }
        public decimal? GASTOSENVIO { get; set; }
    }
}
