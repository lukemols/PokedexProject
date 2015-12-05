using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PokedexProject;

namespace PokedexProject
{
    static class FileManager
    {
        static public async Task<string[]> ReadResourceFiles(string filePath)
        {
            Windows.Storage.StorageFile sFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(filePath);
            
            string text = await Windows.Storage.FileIO.ReadTextAsync(sFile);
            string[] lines = text.Split('\n');
            
            return lines;           
        }

        static public async Task<List<Dex.PokeState>> GetDexAsync(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            List<Dex.PokeState> states = new List<Dex.PokeState>();

            Windows.Storage.StorageFile sFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(path);

            string text = await Windows.Storage.FileIO.ReadTextAsync(sFile);
            string[] lines = text.Split('\n');

            foreach(string line in lines)
            {
                if (line == null || line[0] == '*')
                    continue;
                if(line.Contains("#INFO"))
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

                if(line.Contains("#Pk"))
                {
                    string[] poke = line.Split('|');
                    int num = System.Convert.ToInt32(poke[1]);
                    while(states.Count < num - 1)
                    {
                        states.Add(Dex.PokeState.NOTVIEWED);
                    }
                    Dex.PokeState pkState;
                    switch (poke[2])
                    {
                        case "CAPTURED":
                            pkState = Dex.PokeState.CAPTURED;
                            break;
                        case "VIEWED":
                            pkState = Dex.PokeState.VIEWED;
                            break;
                        case "NOTVIEWED":
                        default:
                            pkState = Dex.PokeState.NOTVIEWED;
                            break;
                    }
                    if (states.Count > num)
                        states[num - 1] = pkState;
                    else
                        states.Add(pkState);
                }
            }
            
            return states;
        }
    }
}
