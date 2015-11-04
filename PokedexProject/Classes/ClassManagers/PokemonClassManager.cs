using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    class PokemonClassManager
    {
        private FileManager fm;

        static List<Pokemon> listaPk;
        public List<Pokemon> ListaPk { get { return listaPk; } }

        public PokemonClassManager()
        {
            fm = new FileManager();
            if (listaPk == null)
                GetPokemonList(6);
        }

        public void GetPokemonList(int generation)
        {
            List<string> lines = new List<string>();
            listaPk = new List<Pokemon>();

            try
            {
                lines = fm.ReadFile(@"Pokemon.data");
            }
            catch
            {
                return;
            }

            int[] g = new int[] { 0, 151, 251, 386, 493, 649, 721 };
            while (lines.Count > 0)
            {
                try
                {
                    Pokemon p = new Pokemon(lines[0]);
                    if (p.numero > g[generation])
                        break;
                    listaPk.Add(p);
                    lines.RemoveAt(0);
                }
                catch (ProgramException)
                {
                    lines.RemoveAt(0);
                    continue;
                }
            }
        }

        public Pokemon GetPokemon(int index)
        {
            Pokemon poke = new Pokemon();
            foreach (Pokemon p in listaPk)
            {
                if (p.numero == index)
                {
                    poke = p;
                    break;
                }
            }
            return poke;
        }

        public List<Pokemon> GetPokeForms(int index)
        {
            List<Pokemon> poke = new List<Pokemon>();
            foreach (Pokemon p in listaPk)
            {
                if (p.numero == index)
                {
                    poke.Add(p);
                }
            }
            return poke;
        }
    }
}
