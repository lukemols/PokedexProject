using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PokedexProject;

namespace PokedexProject8
{
    public partial class PokemonPage : PhoneApplicationPage
    {
        Pokemon ShowedPokemon;
        List<Pokemon> pokemonForms;
        List<Evolution> pokemonEvolutions;
        List<PokemonPlace> pokemonLocations;

        public PokemonPage()
        {
            InitializeComponent();

            FormListPicker.SelectionChanged += FormListPicker_SelectionChanged;
        }

        /// <summary>
        /// Metodo che setta i controlli della pagina con il Pokémon corrente
        /// </summary>
        void SetPokemonInfo()
        {
            // Metti il nome del Pokémon come titolo
            TitlePage.Text = ShowedPokemon.Name;
            // Mostra l'immagine
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(GetImagePath(), UriKind.Relative));
            PokemonImage.Fill = ib;
            // Scrivi numero, peso e altezza
            Number.Text = "# " + ShowedPokemon.Number;
            Weight.Text = ShowedPokemon.Weight + " kg";
            Height.Text = ShowedPokemon.Height + " m";

            // Ottieni le forme del Pokémon
            pokemonForms = PokemonClassManager.Instance.GetPokeForms(ShowedPokemon.Number);
            FormListPicker.ItemsSource = pokemonForms;

            // Ottieni i tipi per le varie forme
            List<PokedexProject.Type> types = TypeClassManager.Instance.GetPokeTypes(ShowedPokemon.Number);
            foreach(PokedexProject.Type t in types)
            {
                if(t.Form == ShowedPokemon.Form)
                {
                    Type1.Content = t.Type1;
                    if(t.Type2 != null && t.Type2 != "")
                    {
                        Type2.Content = t.Type2;
                        Type2.Visibility = Visibility.Visible;
                    }
                    else
                        Type2.Visibility = Visibility.Collapsed;
                }
            }

            pokemonEvolutions = EvolutionClassManager.Instance.GetEvos(ShowedPokemon.Number);
            EvolutionListBox.ItemsSource = pokemonEvolutions;

            CreateLocationList();
        }

        private string GetImagePath()
        {
            if (ShowedPokemon.Form == "" || ShowedPokemon.Form == "Normale")
                return @"Sprites/6Gen/xy/" + ShowedPokemon.Number.ToString() + ".png";
            else if (ShowedPokemon.Form == "Femmina")
                return @"Sprites/6Gen/xy/female/" + ShowedPokemon.Number.ToString() + ".png";
            else if (ShowedPokemon.Form == "Megaevoluzione")
                return @"Sprites/6Gen/xy/" + ShowedPokemon.Number.ToString() + "-M.png";

            return @"Sprites/6Gen/xy/" + ShowedPokemon.Number.ToString() + ".png";
        }

        private void CreateLocationList()
        {
            pokemonLocations = PokemonPlaceClassManager.Instance.GetPlacesOfPokemon(ShowedPokemon.Number);
            List<Group<PokemonPlace>> places = GetItemGroups(pokemonLocations, x => x.Region);
            LocationListBox.ItemsSource = places;
        }

        private List<Group<T>> GetItemGroups<T>(IEnumerable<T> itemList, Func<T, string> getKeyFunc)
        {
            IEnumerable<Group<T>> groupList = from item in itemList
                                              group item by getKeyFunc(item) into g
                                              orderby g.Key
                                              select new Group<T>(g.Key, g);
            return groupList.ToList();
        }

        private void FormListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowedPokemon = FormListPicker.SelectedItem as Pokemon;
            SetPokemonInfo();
        }

        /// <summary>
        /// Metodo che viene attivato all'arrivo alla finestra
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("id"))
            {
                int id = int.Parse(NavigationContext.QueryString["id"]);
                ShowedPokemon = PokemonClassManager.Instance.GetPokemon(id);
                if(ShowedPokemon != null)
                {
                    SetPokemonInfo();
                }
            }
        }
    }
}