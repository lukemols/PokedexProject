using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using PokedexProject8.Resources;
using PokedexProject;

namespace PokedexProject8
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            ProgramManager.Initialize();
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //string filePath = Windows.ApplicationModel.Package.Current.InstalledLocation.Path + @"\Files\IT\";
            //string s = "Conta:\n";
            //s += "Prog list:" + ProgramManager.pokemonList.Count.ToString() + "\n";
            //s += "Evol list:" + EvolutionClassManager.ListaEvo.Count.ToString() + "\n";
            //s += "Loca list:" + LocationClassManager.ListaLocation.Count.ToString() + "\n";
            //s += "Poke list:" + PokemonClassManager.PokemonList.Count.ToString() + "\n";
            //s += "PkPl list:" + PokemonPlaceClassManager.PlaceList.Count.ToString() + "\n";
            //s += "Type list:" + TypeClassManager.ListaTipo.Count.ToString() + "\n";
            //MessageBox.Show(s);
            NavigationService.Navigate(new Uri("/PokemonListPage.xaml", UriKind.Relative));
        }
    }
}