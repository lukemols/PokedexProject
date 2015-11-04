using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    public class Evolution
    {
        public int numero1;
        public int numero2;
        public string modo;


        public Evolution()
        {
            numero1 = 0;
            numero2 = 0;
            modo = "";
        }

        public Evolution(string s)
        {
            string[] arr = s.Split('|');
            try
            {
                if (arr[0] == "#evo")
                {   //Esempio:
                    //#evo|616|617|Scambio per Karrablast|
                    numero1 = System.Convert.ToInt32(arr[1]);
                    numero2 = System.Convert.ToInt32(arr[2]);
                    modo = arr[3];
                }
                else
                    throw new ProgramException("Errore nella stringa del file.");
            }
            catch
            {
                throw new ProgramException("Errore nella creazione dell'evoluzione.");
            }
        }

    }
}
