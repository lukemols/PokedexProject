using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexProject
{
    static class GenerationClass
    {
        static int[] generationLimit = { 0, 151, 251, 386, 493, 649, 721 };
        static public int[] GenerationLimit { get { return generationLimit; } }

        static int actualGeneration = 6;
        static public int ActualGeneration
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
    }
}
