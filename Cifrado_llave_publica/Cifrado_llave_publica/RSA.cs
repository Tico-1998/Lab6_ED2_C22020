using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cifrado_llave_publica
{
    class RSA
    {
        private byte[] bufferescritura;
        private byte[] bufferlectura;
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
            using(var file=new FileStream(rutaarchivo,FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    while (reader.BaseStream.Position!=reader.BaseStream.Length)
                    {
                        bufferescritura = new byte[largobuffer];
                        bufferlectura = new byte[largobuffer];
                        bufferlectura = reader.ReadBytes(largobuffer);
                        posicionbuffer = 0;
                        for (int i = 0; i < bufferlectura.Length; i++)
                        {
                            bufferescritura[i] = Convert.ToByte(Math.Pow(bufferlectura[i], e) % n);
                        }
                        Escribir();
                    }
                }
            }
        }
        private void Escribir()
        {
            using (var file = new FileStream(rutaserver + nombre, FileMode.Append))
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
