using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PruebaFluid.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace PruebaFluid.Controllers
{
    public class HomeController : Controller
    {
        String api = "https://localhost:5001/api/Rest/";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       
        public ActionResult Index()
        {
            return View();
        }

        
        [Authorize]
        public ActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Gestiones()
        {
            return View();
        }

        public ActionResult CodigoAcceso() {
            return RedirectToAction("Gestiones");
        }

        [HttpPost]
        public async Task<ActionResult> Gestiones(string radioButtons, string response) {
            
            Response auxiliar = new Response();
            using (var cliente = new HttpClient()) {
                cliente.BaseAddress = new Uri(api);
                string token ="{ 'token' : '" +response +"'}";
                JObject json = JObject.Parse(token);
                StringContent contenido = new StringContent(
                JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json" );
                HttpResponseMessage respuesta = await cliente.PostAsync("Validar", contenido);
                if (respuesta.IsSuccessStatusCode) {
                    String mensaje = await respuesta.Content.ReadAsStringAsync();
                    auxiliar = JsonConvert.DeserializeObject<Response>(mensaje);
                }
            }

            if (auxiliar.acceso)
            {
                return View("~/Views/Home/CodigoAcceso.cshtml");
            }
            else {
                ViewData["Error"] = "Recaptcha rechazado";
            }
            
            return View();
        }
                
        

        [HttpPost]
        public async Task<ActionResult> CodigoAcceso(string ticket, string codigoSeguridad, string token)
        {
            Response auxiliar = new Response();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(api);
                string stringJson = "{ 'token' : '" + token + "'}";
                JObject json = JObject.Parse(stringJson);
                StringContent contenido = new StringContent(
                JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");
                HttpResponseMessage respuesta = await cliente.PostAsync("Validar2", contenido);
                if (respuesta.IsSuccessStatusCode)
                {
                    String mensaje = await respuesta.Content.ReadAsStringAsync();
                    auxiliar = JsonConvert.DeserializeObject<Response>(mensaje);
                }
            }

            if (auxiliar.acceso)
            {
                return View("Success");
            }
            else
            {
                ViewData["Error"] = "Recaptcha rechazado";
            }
            return View("~/Views/Home/CodigoAcceso.cshtml");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
