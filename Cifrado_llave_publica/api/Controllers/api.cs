using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cifrado_llave_publica;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class api : ControllerBase
    {
        [HttpPost("rsa/{nombre}")]
        public ActionResult rsa([FromForm] IFormFile file, [FromForm]IFormFile llaves, string nombre)
        {
            var pathactual = Environment.CurrentDirectory;
            Directory.CreateDirectory(pathactual + "\\temporal\\");
            ArchivoARuta(file);
            var ruta = pathactual + "\\temporal\\" + nombre;
            var server = pathactual + "\\resultado\\";
            RSA rSA = new RSA( ruta, server);
            var extencion = Path.GetExtension(file.FileName);
            //obtener claves
            int n = 0;
            int e = 0;
            rSA.cifrarodescifrar(n, e, extencion, nombre);
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
            return Ok();//revisar return
        }
        public static void ArchivoARuta(IFormFile Archivo)
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
    }
}
