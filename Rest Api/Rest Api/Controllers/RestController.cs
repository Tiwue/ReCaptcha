using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rest_Api.Models;
using System.IO;

namespace Rest_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestController : ControllerBase
    {
        [HttpPost("Validar")]
        public async Task<IActionResult> validarReCaptcha(){

            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var str = await reader.ReadToEndAsync();
            JObject jsonEntrada = JObject.Parse(str);

            Recaptcha auxiliar = new Recaptcha();

            using (var client = new HttpClient()) 
            {
                String parametros = "secret=6LcqoU0kAAAAALRhxRDXBCtwGTc6E-ezB5uAQO0O&response=" + jsonEntrada["token"];
                var httpContent = new StringContent(parametros, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response2 = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", httpContent);

                if (response2.IsSuccessStatusCode)
                {
                    string respuesta = await response2.Content.ReadAsStringAsync();
                    auxiliar = JsonConvert.DeserializeObject<Recaptcha>(respuesta);
                    if (auxiliar.success) {
                        string stringRespuesta = "{'acceso': true}";
                        JObject jsonRespuesta = JObject.Parse(stringRespuesta);
                        return Ok(jsonRespuesta.ToString());
                    }else {
                        string stringRespuesta = "{'acceso': false}";
                        JObject jsonRespuesta = JObject.Parse(stringRespuesta);
                        return Ok(jsonRespuesta.ToString());
                    }

                    
                }

                string stringRespuestaDefault = "{'acceso': true}";
                JObject jsonRespuestaDefault = JObject.Parse(stringRespuestaDefault);
                return Ok(jsonRespuestaDefault.ToString());

            }
            
        }


        [HttpPost("Validar2")]
        public async Task<IActionResult> validarReCaptcha2()
        {

            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var str = await reader.ReadToEndAsync();
            JObject jsonEntrada = JObject.Parse(str);

            Recaptcha auxiliar = new Recaptcha();

            using (var client = new HttpClient())
            {
                String parametros = "secret=6LcqoU0kAAAAALRhxRDXBCtwGTc6E-ezB5uAQO0O&response=" + jsonEntrada["token"];
                var httpContent = new StringContent(parametros, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response2 = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", httpContent);

                if (response2.IsSuccessStatusCode)
                {
                    string respuesta = await response2.Content.ReadAsStringAsync();
                    auxiliar = JsonConvert.DeserializeObject<Recaptcha>(respuesta);
                    if (auxiliar.success)
                    {
                        string stringRespuesta = "{'acceso': true}";
                        JObject jsonRespuesta = JObject.Parse(stringRespuesta);
                        return Ok(jsonRespuesta.ToString());
                    }
                    else
                    {
                        string stringRespuesta = "{'acceso': false}";
                        JObject jsonRespuesta = JObject.Parse(stringRespuesta);
                        return Ok(jsonRespuesta.ToString());
                    }


                }

                string stringRespuestaDefault = "{'acceso': true}";
                JObject jsonRespuestaDefault = JObject.Parse(stringRespuestaDefault);
                return Ok(jsonRespuestaDefault.ToString());

            }

        }

    }
}
