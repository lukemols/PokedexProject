using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    public class Tipo
    {
        public int numero;
        public string forma;
        public string tipo1;
        public string tipo2;
        public int dagen;

        public Tipo()
        {
            numero = 0;
            dagen = 0;
            forma = "";
            tipo1 = "";
            tipo2 = "";
        }

        public Tipo(string s)
        {
            string[] arr = s.Split('|');
            try
            {
                if (arr[0] == "#type")
                {   //Esempio:
                    //#type|3||Erba|Veleno|1|
                    numero = System.Convert.ToInt32(arr[1]);
                    forma = arr[2];
                    tipo1 = arr[3];
                    tipo2 = arr[4];
                    dagen = System.Convert.ToInt32(arr[5]);
                }
                else
                    throw new ProgramException("Errore nella stringa del file.");
            }
            catch
            {
                throw new ProgramException("Errore nella creazione del tipo");
            }
        }
        public override string ToString()
        {
            string s;
            s = "( " + numero + ", '" + forma + "', '" + tipo1 + "', '" + tipo2 + "', " + dagen + ")," + Environment.NewLine;
            s = "#type|" + numero + "|" + forma + "|" + tipo1 + "|" + tipo2 + "|" + dagen + "|" + Environment.NewLine;
            return s;
        }
    }
}
