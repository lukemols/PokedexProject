using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PokedexProject
{
    class FileManager
    {
        public bool getOnStart;
        string optionPath;
        string filesPath;
        public FileManager()
        {
            string s = Application.StartupPath;
            optionPath = s + @"\Options";
            filesPath = s + @"\Files";
            System.IO.Directory.CreateDirectory(optionPath);
            System.IO.Directory.CreateDirectory(filesPath);
        }

        public async List<string> ReadFile(string fileName)
        {
            string path = filesPath + @"\" + fileName;
            List<string> lines = new List<string>();
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);
                string line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    lines.Add(line);
                }
                return lines;
            }
            else
                throw new FileNotFoundException();
        }

        public void GetOptions()
        {
            string f = optionPath + @"\program.opt";
            if (File.Exists(f))
            {
                FileStream fs = new FileStream(f, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                getOnStart = br.ReadBoolean();
                br.Close();
            }
            else
            {
                FileStream fs = new FileStream(f, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(true);
                bw.Close();
            }
        }

        public List<int> getRegionalDex(int generation)
        {
            List<int> regional = new List<int>();

            string path = filesPath + @"\RegionalDex.data";
            if (File.Exists(path))
            {
                string dex = "";
                switch (generation)
                {
                    case 1:
                        dex = "Kanto";
                        break;
                    case 2:
                        dex = "Jhoto GSC";
                        break;
                    case 3:
                        dex = "Heonn RZS";
                        break;
                    case 41:
                        dex = "Sinnoh";
                        break;
                    case 42:
                        dex = "Sinnoh Platino";
                        break;
                    case 43:
                        dex = "Jhoto HG SS";
                        break;
                    case 51:
                        dex = "Unima";
                        break;
                    case 52:
                        dex = "Unima Nuovo";
                        break;
                    case 61:
                        dex = "Kalos Centrale";
                        break;
                    case 62:
                        dex = "Kalos Costiera";
                        break;
                    case 63:
                        dex = "Kalos Montana";
                        break;
                    case 64:
                        dex = "Heonn RO ZA";
                        break;
                }
                StreamReader sr = new StreamReader(path);
                string line;
                if (generation != 51 && generation != 52)
                    regional.Add(0);
                while (!(line = sr.ReadLine()).Contains(dex))
                    continue;
                line = sr.ReadLine();
                while (!line.Contains("#RegionalDex"))
                {
                    if (line != "")
                    {
                        string[] arr = line.Split('|');
                        int n = System.Convert.ToInt32(arr[2]);
                        regional.Add(n);
                    }

                    line = sr.ReadLine();
                }
            }
            else
                throw new FileNotFoundException();
            

            return regional;
        }

       

        public List<Dex.pokeState> getDex(string path, Dex dex)
        {
            List<Dex.pokeState> pk = new List<Dex.pokeState>();

            if(!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            StreamReader sr = new StreamReader(path);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (line == null || line[0] == '*')
                    continue;

                if (line.Contains("#INFO"))
                {
                    string[] infos = line.Split('|');
                    dex.Generation = System.Convert.ToInt32(infos[1]);
                    dex.Game = infos[2];
                    continue;
                }

                if (line.Contains("#VERSION"))
                {
                    string[] version = line.Split('|');
                    if (System.Convert.ToInt32(version[1]) > 1 && System.Convert.ToInt32(version[1]) > 0)
                        throw new FileLoadException();
                }

                if (line.Contains("#Pk"))
                {
                    string[] poke = line.Split('|');
                    switch (poke[2])
                    {
                        case "CAPTURED":
                            pk.Add(Dex.pokeState.CAPTURED);
                            break;
                        case "VIEWED":
                            pk.Add(Dex.pokeState.VIEWED);
                            break;
                        default:
                            // case "NOTVIEWED":
                            pk.Add(Dex.pokeState.NOTVIEWED);
                            break;
                    }
                }
            }

            return pk;
        }

        public bool saveDex(Dex dex)
        {
            try
            {
                StreamWriter sw = new StreamWriter(dex.Path);
                sw.WriteLine("*Attenzione, file di salvataggio. Modificandolo potresti perdere e/o compromettere i dati salvati" + Environment.NewLine +
                                "* nonché causare problemi nell'operazione di lettura del programma.");
                sw.WriteLine("#INFO|" + dex.Generation + "|" + dex.Game + "|");
                sw.WriteLine("#VERSION|1|0");

                List<Dex.pokeState> pk = dex.Pokedex;
                for (int i = 0; i < pk.Count; i++ )
                {
                    string str = "#Pk|" + (i + 1).ToString() + "|";
                    switch (pk[i])
                    {
                        case Dex.pokeState.CAPTURED:
                            str += "CAPTURED";
                            break;
                        case Dex.pokeState.VIEWED:
                            str += "VIEWED";
                            break;
                        default:
                            str += "NOTVIEWED";
                            break;
                    }
                    sw.WriteLine(str);
                }

                sw.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
