using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISReport_AutoEnmotoo.Models
{
    public class ApiConsultResult
    {
        public bool error { get; set; }
        public List<Report> result { get; set; }
    }
}
