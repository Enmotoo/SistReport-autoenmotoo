using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISReport_AutoEnmotoo.Models
{
    public class Paymethod
    {
        public int id { get; set; }
        public string name { get; set; }
        public string gateway {get; set;}
        public bool enabled {get; set; }
        public string deleted_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
