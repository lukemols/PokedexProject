using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    public class Pokemon
    {
        public int numero;
        public string nome;
        public float peso;
        public float altezza;
        public string forma;

        public Pokemon()
        {
            numero = 0;
            nome = "";
            peso = 0.0f;
            altezza = 0.0f;
            forma = "Normale";
        }

        public Pokemon(string s)
        {
            string[] arr = s.Split('|');
            try
            {
                if (arr[0] == "#poke")
                {   //Esempio:
                    //0 #poke|1 6|2 Charizard|3 1,7|4 110,5|5 MX|
                    numero = System.Convert.ToInt32(arr[1]);
                    nome = arr[2];
                    altezza = System.Convert.ToSingle(arr[3]);
                    peso = System.Convert.ToSingle(arr[4]);
                    forma = arr[5];
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
