using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginApp
{
    /// <summary>
    /// Logique d'interaction pour Songs.xaml
    /// </summary>
    public partial class Songs : Page
    {
        public Songs()
        {
            InitializeComponent();

        }

        private void Buy_songs_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Buy_songs.xaml", UriKind.Relative));
        }

        private void Librairy_songs_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("songs_librairy.xaml", UriKind.Relative));
        }
    }
}
