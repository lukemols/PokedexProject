using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PokedexProject;

namespace PokedexProject
{
    static class FileManager
    {
        static public async void ReadResourceFiles(List<string> paths)
        {
           foreach(string file in paths)
            {
                List<string> lines = new List<string>();
                if (File.Exists(file))
                {
                    StreamReader sr = new StreamReader(file);
                    string line;
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        lines.Add(line);
                    }

                    string[] s = file.Split('\\');
                    switch(s[s.Length - 1])
                    {
                        case "Evolutions.data":
                            EvolutionClassManager.GetEvoList(lines);
                            break;
                        case "Locations.data":
                            LocationClassManager.GetLocationList(lines);
                            break;
                        case "Places.data":
                            PokemonPlaceClassManager.GetPlaceList(lines);
                            break;
                        case "Pokemon.data":
                            PokemonClassManager.GetPokemonList(lines);
                            break;
                        case "PokemonTypes.data":
                            TypeClassManager.GetTipoList(lines);
                            break;
                        case "RegionalDex.data":
                            break;
                    }
                }
            }
        }

        static public async Task<List<Dex.PokeState>> GetDexAsync(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            List<Dex.PokeState> states = new List<Dex.PokeState>();

            StreamReader sr = new StreamReader(path);
            string line;

            while ((line = await sr.ReadLineAsync()) != null)
            {
                if (line == null || line[0] == '*')
                    continue;

                if (line.Contains("#INFO"))
                {
                    string[] infos = line.Split('|');
                    Dex.Generation = System.Convert.ToInt32(infos[1]);
                    Dex.Game = infos[2];
                    continue;
                }

                if (line.Contains("#VERSION"))
                {
                    string[] version = line.Split('|');
                }

                if (line.Contains("#Pk"))
                {
                    string[] poke = line.Split('|');
                    switch (poke[2])
                    {
                        case "CAPTURED":
                            states.Add(Dex.PokeState.CAPTURED);
                            break;
                        case "VIEWED":
                            states.Add(Dex.PokeState.VIEWED);
                            break;
                        default:
                            // case "NOTVIEWED":
                            states.Add(Dex.PokeState.NOTVIEWED);
                            break;
                    }
                }
            }
            return states;
        }
    }
}
