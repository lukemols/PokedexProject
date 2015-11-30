using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokedexProject;

namespace PokedexProject
{
    static class Dex
    {
        public enum PokeState { NOTVIEWED = 0, VIEWED = 1, CAPTURED = 2 };

        static private GameClass.Game game;
        static public string Game
        {
            get { return GameClass.GetGameName(game); }
            set { try { game = GameClass.GetGameFromString(value); } catch { game = GameClass.Game.NOTDEFINED; } }
        }

        static private int generation;
        static public int Generation { get { return generation; } set { if (value > 0 && value < GenerationClass.GenerationLimit.Length) generation = value; } }

        static private string path;
        static public string Path { get { return path; } set { path = value; } }

        static private int pokenum;
        static public int Pokenum { get { return pokenum; } }

        static private List<PokeState> pokedex;
        static public List<PokeState> Pokedex { get { return pokedex; } }

        /// <summary>
        /// Metodo che crea un nuovo Dex a partire dalla generazione e dal gioco
        /// </summary>
        /// <param name="gen">Generazione</param>
        /// <param name="game">Gioco</param>
        static public void CreateNewDex(int gen, string game)
        {
            generation = gen;
            Dex.game = GameClass.GetGameFromString(game);
            path = "";
            CreateDexList();
        }

        /// <summary>
        /// Metodo che carica il dex a partire da un percorso
        /// </summary>
        /// <param name="path">Percorso dove si trova il file .dex</param>
        static public void LoadDex(string path)
        {
            Dex.path = path;
            OpenDex();
        }

        /// <summary>
        /// Metodo che crea la lista per un nuovo Dex 
        /// </summary>
        static void CreateDexList()
        {
            pokenum = GenerationClass.GenerationLimit[generation];
            pokedex = new List<PokeState>();
            if (pokedex.Count > 0)
                pokedex.Clear();
            for (int i = 0; i < pokenum; i++)
            {
                pokedex.Add(PokeState.NOTVIEWED);
            }
        }

        /// <summary>
        /// Metodo che apre il Dex
        /// </summary>
        static async void OpenDex()
        {
            pokedex = await FileManager.GetDexAsync(path);
            pokenum = pokedex.Count;
        }

        /// <summary>
        /// Metodo che ritorna lo stato del dex di un certo Pokémon
        /// </summary>
        /// <param name="index">Indice del Pokémon</param>
        /// <returns>Stato del Dex del Pokémon cercato</returns>
        static public PokeState GetState(int index)
        {
            if (index <= 0)
                index = 1;
            return Pokedex[index - 1];
        }

        /// <summary>
        /// Metodo che setta lo stato del Pokémon indicato
        /// </summary>
        /// <param name="index">Indice del Pokémon</param>
        /// <param name="state">Stato da assegnare</param>
        static public void SetState(int index, PokeState state)
        {
            if (index <= 0)
                index = 1;
            Pokedex[index - 1] = state;
        }
    }
}