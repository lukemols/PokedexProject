﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    class PokemonPlaceClassManager
    {
        private List<PokemonPlace> placeList;
        public List<PokemonPlace> PlaceList { get { return placeList; } }

        //Istanza del singleton
        static private PokemonPlaceClassManager instance;
        static public PokemonPlaceClassManager Instance { get { if (instance == null) instance = new PokemonPlaceClassManager(); return instance; } }

        /// <summary>
        /// Costruttore privato
        /// </summary>
        private PokemonPlaceClassManager() { }


        /// <summary>
        /// Metodo che crea la lista delle zone dei Pokémon a partire da una lista di stringhe
        /// </summary>
        /// <param name="lines">Lista di stringhe contenenti le zone</param>
        public void GetPlaceList(List<string> lines)
        {
            placeList = new List<PokemonPlace>();
            
            while (lines.Count > 0)
            {
                try
                {
                    PokemonPlace p = new PokemonPlace(lines[0]);
                    placeList.Add(p);
                }
                catch (ProgramException) { }

                lines.RemoveAt(0);
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
            //if (ProgramManager.DexActive)
            //    gioco = ProgramManager.GetGameName();
            ProgramManager pm = ProgramManager.Instance;

            foreach (PokemonPlace p in placeList)
            {
                if (p.PokemonNumber == index)
                {
                    if (pm.DexActive && !p.Game.Contains(gioco))
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
            //if (ProgramManager.DexActive)
            //    gioco = ProgramManager.GetGameName();

            ProgramManager pm = ProgramManager.Instance;

            foreach (PokemonPlace p in placeList)
            {
                if (p.Region == region && p.LocationName == location)
                {
                    if (pm.DexActive && !p.Game.Contains(gioco))
                        continue;

                    places.Add(p);
                }
            }
            return places;
        }
    }
}
