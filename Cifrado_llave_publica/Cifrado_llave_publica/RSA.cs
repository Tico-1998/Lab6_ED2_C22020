﻿using System;
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

        public RSA( string rutaArchivo, string rutaServer)//Prueba
        {            
            rutaarchivo = rutaArchivo;
            rutaserver = rutaServer;
            posicionbuffer = 0;
        }
        public void cifrarodescifrar(int n, int e, string extension, string nombre)
        {
            if (extension == ".txt")
            {
                extensionfinal = ".rsa";
            }
            else
            {
                extensionfinal = ".txt";
            }
            var buffer = new char[largobuffer];
            var bufferescritura = new char[largobuffer];
            using (var file = new FileStream(rutaarchivo, FileMode.Open))
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
                        Escribir(bufferescritura, nombre);
                    }
                }
            }
        }
        private void Escribir(char [] bufferescritura, string nombre) 
        {
            using (var file = new FileStream(rutaserver + nombre+extensionfinal, FileMode.Append))//Probar extensión
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
    }
}
