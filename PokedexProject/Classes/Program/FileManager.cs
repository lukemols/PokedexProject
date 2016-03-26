using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PokedexProject;

namespace PokedexProject
{
    class FileManager
    {
        //Istanza del singleton
        static private FileManager instance;
        static public FileManager Instance { get { if (instance == null) instance = new FileManager(); return instance; } }

        private FileManager() { }


        public async Task<string[]> ReadResourceFiles(string filePath)
        {
            Windows.Storage.StorageFile sFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(filePath);
            
            string text = await Windows.Storage.FileIO.ReadTextAsync(sFile);
            string[] lines = text.Split('\n');
            
            return lines;           
        }

        public async Task<List<Dex.PokeState>> GetDexAsync(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            List<Dex.PokeState> states = new List<Dex.PokeState>();
            Dex dex = Dex.Instance;

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
                    dex.Generation = System.Convert.ToInt32(infos[1]);
                    dex.Game = infos[2];
                    continue;
                }

                else if (line.Contains("#VERSION"))
                {
                    string[] version = line.Split('|');
                }

                else if (line.Contains("#AO"))
                {
                    string[] l = line.Split('|');
                    int ao = System.Convert.ToInt32(l[1]);
                    dex.AO = ao;
                }

                else if (line.Contains("PLAYER"))
                {
                    string[] l = line.Split('|');
                    dex.PlayerName = l[1];                  
                }

                else if (line.Contains("#Pk"))
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
