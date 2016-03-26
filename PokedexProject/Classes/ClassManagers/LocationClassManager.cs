using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    public class LocationClassManager
    {
        private List<Location> listaLocation;
        public List<Location> ListaLocation { get { return listaLocation; } }


        //Istanza del singleton
        static private LocationClassManager instance;
        static public LocationClassManager Instance { get { if (instance == null) instance = new LocationClassManager(); return instance; } }

        /// <summary>
        /// Costruttore privato
        /// </summary>
        private LocationClassManager() { }

        /// <summary>
        /// Metodo che carica la lista dei luoghi a partire da una lista di stringhe formattata
        /// </summary>
        /// <param name="lines">Lista di stringhe contenenti i luoghi</param>
        public void GetLocationList(List<string> lines)
        {
            listaLocation = new List<Location>();
            
            while (lines.Count > 0)
            {
                try
                {
                    Location l = new Location(lines[0]);
                    listaLocation.Add(l);
                }
                catch (ProgramException) { }

                lines.RemoveAt(0);
            }
        }

        /// <summary>
        /// Metodo che ritorna una lista di luoghi appartenenti ad una regione
        /// </summary>
        /// <param name="region">Regione da cercare</param>
        /// <returns>Lista dei luoghi</returns>
        public List<Location> GetRegionalLocation(string region)
        {
            List<Location> locs = new List<Location>();
            foreach (Location l in listaLocation)
            {
                if (l.Region == region)
                    locs.Add(l);
            }
            return locs;
        }
    }
}
