using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace SISReport_AutoEnmotoo.Controllers.Data
{
    public class ApiConnection
    {
        public ApiConnection() {}

        public string baseUrl()
        {
            string apiBaseUrl = "https://apiv4.ordering.co";
            string apiVersion = "v400";
            string language = "es";
            string project = "crediexpressv4";
            
            return apiBaseUrl + "/" + apiVersion + "/" + language + "/" + project + "/";
        }

        public string token()
        {
            string token = "iwawyG4gWqzCayitX2xzsB0lLu9yQo81OFAnYVakf8hOMdHM9CM6plXqrf2MbTSwa";

            return token;
        }
    }
}
