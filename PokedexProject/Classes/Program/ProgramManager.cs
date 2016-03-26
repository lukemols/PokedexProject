using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;

namespace PokedexProject
{
    public class ProgramManager
    {
        bool dexActive;
        public bool DexActive { get { return dexActive; } }

        bool regionActive;
        public bool RegionActive { get { return regionActive; } }

        bool isModified;
        public bool IsModified { get { return isModified; } }

        private ObservableCollection<Pokemon> pokemonList;

        public ObservableCollection<Pokemon> PokemonList;

        //Istanza del singleton
        static private ProgramManager instance;
        static public ProgramManager Instance { get { if (instance == null) instance = new ProgramManager(); return instance; } }

        private ProgramManager() { }

        #region Operazioni di inizializzazione

        public async void Initialize(string dexPath = "")
        {
            FileManager fm = FileManager.Instance;
            string filePath = Windows.ApplicationModel.Package.Current.InstalledLocation.Path + @"\Files\IT\";
            string[] paths = new string[]
            {
                filePath + "Evolutions.data", filePath + "Locations.data", filePath + "Places.data",
                filePath + "Pokemon.data", filePath + "PokemonTypes.data"
            };

            foreach(string file in paths)
            {
                string[] fileLines = await fm.ReadResourceFiles(file);
                List<string> lines = new List<string>();
                foreach (string l in fileLines)
                    lines.Add(l);

                string[] s = file.Split('\\');
                switch (s[s.Length - 1])
                {
                    case "Evolutions.data":
                        EvolutionClassManager.Instance.GetEvoList(lines);
                        break;
                    case "Locations.data":
                        LocationClassManager.Instance.GetLocationList(lines);
                        break;
                    case "Places.data":
                        PokemonPlaceClassManager.Instance.GetPlaceList(lines);
                        break;
                    case "Pokemon.data":
                        PokemonClassManager.Instance.GetPokemonList(lines);
                        break;
                    case "PokemonTypes.data":
                        TypeClassManager.Instance.GetTipoList(lines);
                        break;
                }

            }

            if (dexPath != "")
            {
                NewDex(dexPath);
            }
            LoadPokemonList();
        }

        /// <summary>
        /// Metodo che crea un nuovo pokedex a partire dal percorso del file appena aperto
        /// </summary>
        /// <param name="path">Percorso del file da aprire</param>
        public void NewDex(string path)
        {
            Dex.Instance.LoadDex(path);
            regionActive = false;
            isModified = false;
            dexActive = true;
        }

        void LoadPokemonList()
        {
            GenerationClass gen = GenerationClass.Instance;
            Dex dex = Dex.Instance;
            PokemonClassManager pkcm = PokemonClassManager.Instance;

            pokemonList = new ObservableCollection<Pokemon>();
            int pokeLimit;

            if (DexActive)
                pokeLimit = gen.GenerationLimit[dex.Generation];
            else
                pokeLimit = gen.GenerationLimit[gen.GenerationLimit.Length - 1];

            int last = 0;
            foreach(Pokemon p in pkcm.PokemonList)
            {
                if (p.Number != last && p.Number <= pokeLimit)
                {
                    pokemonList.Add(p);
                    last = p.Number;
                }
            }
            PokemonList = pokemonList;
        }

        #endregion
        #region Operazioni sulla lista dei Pokémon

        /// <summary>
        /// Metodo che ritorna la lista da mostrare a partire dalla stringa in input
        /// </summary>
        /// <param name="name">Stringa da cercare tra i nomi dei pokemon</param>
        /// <returns>Lista dei Pokemon da mostrare contenenti il nome cercato</returns>
        public List<Pokemon> GetPokemonListFromText(string name)
        {
            List<Pokemon> actualListPk = new List<Pokemon>();
            string nameH = "";
            if (Char.IsLetter(name[0]))
            {
                if (Char.IsUpper(name[0]))
                    nameH = Char.ToLower(name[0]) + name.Remove(0, 1);
                else
                    nameH = Char.ToUpper(name[0]) + name.Remove(0, 1);
                

                int last = 0;

                foreach (Pokemon p in pokemonList)
                {
                    if ((p.Name.Contains(name) || p.Name.Contains(nameH)) && p.Number != last)
                    {
                        
                    }
                }
            }
            return actualListPk;
        }

#endregion
    }
}
