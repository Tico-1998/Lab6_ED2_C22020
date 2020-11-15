using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Permissions;
using System.Text;

namespace Cifrado_llave_publica
{
    public class GenerarLlaves
    {
        public void Calculos(ref int p,ref int q)
        {
            int n = p * q;
            int phi = (p - 1) * (q - 1);
            int e = ElegirE(phi);
            int d = EncontrarD(e, phi);
            string path = Environment.CurrentDirectory;
            path = path + "\\clave";
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            using (var file = new FileStream(path + "\\public.key",FileMode.Append))
            {
                using(var writer=new BinaryWriter(file))
                {
                    writer.Write(n + "," + e);
                }                
            }
            
            using (var file = new FileStream(path + "\\private.key", FileMode.Append))
            {
                using (var writer = new BinaryWriter(file))
                {
                    writer.Write(n + "," + d);
                }
            }
            string startPath = path;
            string zipPath = path+".zip";
            ZipFile.CreateFromDirectory(startPath, zipPath);



        }

        private int ElegirE(int phi)
        {
            int[] primos = new int[167];
            string path = "C:\\Users\\voca9\\Desktop\\Cifrado_llave_publica\\Cifrado_llave_publica\\primos.txt"; 
            //path = path + "\\primos.txt";
            var numeros = File.ReadAllText(path);
            string [] numero = numeros.Split(',');
            for (int i = 0; i < numero.Length; i++)
            {
                primos[i] = Convert.ToInt32(numero[i]);
            }
            bool buscando = true;
            int intento = 0;
            int e = 0;
            while (buscando)
            {
                e = primos[intento];
                if (intento <primos.Length)
                {
                    int factor = phi % e;
                    if (factor!=0&&e<phi)
                    {
                        buscando = false;
                    }
                    else
                    {
                        intento++;
                    }
                }
                else
                {
                    //lanzar mensaje de error al encontrar e 
                    buscando = false;
                }
            }
            return e;
        }
        private int EncontrarD(int e, int phi)
        {
            int eaux = 0;
            int phiaux = 0;
            int phi1 = phi;
            int phi2 = 1;
            int e1 = phi;
            int e2 = e;
            while (e2!=1)
            {
                eaux = e1 / e2;
                int multiplicacionphi = eaux * phi2;               
                int multiplicacione = e2 * eaux;
                phiaux = phi1;                
                eaux = e1;
                phi1 = phi2;
                e1 = e2;
                phi2 = phiaux - multiplicacionphi;
                e2 = eaux - multiplicacione;
                if (phi2 < 0)
                {
                    phi2 = phi2 % phi;
                }

            }
            int d = phi2;
            return d;
        }
    }
}
