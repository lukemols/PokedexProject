using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    public class Type
    {
        int pokemonNumber;
        public int PokemonNumber { get { return pokemonNumber; } }

        public string form;
        public string Form { get { return form; } }

        string type1;
        public string Type1 { get { return type1; } }

        string type2;
        public string Type2 { get { return type2; } }

        int fromGeneration;
        public int FromGeneration { get { return fromGeneration; } }

        public Type()
        {
            pokemonNumber = 0;
            fromGeneration = 0;
            form = "";
            type1 = "";
            type2 = "";
        }

        public Type(string s)
        {
            string[] arr = s.Split('|');
            try
            {
                if (arr[0] == "#type")
                {   //Esempio:
                    //#type|3||Erba|Veleno|1|
                    pokemonNumber = System.Convert.ToInt32(arr[1]);
                    form = arr[2];
                    type1 = arr[3];
                    type2 = arr[4];
                    fromGeneration = System.Convert.ToInt32(arr[5]);
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
            s = "( " + pokemonNumber + ", '" + form + "', '" + type1 + "', '" + type2 + "', " + fromGeneration + ")," + Environment.NewLine;
            s = "#type|" + pokemonNumber + "|" + form + "|" + type1 + "|" + type2 + "|" + fromGeneration + "|" + Environment.NewLine;
            return s;
        }
    }
}
