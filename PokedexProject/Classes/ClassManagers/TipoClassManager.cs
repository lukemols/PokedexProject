using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    class TipoClassManager
    {
        ProgramManager pm;
        FileManager fm;
        private static int actualGen;
        private static List<Tipo> listaTipo;
        public List<Tipo> ListaTipo { get { return listaTipo; } }

        public TipoClassManager()
        {
            fm = new FileManager();
            pm = new ProgramManager();
            actualGen = 6;
            if (listaTipo == null)
            {
                if(pm.DexActive)
                    GetTipoList((actualGen = pm.GetGeneration()));
                else
                    GetTipoList(actualGen);
            }
            else if (pm.DexActive && actualGen != pm.GetGeneration())
            {
                GetTipoList((actualGen = pm.GetGeneration()));
            }
        }

        public void GetTipoList(int generation)
        {
            List<string> lines;
            listaTipo = new List<Tipo>();

            try
            {
                lines = fm.ReadFile(@"TipiPokemon.data");
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
                    Tipo t = new Tipo(lines[0]);
                    if (t.numero > g[generation])
                        break;
                    listaTipo.Add(t);
                    lines.RemoveAt(0);
                }
                catch (ProgramException)
                {
                    lines.RemoveAt(0);
                    continue;
                }
            }
        }

        public List<Tipo> GetPokeTypes(int index)
        {
            List<Tipo> types = new List<Tipo>();
            foreach (Tipo t in listaTipo)
                if (t.numero == index)
                    types.Add(t);
            return types;
        }
    
    }
}
