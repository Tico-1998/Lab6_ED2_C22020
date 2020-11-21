using System;
using Cifrado_llave_publica;

namespace Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerarLlaves generar = new GenerarLlaves();
            generar.Calculos(51, 63);
            string path = Console.ReadLine();
            RSA rsa = new RSA(path, path);
            rsa.cifrar (3233, 17, "intento");

        }
    }
}
