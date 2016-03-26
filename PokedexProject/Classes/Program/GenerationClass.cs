using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexProject
{
    public class GenerationClass
    {
        int[] generationLimit = { 0, 151, 251, 386, 493, 649, 721 };
        public int[] GenerationLimit { get { return generationLimit; } }

        int actualGeneration = 6;
        public int ActualGeneration
        {
            get
            {
                return actualGeneration;
            }
            set
            {
                if (value < 7 && value > 0)
                    actualGeneration = value;
            }
        }

        //Istanza del singleton
        static private GenerationClass instance;
        static public GenerationClass Instance { get { if (instance == null) instance = new GenerationClass(); return instance; } }

        private GenerationClass() { }
    }
}
