using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    public class Evolution
    {
        int lower;
        public int Lower { get { return lower; } }

        int higher;
        public int Higher { get { return higher;} }

        string trigger;
        public string Trigger { get { return trigger; } }


        public Evolution()
        {
            lower = 0;
            higher = 0;
            trigger = "";
        }

        public Evolution(string s)
        {
            string[] arr = s.Split('|');
            try
            {
                if (arr[0] == "#evo")
                {   //Esempio:
                    //#evo|616|617|Scambio per Karrablast|
                    lower = System.Convert.ToInt32(arr[1]);
                    higher = System.Convert.ToInt32(arr[2]);
                    trigger = arr[3];
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
