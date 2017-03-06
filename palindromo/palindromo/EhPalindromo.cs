using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace palindromo
{
    public class Palindromo
    {
        public bool EhPalindromo(String frase)
        {

            String fraseFiltrada = frase.ToUpper().Replace(" ", "").Replace("-", "");

            for (int i = 0; i < fraseFiltrada.Length; i++)
            {
                // bug corrigido na linha abaixo!
                int outroLado = fraseFiltrada.Length - i - 1;
                if (fraseFiltrada[i] != fraseFiltrada[outroLado])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
    
