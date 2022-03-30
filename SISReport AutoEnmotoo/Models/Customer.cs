using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISReport_AutoEnmotoo.Models
{
    public class Customer
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
    }
}
