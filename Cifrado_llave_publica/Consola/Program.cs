using System;
using Cifrado_llave_publica;

namespace Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Console.ReadLine();            
            RSA rsa = new RSA(path,path);
            rsa.cifrarodescifrar(3233, 2753,".txt", "intento");

        }
    }
}
