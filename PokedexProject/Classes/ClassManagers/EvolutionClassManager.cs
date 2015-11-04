using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    class EvolutionClassManager
    {
        ProgramManager pm;
        FileManager fm;
        private static List<Evolution> listaEvo;

        public List<Evolution> ListaEvo { get { return listaEvo; } }

        public EvolutionClassManager()
        {
            fm = new FileManager();
            pm = new ProgramManager();
            if (listaEvo == null)
            {
                if(pm.DexActive)
                    GetEvoList(pm.GetGeneration());
                else
                    GetEvoList(6);
            }
        }

        /// <summary>
        /// Ottieni le evoluzioni del Pokémon selezionato
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<Evolution> GetEvos(int index)
        {
            List<Evolution> evos = new List<Evolution>();
            Evolution pre = new Evolution();

            foreach (Evolution e in listaEvo)
            {
                if (e.numero2 == index)
                {
                    pre = e;
                    break;
                }
            }

            if (pre.numero1 != 0) // esiste una preevoluzione
            {
                foreach (Evolution e in listaEvo)
                {
                    if (e.numero2 == pre.numero1) // se index è il terzo stadio
                    {
                        evos.Add(e);
                        break;
                    }
                }
                evos.Add(pre);
            }

            bool hasPostEvo = false;

            foreach (Evolution e in listaEvo)
            {
                if (e.numero1 == index)
                {
                    evos.Add(e);
                    hasPostEvo = true;
                }
            }

            if (hasPostEvo)
            {
                for (int i = 0; i < evos.Count; i++)
                {
                    if (evos[i].numero1 == index)
                    {
                        foreach (Evolution e in listaEvo)
                        {
                            if (e.numero1 == evos[i].numero2)
                                evos.Add(e);
                        }
                    }
                }
            }

            return evos;
        }

        /// <summary>
        /// Metodo che carica tutte le evoluzioni fino alla generazione selezionata
        /// </summary>
        /// <param name="generation"></param>
        public void GetEvoList(int generation)
        {
            List<string> lines;
            listaEvo = new List<Evolution>();
            try
            {
                lines = fm.ReadFile(@"Evoluzioni.data");
            }
            catch
            {
                return;
            }

            int[] g = new int[] { 0, 151, 251, 386, 493, 649, 719 };

            while (lines.Count > 0)
            {
                try
                {
                    Evolution e = new Evolution(lines[0]);
                    if ((e.numero1 > g[generation]) || (e.numero2 > g[generation]))
                        break;
                    listaEvo.Add(e);
                    lines.RemoveAt(0);
                }
                catch (ProgramException)
                {
                    lines.RemoveAt(0);
                    continue;
                }
            }
        }
    }
}
