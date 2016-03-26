using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    public class Location
    {
        string region;
        public string Region { get { return region; } }

        string name;
        public string Name { get { return name; } }

        string description;
        public string Description { get { return description; } }

        public Location()
        {
            region = "";
            name = "";
            description = "";
        }

        public Location(string s)
        {
            string[] arr = s.Split('|');
            try
            {
                if (arr[0] == "#city" || arr[0] == "#landmark" || arr[0] == "#route")
                {
                    region = arr[1];
                    name = arr[2];
                    description = arr[3];
                }
                else
                    throw new ProgramException("Errore nella stringa del file.");
            }
            catch
            {
                throw new ProgramException("Errore nella creazione della location");
            }
        }
    }
}
