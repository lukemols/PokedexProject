using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexProject
{
    static class GameClass
    {
        public enum Game
        {
            ROSSO, BLU, GIALLO,
            ORO, ARGENTO, CRISTALLO,
            RUBINO, ZAFFIRO, SMERALDO, ROSSOFUOCO, VERDEFOGLIA,
            DIAMANTE, PERLA, PLATINO, HEARTGOLD, SOULSILVER,
            BIANCO, NERO, BIANCO2, NERO2,
            X, Y, RUBINOOMEGA, ZAFFIROALPHA,
            NOTDEFINED
        };

        static string[] italianGames = { "Rosso", "Blu", "Giallo",
                                        "Oro", "Argento", "Cristallo",
                                        "Rubino", "Zaffiro", "Smeraldo", "RF", "VF",
                                        "Diamante", "Perla", "Platino", "HG", "SS",
                                        "Bianco", "Nero", "Bianc2", "Ner2",
                                        "X", "Y", "RO", "ZA",
                                        "NotDefined"};

        static string[] englishGames = { "Red", "Blue", "Yellow",
                                        "Gold", "Silver", "Cristal",
                                        "Ruby", "Sapphire", "Emerald", "FR", "LG",
                                        "Diamond", "Pearl", "Platinum", "HG", "SS",
                                        "White", "Black", "Whit2", "Blac2",
                                        "X", "Y", "OR", "AS",
                                        "NotDefined"};

        /// <summary>
        /// Ritorna la stringa del nome del gioco a partire dall'enum del gioco
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        static public string GetGameName(Game game)
        {
            return italianGames[(int)game];
        }

        static public Game GetGameFromString(string str)
        {
            for (int i = 0; i < italianGames.Length; i++)
                if (italianGames[i] == str)
                    return (Game)i;
            throw new Exception();
        }
    }
}
