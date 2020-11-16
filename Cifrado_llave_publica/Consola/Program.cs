using System;
using Cifrado_llave_publica;

namespace Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Console.ReadLine();            
            string nombre = "prueba0";
            RSA rsa = new RSA(nombre,path,path);
            rsa.cifrarodescifrar(3233, 2753);

        }
    }
}
