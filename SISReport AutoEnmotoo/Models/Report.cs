using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISReport_AutoEnmotoo.Models
{
    public class Report
    {
        public int id { get; set; }
        public int delivery_type { get; set; }
        public int business_id { get; set; }
        public int customer_id { get; set; }
        public double tax { get; set; }
        public double service_fee { get; set; }
        public string delivery_datetime { get; set; }
        public int status { get; set; }
        public double delivery_zone_price { get; set; }
        public double discount { get; set; }
        public double driver_tip { get; set; }
        public Paymethod paymethod { get; set; }
        public string pay_data { get; set; }
        public List<ProductData> productdata { get; set; }
        public List<Products> products { get; set; }
        public Driver driver { get; set; }
        public Business business { get; set; }
        public Customer customer { get; set; }
        public Summary summary { get; set; }
        public string customInvoice { get; set; }
        public string commission { get; set; }
    }
}
