using Microsoft.AspNetCore.Mvc;
using SISReport_AutoEnmotoo.Controllers.Data;
using SISReport_AutoEnmotoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using SISReport_AutoEnmotoo.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace SISReport_AutoEnmotoo.Controllers
{
    public class ReportController : Controller
    {
        private static ApiConnection apic = new ApiConnection();
        private string baseUrl = apic.baseUrl();
        private ApiConsultResult _report;
        private List<Order> orders;

        private readonly OrderingAsLocalContext _context;

        public ReportController(OrderingAsLocalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            _report = await getAllData(0);
            orders = await _context.Orders.ToListAsync();

            return PartialView("_MainReport", orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDataTerminado()
        {
            //_report = await getAllData();
            orders = await _context.Orders.ToListAsync();

            return PartialView("_MainReport", orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDataCancelado()
        {
            //_report = await getAllData();
            orders = await _context.Orders.ToListAsync();

            return PartialView("_MainReportCancel", orders);
        }

        [HttpGet]
        [Route("GetConsolidate/{dateinit}/{dateend}")]
        public async Task<IActionResult> GetConsolidate(string dateinit, string dateend)
        {
            _report = await getAllData(0);
            List<SpGetConsolidate> res = _context.SpConsolidate
                .FromSqlInterpolated($"EXEC GETCONSILIDATE @DATEINIT={dateinit}, @DATEEND={dateend}").ToList();
            return PartialView("_Consolidate", res);    
        }

        public IActionResult Consolidado()
        {
            return View();
        }

        public async Task<ApiConsultResult> getAllData(int size)
        {
            try
            {
                var oft = await _context.Orders.ToListAsync();
                
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(baseUrl);

                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);

                httpClient.DefaultRequestHeaders.Add("x-api-key", apic.token());
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                jsonSettings.Error = (serializer, err) =>
                {
                    err.ErrorContext.Handled = true;
                };
                var respuesta = httpClient.GetAsync("orders?offset=" + size + "&mode=dashboard").Result;
                var custom = JsonConvert.DeserializeObject<ApiConsultResult>(respuesta.Content.ReadAsStringAsync().Result, jsonSettings);


                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://cobresp.com/globalapi.enmotoo.com/CustomFactura/");

                var r = hc.GetAsync("index.php").Result;
                var c = JsonConvert.DeserializeObject<ApiInoviceConsultResult[]>(r.Content.ReadAsStringAsync().Result);
                ApiInoviceConsultResult s;


                HttpClient hcd = new HttpClient();
                hcd.BaseAddress = new Uri("https://cobresp.com/globalapi.enmotoo.com/CustomCommissions/");

                var rd = hcd.GetAsync("index.php").Result;
                var cd = JsonConvert.DeserializeObject<Commissions[]>(rd.Content.ReadAsStringAsync().Result);
                Commissions sd;

                DateTime d;
                DateTime auxd = DateTime.Parse("25/11/2021");

                var nameM = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                var status = new string[] { "Pendiente", "Terminado", "Rechazado", "Conductor llegó a negocio", "Listo para recoger", "Rechazado por los negocios", "Rechazado por el conductor",
                    "Aceptado por negocio", "Aceptado por conductor", "Recoger completado por el conductor", "No se ha podido recoger la orden por el conductor",
                    "Entrega completada por el conductor", "Error de entrega por el conductor" };
                Consolidate con;
                List<Consolidate> consolidados = new List<Consolidate>();

                foreach (var item in custom.result)
                {
                    //DateTime d = DateTime.Parse(item.delivery_datetime);
                    //DateTime e = DateTime.Parse("25/11/2021");
                    //int s = DateTime.Compare(DateTime.Parse(item.delivery_datetime), DateTime.Parse("23/11/2021"));
                    s = c.Where(e => e.npedido == item.id.ToString()).FirstOrDefault();
                    sd = cd.Where(e => e.businessId == item.business_id.ToString()).FirstOrDefault();

                    if (s != null)
                    {
                        item.customInvoice = s.nfactura;
                    }
                    if (sd != null)
                    {
                        item.commission = sd.commission;
                    }

                    if (item.status != 1)
                    {
                        if (item.status != 11)
                        {
                            continue;
                        }
                    }

                    d = DateTime.Parse(item.delivery_datetime);

                    con = new Consolidate();
                    con.Date = nameM[d.Month - 1] + " " + d.Year;
                    con.consolidated = item.summary.subtotal;
                    con.negocio = item.business.name;
                    con.consolidatedEnmotoo = Convert.ToDouble(item.commission) * Math.Round(item.summary.subtotal, 2);

                    consolidados.Add(con);
                }

                var lsd = consolidados.GroupBy(x => x.negocio);
                var sdf = lsd.Select(e => e.Key);

                ArrayList columns = new ArrayList();
                ArrayList rows = new ArrayList();
                ArrayList dates = new ArrayList();
                bool allDates = false;

                foreach (var item in sdf)
                {
                    var l = consolidados.GroupBy(x => x.Date);
                    columns.Add(item);
                    foreach (var le in l)
                    {
                        if (!allDates) dates.Add(le.Key);
                        var dsd = Math.Round(consolidados.Where(e => e.negocio == item && e.Date == le.Key).ToList().Select(x => x.consolidated).Sum(), 2);
                        var dsdEnmotoo = Math.Round(consolidados.Where(e => e.negocio == item && e.Date == le.Key).ToList().Select(x => x.consolidatedEnmotoo).Sum(), 2);
                        columns.Add(dsd);
                        columns.Add(dsdEnmotoo);
                    }
                    allDates = true;

                    rows.Add(columns);
                    columns = new ArrayList();
                }

                ViewBag.dates = dates;
                ViewBag.rows = rows;


                _report = custom;
                size += _report.result.Count;
                if (_report.result.Count <= 0)
                {
                    return null;
                }

                Order order = new Order();
                foreach (Report rpt in _report.result)
                {
                    var sdfg = oft.Where(o => o.OrderId == rpt.id).FirstOrDefault();
                
                    order.Oid = rpt.id;
                    order.OrderId = rpt.id;
                    order.DeliveryType = rpt.delivery_type == 1 ? "Entrega" : "Recoger";
                    order.Factura = rpt.customInvoice;
                    order.Fecha = rpt.delivery_datetime.Split(" ")[0];
                    order.Hora = rpt.delivery_datetime.Split(" ")[1];
                    order.IdNegocio = rpt.business_id;
                    order.Negocio = rpt.business.name == "American donust Ciudad Jardin" ? "American donuts Ciudad Jardin" : rpt.business.name == "American donust carretera Masaya" ? "American donuts carretera Masaya" : rpt.business.name;
                    order.PorcentajeComision = Convert.ToDecimal(rpt.commission) * 100;
                    order.Comision = Convert.ToDecimal(Convert.ToDouble(rpt.commission) * Math.Round(rpt.summary.subtotal, 2));
                    order.IdCliente = rpt.customer_id;
                    order.NombreCliente = rpt.customer.name + " " + rpt.customer.lastname;
                    order.CorreoCliente = rpt.customer.email;
                    order.MetodoPago = rpt.paymethod.name == "Cash" ? "Efectivo" : "Tarjeta de crédito";
                    order.PayData = rpt.pay_data;
                    order.Estado = status[rpt.status];
                    order.IdRanger = rpt.driver != null ? rpt.driver.id : null;
                    order.Ranger = rpt.driver != null ? rpt.driver.name : null;
                    order.Productos = Newtonsoft.Json.JsonConvert.SerializeObject(rpt.products);

                    int quantity = 0;
                    foreach (Products p in rpt.products)
                    {
                        quantity += p.quantity;
                    }

                    order.CantidadProductos = quantity;
                    order.GastosEnvio = Convert.ToDecimal(rpt.summary.delivery_price);
                    order.Descuento = Convert.ToDecimal(rpt.discount);
                    order.PorcentajePropina = Convert.ToDecimal(rpt.driver_tip);
                    order.Propina = Convert.ToDecimal(rpt.summary.driver_tip);
                    order.Subtotal = Convert.ToDecimal(rpt.summary.subtotal);
                    order.Total = Convert.ToDecimal(rpt.summary.total);
                    
                    if (sdfg != null)
                    {
                        if (sdfg.Estado != order.Estado)
                        {
                            sdfg.Estado = order.Estado;
                            _context.Orders.Update(sdfg);
                            await _context.SaveChangesAsync();
                        }

                        //if (sdfg.PorcentajeComision != order.PorcentajeComision)
                        //{
                        //    sdfg.PorcentajeComision = order.PorcentajeComision;
                        //    _context.Orders.Update(sdfg);
                        //    await _context.SaveChangesAsync();
                        //}
                    }
                    else
                    {
                        if (ModelState.IsValid)
                        {
                            _context.Add(order);
                            await _context.SaveChangesAsync();
                        }
                    }
                    
                }

                return await getAllData(size);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR " + ex.Message);
                return null;
            }
        }

    }
}
