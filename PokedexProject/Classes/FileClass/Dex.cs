using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokedexProject
{
    class Dex
    {
        public enum pokeState { NOTVIEWED = 0, VIEWED = 1, CAPTURED = 2 };
        private string game;
        public string Game
        {
            get
            {
                return game;
            }
            set
            {
                string[] games = {"Rosso", "Blu", "Giallo", "Oro", "Argento", "Cristallo", "Rubino", "Zaffiro", "Smeraldo", "Rosso Fuoco", "Verde Foglia", 
                                  "Diamante", "Perla", "Platino", "Heart Gold", "Soul Silver", "Bianco", "Nero", "Bianco 2", "Nero 2", "X", "Y", "Rubino Omega", "Zaffiro Alpha" };
                foreach(string s in games)
                {
                    if(value == s)
                    {
                        game = s;
                        break;
                    }
                }
            }
        }
        private int generation;
        public int Generation { get { return generation; } set { if (value > 0 && value < GenerationClass.GenerationLimit.Length) generation = value; } }

        private string path;
        public string Path { get { return path; } set { path = value; } }

        private int pokenum;
        public int Pokenum { get { return pokenum; } }

        private List<pokeState> pokedex;
        public List<pokeState> Pokedex { get { return pokedex; } }


        public Dex(int gen, string g)
        {
            generation = gen;
            game = g;
            createDex();
            path = "";
        }

        public Dex(string p)
        {
            path = p;
        }

        public void createDex()
        {
            pokenum = GenerationClass.GenerationLimit[generation];
            pokedex = new List<pokeState>();
            for (int i = 0; i < pokenum; i++)
            {
                pokedex.Add(pokeState.NOTVIEWED);
            }
        }

        public void OpenDex()
        {
            pokedex = FileManager.GetDex(path, this);
            pokenum = pokedex.Count;
        }

        public pokeState getState(int index)
        {
            if (index <= 0)
                index = 1;
            return Pokedex[index - 1];
        }

        public void setState(int index, pokeState state)
        {
            if (index <= 0)
                index = 1;
            Pokedex[index - 1] = state;
        }
    }
}