using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PokedexProject;

namespace PokedexProject8
{
    public partial class PokemonListPage : PhoneApplicationPage
    {
        public PokemonListPage()
        {
            InitializeComponent();
            PokemonListBox.ItemsSource = ProgramManager.PokemonList;
        }

        private void PokemonListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PokemonListBox.SelectedItem == null)
                return;
            Pokemon p = (Pokemon)PokemonListBox.SelectedItem;
            PokemonListBox.SelectedItem = null;

            NavigationService.Navigate(new Uri("/PokemonPage.xaml?id=" + p.Number, UriKind.Relative));
        }
    }
}