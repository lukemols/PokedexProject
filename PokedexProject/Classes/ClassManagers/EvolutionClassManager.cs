﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    public class EvolutionClassManager
    {
        private List<Evolution> listaEvo;

        public List<Evolution> ListaEvo { get { return listaEvo; } }

        //Istanza del singleton
        static private EvolutionClassManager instance;
        static public EvolutionClassManager Instance { get { if (instance == null) instance = new EvolutionClassManager(); return instance; } }

        /// <summary>
        /// Costruttore privato
        /// </summary>
        private EvolutionClassManager() { }

        /// <summary>
        /// Metodo che carica tutte le evoluzioni a partire da una lista di stringhe
        /// </summary>
        /// <param name="lines">Lista di stringhe contenenti le evoluzioni</param>
        public void GetEvoList(List<string> lines)
        {
            listaEvo = new List<Evolution>();

            while (lines.Count > 0)
            {
                try
                {
                    Evolution e = new Evolution(lines[0]);
                    listaEvo.Add(e);
                }
                catch (ProgramException) { }
                lines.RemoveAt(0);
            }
        }


        /// <summary>
        /// Ottieni le evoluzioni del Pokémon selezionato
        /// </summary>
        /// <param name="index">Numero del Pokémon</param>
        /// <returns>Lista di evoluzioni</returns>
        public List<Evolution> GetEvos(int index)
        {
            List<Evolution> evos = new List<Evolution>();
            Evolution pre = new Evolution();

            foreach (Evolution e in listaEvo)
            {
                if (e.Higher == index)
                {
                    pre = e;
                    break;
                }
            }

            if (pre.Lower != 0) // esiste una preevoluzione
            {
                foreach (Evolution e in listaEvo)
                {
                    if (e.Higher == pre.Lower) // se index è il terzo stadio
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
                if (e.Lower == index)
                {
                    evos.Add(e);
                    hasPostEvo = true;
                }
            }

            if (hasPostEvo)
            {
                for (int i = 0; i < evos.Count; i++)
                {
                    if (evos[i].Lower == index)
                    {
                        foreach (Evolution e in listaEvo)
                        {
                            if (e.Lower == evos[i].Higher)
                                evos.Add(e);
                        }
                    }
                }
            }
            GenerationClass gen = GenerationClass.Instance;
            for (int i = evos.Count - 1; i >= 0; i--)
            {//Se è una evoluzione futura rimuovila
                if (evos[i].Lower > gen.GenerationLimit[gen.ActualGeneration]
                    || evos[i].Higher > gen.GenerationLimit[gen.ActualGeneration])
                    evos.RemoveAt(i);
            }
            return evos;
        }        
    }
}
