using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    public class PokemonClassManager
    {
        ObservableCollection<Pokemon> pokemonList;
        public ObservableCollection<Pokemon> PokemonList { get { return pokemonList; } }

        //Istanza del singleton
        static private PokemonClassManager instance;
        static public PokemonClassManager Instance { get { if (instance == null) instance = new PokemonClassManager(); return instance; } }

        /// <summary>
        /// Costruttore privato
        /// </summary>
        private PokemonClassManager() { }

        /// <summary>
        /// Metodo che crea la lista di Pokémon a partire da una lista di stringhe
        /// </summary>
        /// <param name="lines">Lista di stringhe contenente i Pokémon</param>
        public void GetPokemonList(List<string> lines)
        {
            pokemonList = new ObservableCollection<Pokemon>();
            GenerationClass gen = GenerationClass.Instance;

            while (lines.Count > 0)
            {
                try
                {
                    Pokemon p = new Pokemon(lines[0]);
                    if (p.Number > gen.GenerationLimit[gen.ActualGeneration])
                        break;
                    pokemonList.Add(p);
                }
                catch (ProgramException) { }
                lines.RemoveAt(0);
            }
        }

        /// <summary>
        /// Metodo che ritorna il Pokémon con il numero di dex indicato. Il numero di dex può essere del dex Nazionale o Regionale
        /// </summary>
        /// <param name="index">Numero del Pokémon</param>
        /// <param name="regionNumber">Indica se si sta cercando il numero di dex regionale. Di default è false</param>
        /// <returns>Pokémon cercato</returns>
        public Pokemon GetPokemon(int index, bool regionNumber = false)
        {
            Pokemon poke = new Pokemon();
            foreach (Pokemon p in pokemonList)
            {
                if ((!regionNumber && p.Number == index) || (regionNumber && p.RegionalNumber == index))
                {
                    poke = p;
                    break;
                }
            }
            return poke;
        }

        /// <summary>
        /// Metodo che ritorna le forme del Pokémon con il numero di dex indicato. Il numero di dex può essere del dex Nazionale o Regionale
        /// </summary>
        /// <param name="index">Numero del Pokémon</param>
        /// <param name="regionNumber">Indica se si sta cercando il numero di dex regionale. Di default è false</param>
        /// <returns>Forme del Pokémon cercato</returns>
        public List<Pokemon> GetPokeForms(int index, bool regionNumber = false)
        {
            List<Pokemon> poke = new List<Pokemon>();
            foreach (Pokemon p in pokemonList)
            {
                if ((!regionNumber && p.Number == index) || (regionNumber && p.RegionalNumber == index))
                {
                    poke.Add(p);
                }
            }
            return poke;
        }
    }
}
