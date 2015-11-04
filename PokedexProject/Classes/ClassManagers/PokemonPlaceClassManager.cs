using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    class PokemonPlaceClassManager
    {
        ProgramManager pm;
        FileManager fm;
        private static List<PokemonPlace> listaZone;
        public List<PokemonPlace> ListaZone { get { return listaZone; } }

        public PokemonPlaceClassManager()
        {
            fm = new FileManager();
            pm = new ProgramManager();
            if (listaZone == null)
            {
                GetPlaceList();
            }
        }

        public void GetPlaceList()
        {
            List<string> lines;
            listaZone = new List<PokemonPlace>();

            try
            {
                lines = fm.ReadFile(@"Places.data");
            }
            catch
            {
                return;
            }

            while (lines.Count > 0)
            {
                try
                {
                    PokemonPlace p = new PokemonPlace(lines[0]);
                    listaZone.Add(p);
                    lines.RemoveAt(0);
                }
                catch (ProgramException)
                {
                    lines.RemoveAt(0);
                    continue;
                }
            }
        }
        /// <summary>
        /// Metodo che ritorna la lista dei luoghi in cui è possibile trovare il Pokémon specificato
        /// </summary>
        /// <param name="index">Numero di indice del Pokémon da cercare</param>
        /// <returns>Lista dei luoghi</returns>
        public List<PokemonPlace> GetPlacesOfPokemon(int index)
        {
            List<PokemonPlace> places = new List<PokemonPlace>();
            string gioco = "";
            if (pm.DexActive)
                gioco = pm.GetGameName();

            foreach (PokemonPlace p in listaZone)
            {
                if (p.numeroPk == index)
                {
                    if (pm.DexActive && !p.gioco.Contains(gioco))
                        continue;
                    places.Add(p);
                }
            }
            return places;
        }
        /// <summary>
        /// Metodo che ritorna la lista dei Pokémon nel luogo specificato
        /// </summary>
        /// <param name="location">Luogo</param>
        /// <param name="region">Regione del luogo</param>
        /// <returns>Lista dei Pokémon che vi si trovano</returns>
        public List<PokemonPlace> GetPokemonInPlace(string location, string region)
        {
            List<PokemonPlace> places = new List<PokemonPlace>();
            string gioco = "";
            if (pm.DexActive)
                gioco = pm.GetGameName();
            foreach (PokemonPlace p in listaZone)
            {
                if (p.regione == region && p.nomeLuogo == location)
                {
                    if (pm.DexActive && !p.gioco.Contains(gioco))
                        continue;

                    places.Add(p);
                }
            }
            return places;
        }
    }
}
