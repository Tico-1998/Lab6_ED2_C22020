using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cifrado_llave_publica;

namespace api.Controllers
{
    [Route("api")]
    [ApiController]
    public class api : ControllerBase//revisar API
    {
        int n = 0;
        int f = 0;
        [HttpPost("rsa/{nombre}")]
        public ActionResult rsa([FromForm] IFormFile file, string nombre)
        {
            var pathactual = Environment.CurrentDirectory;
            Directory.CreateDirectory(pathactual + "\\temporal\\");
            ArchivoARuta(file);
            var ruta = pathactual + "\\temporal\\" + file.FileName;
            var server = pathactual + "\\resultado\\";
            RSA rSA = new RSA( ruta, server);
            var extencion = Path.GetExtension(file.FileName);
            var patharchivos = pathactual + "\\clave\\";
            if (extencion==".rsa")
            {

                Obtenerclaves(patharchivos + "private.key");               
            }
            else
            {

                Obtenerclaves(patharchivos + "public.key");
            }
            rSA.cifrarodescifrar(n, f, extencion, nombre);
            return Ok();
        }
        [HttpGet("rsa/keys/{p}/{q}")]
        public ActionResult llaves(int p, int q)
        {
            var pathactual = Environment.CurrentDirectory;
            Directory.CreateDirectory(pathactual + "\\temporal\\");
            var ruta = pathactual + "\\temporal\\";
            var server = pathactual + "\\resultado\\";
            GenerarLlaves generarLlaves = new GenerarLlaves();
            generarLlaves.Calculos(p, q);
            return Ok();//revisar return //retornar archivo 
        }


        private static void ArchivoARuta(IFormFile Archivo)
        {
            var resultado = new StringBuilder();
            using (var reader = new StreamReader(Archivo.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    resultado.AppendLine(reader.ReadLine());
            }
            resultado.ToString();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Environment.CurrentDirectory + "\\Temporal\\" + Archivo.FileName, true))
            {
                file.Write(resultado);
            }
        }
        private void Obtenerclaves( string nombre)
        {
            using (StreamReader sr = new StreamReader(nombre))
            {
                var datos = sr.ReadToEnd();
                string[] separando = datos.Split(',');
                separando[0] = separando[0].Trim();
                separando[1] = separando[1].Trim();
                n = Convert.ToInt32(separando[0]);
                f = Convert.ToInt32(separando[1]);
                
            }
            
               
                    
                           
        }
    }    
}
