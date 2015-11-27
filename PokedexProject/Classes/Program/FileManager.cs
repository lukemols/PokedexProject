using System;
using System.Collections.Generic;
using System.IO;

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
                            break;
                        case "Pokemon.data":
                            break;
                        case "PokemonTypes.data":
                            break;
                        case "RegionalDex.data":
                            break;
                    }
                }
            }
        }

        static public async void GetDex()
        {

        }
    }
}
