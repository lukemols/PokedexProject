using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PokedexProject
{
    class ProgramManager
    {
        //Liste
        static List<Pokemon> listPk; //Contiene tutti i pokemon
        static List<Pokemon> actualListPk;
        static List<int> regionalListPk;
        static List<Location> locations;
        static List<Location> actualListLoc;

        Pokemon fake = new Pokemon();
        Location fakeL = new Location();

        int startNumber;
        public int StartNumber { get { return startNumber; } }
        int pokeLimit;
        public int PokeLimit { get { return pokeLimit; } }
        static bool regionActive = false;
        static bool isModified = false;
        public bool IsModified { get { return isModified; } }
        static bool dexActive = false;
        public bool DexActive { get { return dexActive; } }
        static Dex actualDex;

        int startNumberLocation;
        public int StartNumberLocation { get { return startNumberLocation; } }
        int limitNumberLocation;
        static bool showPoke = true; /// Se showPoke è true allora mostra i pokemon, altrimenti i luoghi
        public bool ShowPoke { get { return showPoke; } set { if (value) showPoke = value; } }
        
        FileManager fm;
        PokemonClassManager pkClassMngr;

        public ProgramManager()
        {

            fm = new FileManager();
            if(actualListPk == null)
                actualListPk = new List<Pokemon>();
            if(locations == null)
                locations = new List<Location>();
            if(actualListLoc == null)
                actualListLoc = new List<Location>();
            pkClassMngr = new PokemonClassManager();
            if (listPk == null)
            {
                listPk = pkClassMngr.ListaPk;
                startNumber = 1;
                pokeLimit = listPk[listPk.Count - 1].numero;
            }
            if(actualDex != null)
            {
                startNumber = 1;
                pokeLimit = actualDex.Pokenum;
            }
        }

        #region Metodi per la creazione del pokedex
        /// <summary>
        /// Metodo che crea un nuovo pokedex a partire da generazione e gioco
        /// </summary>
        /// <param name="gen">Intero che rappresenta la generazione del gioco</param>
        /// <param name="game">Stringa in italiano che rappresenta il nome del gioco</param>
        public void NewDex(int gen, string game)
        {
            actualDex = new Dex(gen, game);
            regionActive = false;
            isModified = false;
            dexActive = true;
            startNumber = 1;
            pokeLimit = actualDex.Pokenum;
        }
        /// <summary>
        /// Metodo che crea un nuovo pokedex a partire dal percorso del file appena aperto
        /// </summary>
        /// <param name="path">Percorso del file da aprire</param>
        public void NewDex(string path)
        {
            actualDex = new Dex(path);
            actualDex.openDex();
            regionActive = false;
            isModified = false;
            dexActive = true;
            startNumber = 1;
            pokeLimit = actualDex.Pokenum;
        }
        #endregion

        #region Metodi che gestiscono le liste dei pokemon
        /// <summary>
        /// Metodo che ritorna la lista da mostrare a partire dal numero attuale
        /// </summary>
        /// <returns>Lista dei Pokemon da mostrare</returns>
        public List<Pokemon> GetPokemonListFromNumber()
        {
            ResetList();
            actualListPk.Add(fake);
            for (int i = 0; i < 10; i++ )
            {
                int index = i + startNumber;
                if (regionActive && index >= regionalListPk.Count)
                    break;
                foreach(Pokemon p in listPk)
                {
                    if (regionActive && p.numero == regionalListPk[index])
                    {
                        actualListPk.Add(p);
                        break;
                    }
                    else if (!regionActive && p.numero == index && p.numero <= pokeLimit)
                    {
                        actualListPk.Add(p);
                        break;
                    }
                }
            }
            return actualListPk;
        }
        /// <summary>
        /// Metodo che ritorna la lista da mostrare a partire dal numero attuale
        /// </summary>
        /// <param name="index">Numero da cui iniziare la ricerca</param>
        /// <returns>Lista dei Pokemon da mostrare</returns>
        public List<Pokemon> GetPokemonListFromNumber(int index)
        {
            if (index < 0)
                index = 0;
            else if (!regionActive && index > pokeLimit)
                index = pokeLimit;
            else if (regionActive && index > pokeLimit)
                index = pokeLimit - 1;
            startNumber = index;
            return GetPokemonListFromNumber();
        }
        /// <summary>
        /// Metodo che ritorna la lista da mostrare a partire dalla stringa in input
        /// </summary>
        /// <param name="name">Stringa da cercare tra i nomi dei pokemon</param>
        /// <returns>Lista dei Pokemon da mostrare contenenti il nome cercato</returns>
        public List<Pokemon> GetPokemonListFromText(string name)
        {
            string nameH = "";
            if(Char.IsLetter(name[0]))
            {
                if (Char.IsUpper(name[0]))
                    nameH = Char.ToLower(name[0]) + name.Remove(0, 1);
                else
                    nameH = Char.ToUpper(name[0]) + name.Remove(0, 1);

                ResetList();
                actualListPk.Add(fake);

                int last = 0;
                int count = 0;
                
                foreach(Pokemon p in listPk)
                {
                    if((p.nome.Contains(name) || p.nome.Contains(nameH)) && p.numero != last)
                    {
                        if(regionActive)
                        {
                            for(int i = 0; i < regionalListPk.Count; i++)
                                if(p.numero == regionalListPk[i])
                                {
                                    actualListPk.Add(p);
                                    last = p.numero;
                                    break;
                                }
                        }
                        else
                        {
                            if (p.numero > pokeLimit)
                                break;
                            actualListPk.Add(p);
                            last = p.numero;
                        }
                        if ((++count) > 10)
                            break;
                    }
                }
            }
            return actualListPk;
        }

        /// <summary>
        /// Metodo che ritorna la lista degli stati del pokedex per la lista attuale dei pokemon
        /// </summary>
        /// <returns></returns>
        public List<Dex.pokeState> GetPokedexStates()
        {
            List<Dex.pokeState> states = new List<Dex.pokeState>();
            if (dexActive)
            {
                states.Add(Dex.pokeState.NOTVIEWED);
                for (int i = 1; i < actualListPk.Count; i++)
                    states.Add(actualDex.getState(actualListPk[i].numero));
            }

            return states;
        }

        public Dex.pokeState GetPokemonState(int index)
        {
            if (dexActive)
                return actualDex.getState(index);
            else
                return Dex.pokeState.NOTVIEWED;
        }

        public void SetPokemonState(int index, Dex.pokeState state)
        {
            if (dexActive)
            {
                if (actualDex.getState(index) != state)
                {
                    actualDex.setState(index, state);
                    isModified = true;
                }
            }
        }

        /// <summary>
        /// Metodo che setta il pokedex regionale per la generazione dichiarata. Se non coincide con la generazione del pokedex attuale ritorna falso
        /// </summary>
        /// <param name="generation"></param>
        /// <returns></returns>
        public bool SetRegionalDex(int generation)
        {
            regionActive = false;
            if (dexActive)
            {
                switch (actualDex.Generation)
                {
                    case 1:
                        if (generation != 1)
                            return false;
                        break;
                    case 2:
                        if (generation != 2)
                            return false;
                        break;
                    case 3:
                        if (generation == 3 && (actualDex.Game == "Rubino" || actualDex.Game == "Zaffiro" || actualDex.Game == "Smeraldo"))
                            break;
                        else if (generation == 1 && (actualDex.Game == "Rosso Fuoco" || actualDex.Game == "Verde Foglia"))
                            break;
                        else
                            return false;
                    case 4:
                        if (generation == 41 && (actualDex.Game == "Diamante" || actualDex.Game == "Perla"))
                            break;
                        else if (generation == 42 && actualDex.Game == "Platino")
                            break;
                        else if (generation == 43 && (actualDex.Game == "Heart Gold" || actualDex.Game == "Soul Silver"))
                            break;
                        else
                            return false;
                    case 5:
                        if (generation == 51 && (actualDex.Game == "Bianco" || actualDex.Game == "Nero"))
                            break;
                        else if (generation == 52 && (actualDex.Game == "Bianco 2" || actualDex.Game == "Nero 2"))
                            break;
                        else
                            return false;
                    case 6:
                        if ((generation == 61 || generation == 62 || generation == 63) && (actualDex.Game == "X" || actualDex.Game == "Y"))
                            break;
                        else if (generation == 64 && (actualDex.Game == "Rubino Omega" || actualDex.Game == "Zaffiro Alpha"))
                            break;
                        else
                            return false;
                }
            }
            try
            {
                regionalListPk = fm.getRegionalDex(generation);
                regionActive = true;
                startNumber = 0;
                pokeLimit = regionalListPk.Count;
            }
            catch(FileNotFoundException)
            {
                regionActive = false;
                return false;
            }
            return true;
        }

        public void SetNationalDex()
        {
            regionActive = false;
            startNumber = 0;
            pokeLimit = listPk[listPk.Count - 1].numero;
        }
        /// <summary>
        /// Metodo che resetta la lista attuale
        /// </summary>
        private void ResetList()
        {
            for (int i = actualListPk.Count - 1; i >= 0; i--)
                actualListPk.Remove(actualListPk[i]);
        }
        #endregion

        #region Metodi che gestiscono le liste dei luoghi
        /// <summary>
        /// Metodo che setta la lista dei luoghi a seconda della regione passata
        /// </summary>
        /// <param name="region">Regione dei luoghi da mostrare</param>
        public void SetListFromRegion(string region)
        {
            locations.Clear();
            LocationClassManager locationClassManager = new LocationClassManager();
            locations = locationClassManager.GetRegionalLocation(region);
            startNumberLocation = 0;
            limitNumberLocation = locations.Count;
        }
        /// <summary>
        /// Metodo che setta la lista attuale dei luoghi da mostrare
        /// </summary>
        /// <returns>Lista dei luoghi da mostrare</returns>
        public List<Location> GetActualListLocation()
        {
            actualListLoc.Clear();
            actualListLoc.Add(fakeL);
            for (int i = startNumberLocation; i < startNumberLocation + 10; i++)
            {
                if (i >= limitNumberLocation)
                    break;
                actualListLoc.Add(locations[i]);
            }
            return actualListLoc;
        }
        /// <summary>
        /// Metodo che ritorna la lista dei luoghi da mostrare a partire dall'indice
        /// </summary>
        /// <param name="index">Indice di partenza</param>
        /// <returns>Lista dei luoghi da mostrare</returns>
        public List<Location> GetActualListLocation(int index)
        {
            if (index < 0)
                index = 0;
            else if (index > limitNumberLocation)
                index = limitNumberLocation - 1;
            startNumberLocation = index;
            return GetActualListLocation();
        }
        /// <summary>
        /// Metodo che ritorna la lista dei luoghi che contengono la stringa richiesta
        /// </summary>
        /// <param name="name">Stringa da cercare all'interno dei nomi dei luoghi</param>
        /// <returns>Lista contenente i luoghi che corrispondono al nome cercato</returns>
        public List<Location> GetActualListLocationFromName(string name)
        {
            string nameH = "";
            if (Char.IsLetter(name[0]) || Char.IsNumber(name[0]))
            {
                if (Char.IsLetter(name[0]) && Char.IsUpper(name[0]))
                    nameH = Char.ToLower(name[0]) + name.Remove(0, 1);
                else if (Char.IsLetter(name[0]) && Char.IsLower(name[0]))
                    nameH = Char.ToUpper(name[0]) + name.Remove(0, 1);
                else
                    nameH = name;
                actualListLoc.Clear();
                actualListLoc.Add(fakeL);
                int count = 0;
                foreach(Location l in locations)
                {
                    if (count > 9)
                        break;
                    if (l.name.Contains(name) || l.name.Contains(nameH))
                    {
                        actualListLoc.Add(l);
                        count++;
                    }
                }
            }

            return actualListLoc;
        }
        /// <summary>
        /// Metodo che controlla la compatibilità tra il luoghi e la regione attuale e setta la lista dei luoghi
        /// </summary>
        /// <param name="region">Regione dei luoghi da mostrare</param>
        /// <returns>Se ritorna true è andato tutto a buon fine, altrimenti no</returns>
        public bool SetRegion(string region)
        {
            showPoke = true;
            if(dexActive)
            {
                switch(region)
                {
                    case "Kanto":
                        if (actualDex.Generation == 1 || actualDex.Generation == 2)
                            break;
                        else if (actualDex.Generation == 3 && (actualDex.Game == "Rosso Fuoco" || actualDex.Game == "Verde Foglia"))
                            break;
                        else if (actualDex.Generation == 4 && (actualDex.Game == "Heart Gold" || actualDex.Game == "Soul Silver"))
                            break;
                        else
                            return false;
                    case "Settipelago":
                        if (actualDex.Generation == 3 && (actualDex.Game == "Rosso Fuoco" || actualDex.Game == "Verde Foglia" || actualDex.Game == "Smeraldo"))
                            break;
                        else
                            return false;
                    case "Jhoto":
                        if (actualDex.Generation == 2)
                            break;
                        else if (actualDex.Generation == 4 && (actualDex.Game == "Heart Gold" || actualDex.Game == "Soul Silver"))
                            break;
                        else
                            return false;
                    case "Heonn":
                        if (actualDex.Generation == 3 && (actualDex.Game == "Rubino" || actualDex.Game == "Zaffiro" || actualDex.Game == "Smeraldo"))
                            break;
                        else if (actualDex.Generation == 6 && (actualDex.Game == "Rubino Omega" || actualDex.Game == "Zaffiro Alpha"))
                            break;
                        else
                            return false;
                    case "Sinnoh":
                        if (actualDex.Generation == 4 && (actualDex.Game == "Diamante" || actualDex.Game == "Perla" || actualDex.Game == "Platino"))
                            break;
                        else
                            return false;
                    case "Unima":
                        if (actualDex.Generation == 5)
                            break;
                        else
                            return false;
                    case "Kalos":
                        if (actualDex.Generation == 6 && (actualDex.Game == "X" || actualDex.Game == "Y"))
                            break;
                        else
                            return false;
                }
            }
            try
            {
                SetListFromRegion(region);
                showPoke = false;
            }
            catch
            {
                showPoke = true;
                return false;
            }
            return true;
        }

        #endregion

        #region Metodi per il salvataggio dei dati e la gestione del pokedex durante l'utilizzo
        /// <summary>
        /// Metodo che setta gli stati del pokedex
        /// </summary>
        /// <param name="states"></param>
        public void SaveChanges(List<Dex.pokeState> states)
        {
            if (!dexActive)
                return;
            for(int i = 1; i < 10; i++)
            {
                if (i < actualListPk.Count)
                {
                    if (!isModified && (actualDex.getState(actualListPk[i].numero) != states[i]))
                        isModified = true;
                    actualDex.setState(actualListPk[i].numero, states[i]);
                }
                else
                    break;
            }
        }

        public void SetAllViewed()
        {
            if (!dexActive)
                return;
            for(int i = 0; i <= pokeLimit; i++)
            {
                int index;
                if (regionActive && i != pokeLimit)
                    index = regionalListPk[i];
                else
                    index = i;
                if (actualDex.getState(index) == Dex.pokeState.NOTVIEWED)
                    actualDex.setState(index, Dex.pokeState.VIEWED);
            }
            isModified = true;
        }

        public void SetAllCaptured()
        {
            if (!dexActive)
                return;
            for (int i = 0; i <= pokeLimit; i++)
            {
                int index;
                if (regionActive && i != pokeLimit)
                    index = regionalListPk[i];
                else
                    index = i;
                actualDex.setState(index, Dex.pokeState.CAPTURED);
            }
            isModified = true;
        }

        public bool SaveDex()
        {
            if(fm.saveDex(actualDex))
            {
                isModified = false;
                return true;
            }
            else
            {
                isModified = true;
                return false;
            }
        }
        
        public string GetDexPath()
        {
            if (!dexActive)
                return "Nessun file aperto";
            else
                return actualDex.Path;
        }

        public bool SetDexPath(string path)
        {
            if (!dexActive)
                return false;
            actualDex.Path = path;
            return true;
        }

        public string GetStatus()
        {
            if (!dexActive)
                return "";
            return "Gioco attuale: " + actualDex.Game + ", della generazione " + actualDex.Generation.ToString();
        }

        public string GetGameName()
        {
            if (!dexActive)
                return "";
            return actualDex.Game;
        }

        public int GetGeneration()
        {
            if (!dexActive)
                return 0;
            return actualDex.Generation;
        }
        #endregion

    }
}
