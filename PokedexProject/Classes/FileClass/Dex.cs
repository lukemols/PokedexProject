using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokedexProject;

namespace PokedexProject
{
    class Dex
    {
        //Istanza del singleton
        static private Dex instance;
        static public Dex Instance { get { if (instance == null) instance = new Dex(); return instance; } }

        public enum PokeState { NOTVIEWED = 0, VIEWED = 1, CAPTURED = 2 };

        private GameClass.Game game;
        public string Game
        {
            get { return GameClass.Instance.GetGameName(game); }
            set { try { game = GameClass.Instance.GetGameFromString(value); } catch { game = GameClass.Game.NOTDEFINED; } }
        }

        private int generation;
        public int Generation { get { return generation; } set { if (value > 0 && value < GenerationClass.Instance.GenerationLimit.Length) generation = value; } }

        private string path;
        public string Path { get { return path; } set { path = value; } }

        private int pokenum;
        public int Pokenum { get { return pokenum; } }

        private List<PokeState> pokedex;
        public List<PokeState> Pokedex { get { return pokedex; } }

        string playerName;
        public string PlayerName { get; set; }

        int ao;
        public int AO { get { return AO; } set { if (value < 10000 && value >= 0) ao = value; } }


        /// <summary>
        /// Costruttore privato
        /// </summary>
        private Dex() { }

        /// <summary>
        /// Metodo che crea un nuovo Dex a partire dalla generazione e dal gioco
        /// </summary>
        /// <param name="gen">Generazione</param>
        /// <param name="game">Gioco</param>
        /// <param name="name">Nome del giocatore, di default nullo</param>
        /// <param name="ao">ID del giocatore, di default zero</param>
        public void CreateNewDex(int gen, string game, string name = "", int ao = 0)
        {
            generation = gen;
            this.game = GameClass.Instance.GetGameFromString(game);
            path = "";
            playerName = name;
            this.ao = ao;
            CreateDexList();
        }

        /// <summary>
        /// Metodo che carica il dex a partire da un percorso
        /// </summary>
        /// <param name="path">Percorso dove si trova il file .dex</param>
        public void LoadDex(string path)
        {
            this.path = path;
            OpenDex();
        }

        /// <summary>
        /// Metodo che crea la lista per un nuovo Dex 
        /// </summary>
        void CreateDexList()
        {
            pokenum = GenerationClass.Instance.GenerationLimit[generation];
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
        async void OpenDex()
        {
            FileManager fm = FileManager.Instance;
            pokedex = await fm.GetDexAsync(path);
            pokenum = pokedex.Count;
        }

        /// <summary>
        /// Metodo che ritorna lo stato del dex di un certo Pokémon
        /// </summary>
        /// <param name="index">Indice del Pokémon</param>
        /// <returns>Stato del Dex del Pokémon cercato</returns>
        public PokeState GetState(int index)
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
        public void SetState(int index, PokeState state)
        {
            if (index <= 0)
                index = 1;
            Pokedex[index - 1] = state;
        }
    }
}