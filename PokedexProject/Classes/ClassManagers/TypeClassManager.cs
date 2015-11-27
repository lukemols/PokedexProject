using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    static class TypeClassManager
    {
        private static List<Type> listaTipo;
        static public List<Type> ListaTipo { get { return listaTipo; } }
        
        /// <summary>
        /// Metodo che crea la lista dei tipi a partire da una lista di stringhe
        /// </summary>
        /// <param name="lines">Lista di stringhe contenenti i tipi</param>
        static public void GetTipoList(List<string> lines)
        {
            listaTipo = new List<Type>();
            
            while (lines.Count > 0)
            {
                try
                {
                    Type t = new Type(lines[0]);
                    listaTipo.Add(t);
                }
                catch (ProgramException) { }

                lines.RemoveAt(0);
            }
        }

        /// <summary>
        /// Metodo che ritorna i tipi del Pokémon selezionato
        /// </summary>
        /// <param name="index">Indice del Pokémon</param>
        /// <returns>Lista dei tipi</returns>
        static public List<Type> GetPokeTypes(int index)
        {
            List<Type> types = new List<Type>();
            foreach (Type t in listaTipo)
                if (t.PokemonNumber == index)
                    types.Add(t);
            return types;
        }
    
    }
}
