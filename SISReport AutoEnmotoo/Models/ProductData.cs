using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISReport_AutoEnmotoo.Models
{
    public class ProductData
    {
        public int id { get; set; }
        //public int product_id { get; set; }
        //public int order_id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double total { get; set; }
        public int quantity {get; set; }
        //public string comment { get; set; }
        public List<Options> options { get; set; }
    }
}
