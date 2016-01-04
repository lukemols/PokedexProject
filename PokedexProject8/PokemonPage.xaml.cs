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

        public PokemonPage()
        {
            InitializeComponent();

        }

        void SetPokemonInfo()
        {
            // Metti il nome del Pokémon come titolo
            TitlePage.Text = ShowedPokemon.Name;
            // Mostra l'immagine
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"Sprites/6Gen/xy/" + ShowedPokemon.Number.ToString() + ".png", UriKind.Relative));
            PokemonImage.Fill = ib;
            // Scrivi numero, peso e altezza
            Number.Text = "# " + ShowedPokemon.Number;
            Weight.Text = ShowedPokemon.Weight + " kg";
            Height.Text = ShowedPokemon.Height + " m";

            List<Pokemon> forms = PokemonClassManager.GetPokeForms(ShowedPokemon.Number);
            FormListPicker.ItemsSource = forms;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("id"))
            {
                int id = int.Parse(NavigationContext.QueryString["id"]);
                ShowedPokemon = PokemonClassManager.GetPokemon(id);
                if(ShowedPokemon != null)
                {
                    SetPokemonInfo();
                }
            }
        }
    }
}