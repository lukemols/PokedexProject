using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    class PokemonPlace
    {
        //Numero, Luogo, ParteDelLuogo, Regione, Gioco, Modo, Livello, Orario, Percentuale
        int pokemonNumber;
        public int PokemonNumber { get { return pokemonNumber; } }

        string form;
        public string Form { get { return form; } }

        string locationName;
        public string LocationName { get { return locationName; } }

        string locationPart;
        public string LocationPart { get { return locationPart; } }

        string region;
        public string Region { get { return region; } }

        string game;
        public string Game { get { return game; } }

        string mode; //starter, amo, erba, scambio ecc
        public string Mode { get { return mode; } }

        string level;
        public string Level { get { return level; } }

        string probability;
        public string Probability { get { return probability; } }

        public PokemonPlace()
        {
            pokemonNumber = 0;
            form = "Normale";
            locationName = "";
            locationPart = "";
            region = "";
            game = "";
            mode = "";
            level = "";
            probability = "";
        }

        public PokemonPlace(string s)
        {
            string[] arr = s.Split('|');
            try
            {
                if (arr[0] == "#placePk")
                {
                    region = arr[1];
                    locationName = arr[2];
                    locationPart = arr[3];
                    game = arr[4];
                    pokemonNumber = System.Convert.ToInt32(arr[5]);
                    form = arr[6];
                    mode = arr[7];
                    level = arr[8];
                    probability = arr[9];
                }
                else
                    throw new ProgramException("Errore nella stringa del file.");
            }
            catch
            {
                throw new ProgramException("Errore nella creazione del PokemonPlace.");
            }
        }

        public override string ToString()
        {
            string s = "";
            if (nomeLuogo.Contains("Percorso"))
                s += "nel " + nomeLuogo;
            else
                s += "a " + nomeLuogo;

            if (level.Contains(',') || level.Contains('-'))
                s += " ai livelli " + level;
            else
                s += " al livello " + level;

            if (parteLuogo.Contains("Acqua"))
                s += " in " + parteLuogo + " con " + mode;
            else if (parteLuogo.Contains("Laboratorio"))
                s += " nel " + parteLuogo;
            else if (parteLuogo.Contains("Grotta"))
                s += " nella " + parteLuogo;
            else if (parteLuogo != "")
                s += " nella parte " + parteLuogo;

            if (mode.Contains("Erba"))
                s += " nell'erba";
            else if (mode.Contains("Fiori"))
                s += " nei " + mode;
            else if (!parteLuogo.Contains("Acqua"))
                s += " in " + mode;

           
            if (forma == "" || forma == "Normale")
                s += ",";
            else
                s += ", nella forma " + forma;

            s += " con probabilità " + probability;

            return s;
        }
    }
}
