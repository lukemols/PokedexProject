using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    public class Pokemon
    {
        int number;
        public int Number { get { return number; } }

        int regionalNumber;
        public int RegionalNumber { get { return regionalNumber; } }

        string name;
        public string Name { get { return name; } }

        float weight;
        public float Weight { get { return weight; } }

        float height;
        public float Height { get { return height; } }

        string form;
        public string Form { get { return form; } }

        public Pokemon()
        {
            number = 0;
            name = "";
            weight = 0.0f;
            height = 0.0f;
            form = "Normale";
        }

        public Pokemon(string s)
        {
            string[] arr = s.Split('|');
            try
            {
                if (arr[0] == "#poke")
                {   //Esempio:
                    //0 #poke|1 6|2 Charizard|3 1,7|4 110,5|5 MX|
                    number = System.Convert.ToInt32(arr[1]);
                    name = arr[2];
                    height = System.Convert.ToSingle(arr[3]);
                    weight = System.Convert.ToSingle(arr[4]);
                    form = arr[5];
                }
                else
                    throw new ProgramException("Errore nella stringa del file.");
            }
            catch
            {
                throw new ProgramException("Errore nella creazione del Pokemon.");
            }
            
        }
    }
}
