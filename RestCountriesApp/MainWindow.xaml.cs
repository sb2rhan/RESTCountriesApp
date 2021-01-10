using RestCountriesApp.Models;
using RestCountriesApp.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RestCountriesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<CountryDAO> countriesDB;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CountriesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = CountriesList.SelectedIndex;

            // make country appear on the second part of window
            CountryDAO selectedCountry = countriesDB[selectedIndex];

            CountryName.Content = selectedCountry.Name;

            CountryPopulation.Content = selectedCountry.Population;

            CountryCapital.Content = selectedCountry.Capital == string.Empty
                                        ? "No capital" : selectedCountry.Capital;

            CountryRegion.Content = selectedCountry.Region == string.Empty
                                        ? "Unknown" : selectedCountry.Region;
            try
            {
                // needs internet connection
                CountryFlag.Source = new Uri(selectedCountry.FlagPath);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            countriesDB = DataAccess.GetCountriesFromDB();

            CountriesList.ItemsSource = countriesDB;
        }
    }
}