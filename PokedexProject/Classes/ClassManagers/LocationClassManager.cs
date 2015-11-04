using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    class LocationClassManager
    {
        
        ProgramManager pm;
        FileManager fm;
        private static List<Location> listaLocation;
        public List<Location> ListaLocation { get { return listaLocation; } }
             

        public LocationClassManager()
        {
            fm = new FileManager();
            pm = new ProgramManager();
            if (listaLocation == null)
            {
                GetLocationList();
            }
        }

        public void GetLocationList()
        {
            List<string> lines;
            listaLocation = new List<Location>();

            try
            {
                lines = fm.ReadFile(@"Location.data");
            }
            catch
            {
                return;
            }

            while (lines.Count > 0)
            {
                try
                {
                    Location l = new Location(lines[0]);
                    listaLocation.Add(l);
                    lines.RemoveAt(0);
                }
                catch (ProgramException)
                {
                    lines.RemoveAt(0);
                    continue;
                }
            }
        }

        public List<Location> GetRegionalLocation(string region)
        {
            List<Location> locs = new List<Location>();
            foreach (Location l in listaLocation)
            {
                if (l.region == region)
                    locs.Add(l);
            }
            return locs;
        }
    }
}
