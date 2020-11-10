using System;
using System.Collections.Generic;
using System.Text;

namespace Cifrado_llave_publica
{
    class GenerarLlaves
    {
        public void Calculos(int p, int q)
        {
            int n = p * q;
            int phi = (p - 1) * (q - 1);

        }

        public void ElegirE (int phi)
        {
            //1<e<phi
            //no compratir factores con phi 
            //que sea primo
        }
    }
}
