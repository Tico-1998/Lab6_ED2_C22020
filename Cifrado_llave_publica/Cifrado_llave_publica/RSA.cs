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
        private string nombre;

        public RSA(string nombrearchivo, string rutaArchivo, string rutaServer)//probar
        {
            nombre = nombrearchivo;
            rutaarchivo = rutaArchivo;
            rutaserver = rutaServer;
            posicionbuffer = 0;
        }
        public void cifrarodescifrar(int n, int e)
        {
            var buffer = new char[largobuffer];
            var bufferescritura = new char[largobuffer];
            using (var file=new FileStream(rutaarchivo,FileMode.Open))
            {
                using (var reader = new StreamReader(file))
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
                            posicionbuffer++;
                        }
                        Escribir(bufferescritura);
                    }
                }
            }
        }
        private void Escribir(char [] bufferescritura) 
        {
            using (var file = new FileStream(rutaserver + nombre, FileMode.Append))
            {
                using (var writer = new StreamWriter(file))
                {
                    for (int i = 0; i < bufferescritura.Length; i++)
                    {
                        writer.Write(bufferescritura[i]);
                    }
                }
            }
        }
        /*public void Descifrar(int d, int n)
        {
            using (var file = new FileStream(rutaarchivo, FileMode.Open))
            {
                using (var reader=new BinaryReader(file))
                {
                    while (reader.BaseStream.Position!=reader.BaseStream.Length)
                    {
                        bufferescritura = new byte[largobuffer];
                        bufferlectura = reader.ReadBytes(largobuffer);
                        posicionbuffer = 0;
                        for (int i = 0; i < bufferlectura.Length; i++)
                        {
                            bufferescritura[i] = Convert.ToByte(Math.Pow(bufferlectura[i], d) % n);
                        }
                        Escribir();
                    }
                }
            }
        }*/
    }
}
