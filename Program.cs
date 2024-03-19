using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using CASSAWS.Util;
using Newtonsoft.Json;
using CASSAWS.Models;
using System.Threading.Tasks;
using CASSAWS.Services;
using System.Linq;

namespace CASSAWS
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

           
            //Configuracion user y general
             configJson ac = new configJson();
             configUserModel d = ac.getConfig();
             resultUserTokenModel datauser = getUser<resultUserTokenModel>(d);

             //Data de SP
             AliadosComunidadVelocidadService AliadosComunidadVelocidadService = new AliadosComunidadVelocidadService();
             IList<AliadosComunidadVelocidadModel> datosAliadosVel = (await AliadosComunidadVelocidadService.getAliadosVel()).ToList();

             for (int i = 0, len = datosAliadosVel.Count(); i < len; i++)
             {
                 setDataToCASSA<resultResponseModel>(d, datauser, datosAliadosVel[i]);
             }
             
        }

        //Obtener los datos del usuario
        public static resultUserTokenModel getUser<T>(configUserModel d)
        {

            dynamic jsonResponse = null;
            var data = new
            {
                user = d.user,
                password = d.password
            };

            var settings = new JsonSerializerSettings { Converters = { new ReplacingStringWritingConverter("\n", "") } };
            var json = JsonConvert.SerializeObject(data, Formatting.None, settings);
            var djson = Encoding.ASCII.GetBytes(json);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebRequest tRequest = WebRequest.Create(d.urlRequest);
            tRequest.Method = d.method;
            tRequest.Timeout = d.timeout;
            tRequest.ContentType = d.contentType;
           // tRequest.Headers["Bearer"] = apiToken;
            tRequest.ContentLength = djson.Length;
            using (var stream = tRequest.GetRequestStream()) {
                stream.Write(djson, 0, djson.Length);
            }
            WebResponse response = (HttpWebResponse)tRequest.GetResponse();
            jsonResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var uResult = convertjson.Get <T>(jsonResponse);
            return uResult;
        }

        //Enviar la informacion a la base de CASSA
        public static void setDataToCASSA<T>(configUserModel d, resultUserTokenModel d2, AliadosComunidadVelocidadModel d3)
        {

            dynamic jsonResponse = null;
            var data = new
            {
               data = new {
                    FechaEvento = d3.fechaEvento,
                    Equipo = d3.equipo,
                    CodComunidad = d3.codComunidad,
                    Velocidad = d3.velocidad.ToString(),
                    Ubicacion = new {
                        Lat = d3.latitude,
                        Long = d3.longitude
                    }
               }
            };

            var settings = new JsonSerializerSettings { Converters = { new ReplacingStringWritingConverter("\n", "") } };
            var json = JsonConvert.SerializeObject(data, Formatting.None, settings);
            var djson = Encoding.ASCII.GetBytes(json);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebRequest tRequest = WebRequest.Create(d.urlRequest_setData);
            tRequest.Method = d.method;
            tRequest.Timeout = d.timeout;
            tRequest.ContentType = d.contentType;
            tRequest.Headers["Authorization"] = d2.token_type + " "+ d2.access_token;
            tRequest.ContentLength = djson.Length;
            using (var stream = tRequest.GetRequestStream())
            {
                stream.Write(djson, 0, djson.Length);
            }
            WebResponse response = (HttpWebResponse)tRequest.GetResponse();
            jsonResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var uResult = convertjson.Get<T>(jsonResponse);
            string estado = "";

            Console.ForegroundColor = ConsoleColor.White;
            if (uResult.error != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                estado = "Error - Mensaje: " + uResult.error;
            } else
            {
                estado = "Correcto - Mensaje: " + uResult.success;
            }
            Console.WriteLine("Estado: " + estado + " | " + "Equipo: " + d3.equipo + " Fecha Evento: "  + d3.fechaEvento);

        }
    }
}
