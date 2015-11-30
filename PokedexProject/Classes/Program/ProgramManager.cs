using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;

namespace PokedexProject
{
    static class ProgramManager
    {
        static bool dexActive;
        static public bool DexActive { get { return dexActive; } }

        static bool regionActive;
        static public bool RegionActive { get { return regionActive; } }

        static bool isModified;
        static public bool IsModified { get { return isModified; } }

        static ObservableCollection<Pokemon> pokemonList;

        static void Initialize(string dexPath)
        {
            string filePath = Windows.ApplicationModel.Package.Current.InstalledLocation.Path + @"\Files\IT\";
            List<string> paths = new List<string>
            {
                filePath + "Evolutions.data", filePath + "Locations.data", filePath + "Places.data",
                filePath + "Pokemon.data", filePath + "PokemonTypes.data"
            };
            FileManager.ReadResourceFiles(paths);
            if(dexPath != "")
            {
                NewDex(dexPath);
            }
        }

        /// <summary>
        /// Metodo che crea un nuovo pokedex a partire dal percorso del file appena aperto
        /// </summary>
        /// <param name="path">Percorso del file da aprire</param>
        static public void NewDex(string path)
        {
            Dex.LoadDex(path);
            regionActive = false;
            isModified = false;
            dexActive = true;
        }

        #region Operazioni sulla lista dei Pokémon

        /// <summary>
        /// Metodo che ritorna la lista da mostrare a partire dalla stringa in input
        /// </summary>
        /// <param name="name">Stringa da cercare tra i nomi dei pokemon</param>
        /// <returns>Lista dei Pokemon da mostrare contenenti il nome cercato</returns>
        static public List<Pokemon> GetPokemonListFromText(string name)
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
