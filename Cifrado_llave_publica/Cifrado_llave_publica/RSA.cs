using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace Cifrado_llave_publica
{
    public class RSA
    {                
        private int largobuffer = 150;        
        private int posicionbuffer;
        private string rutaarchivo;
        private string rutaserver;
        private string extensionfinal;

        public RSA( string rutaArchivo, string rutaServer)//probar
        {            
            rutaarchivo = rutaArchivo;
            rutaserver = rutaServer;
            posicionbuffer = 0;
        }
        public void cifrar(int n, int e, string nombre)
        {
            
            var buffer = new char[largobuffer];
            var bufferescritura = new char[largobuffer];
            using (var file = new FileStream(rutaarchivo, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {                    
                    while (reader.BaseStream.Position!=reader.BaseStream.Length)
                    {                        
                        reader.Read(buffer,0,largobuffer);
                        posicionbuffer = 0;
                        foreach (var caracter in buffer)
                        {
                            var caracterByte = (int)caracter;
                            var caractercifrado = BigInteger.ModPow(caracter, e, n);
                            bufferescritura[posicionbuffer] = (char)caractercifrado;
                            //cuantos bytes cabe n
                            posicionbuffer++;
                        }
                        nombre = nombre + ".txt";
                        //primera vez cuantos bytes 
                        Escribir(bufferescritura, nombre);
                    }
                    
                }
            }
        }
        private void Escribir(char [] bufferescritura, string nombre) 
        {
            using (var file = new FileStream(rutaserver + nombre+extensionfinal, FileMode.Append))//probar extension
            {
                using (var writer = new BinaryWriter(file))
                {
                    for (int i = 0; i < bufferescritura.Length; i++)
                    {
                        writer.Write(bufferescritura[i]);
                    }
                }
            }
        }
        public void Descifrar(int d, int n, string nombre)
        {
            var buffer = new char[largobuffer];
            var bufferescritura = new char[largobuffer];
            using (var file = new FileStream(rutaarchivo, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        reader.Read(buffer, 0, largobuffer);
                        posicionbuffer = 0;
                        //primer byte agrupar en esa cantidad
                        foreach (var caracter in buffer)
                        {
                            var caracterByte = (int)caracter;
                            var caractercifrado = BigInteger.ModPow(caracter, d, n);
                            bufferescritura[posicionbuffer] = (char)caractercifrado;
                            posicionbuffer++;
                        }
                        nombre = nombre + ".rsa";
                        Escribir(bufferescritura, nombre);
                    }
                }
            }
        }
    }
}
