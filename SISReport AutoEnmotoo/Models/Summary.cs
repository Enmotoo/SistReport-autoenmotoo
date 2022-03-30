using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISReport_AutoEnmotoo.Models
{
    public class Summary
    {
        public double total { get; set; }
        public double subtotal { get; set; }
        public double delivery_price { get; set; }
        public double driver_tip { get; set; }
    }
}
