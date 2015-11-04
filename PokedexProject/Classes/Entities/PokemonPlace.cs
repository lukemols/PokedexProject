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
        public int numeroPk;
        public string forma;
        public string nomeLuogo;
        public string parteLuogo;
        public string regione;
        public string gioco;
        public string modo; //starter, amo, erba, scambio ecc
        public string livello;
        public string percentuale;

        public PokemonPlace()
        {
            numeroPk = 0;
            forma = "Normale";
            nomeLuogo = "";
            parteLuogo = "";
            regione = "";
            gioco = "";
            modo = "";
            livello = "";
            percentuale = "";
        }

        public PokemonPlace(string s)
        {
            string[] arr = s.Split('|');
            try
            {
                if (arr[0] == "#placePk")
                {
                    regione = arr[1];
                    nomeLuogo = arr[2];
                    parteLuogo = arr[3];
                    gioco = arr[4];
                    numeroPk = System.Convert.ToInt32(arr[5]);
                    forma = arr[6];
                    modo = arr[7];
                    livello = arr[8];
                    percentuale = arr[9];
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

            if (livello.Contains(',') || livello.Contains('-'))
                s += " ai livelli " + livello;
            else
                s += " al livello " + livello;

            if (parteLuogo.Contains("Acqua"))
                s += " in " + parteLuogo + " con " + modo;
            else if (parteLuogo.Contains("Laboratorio"))
                s += " nel " + parteLuogo;
            else if (parteLuogo.Contains("Grotta"))
                s += " nella " + parteLuogo;
            else if (parteLuogo != "")
                s += " nella parte " + parteLuogo;

            if (modo.Contains("Erba"))
                s += " nell'erba";
            else if (modo.Contains("Fiori"))
                s += " nei " + modo;
            else if (!parteLuogo.Contains("Acqua"))
                s += " in " + modo;

           
            if (forma == "" || forma == "Normale")
                s += ",";
            else
                s += ", nella forma " + forma;

            s += " con probabilità " + percentuale;

            return s;
        }
    }
}
