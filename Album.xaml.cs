using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour Album.xaml
    /// </summary>
    public partial class Album : Page
    {
        public Album()
        {
            InitializeComponent();
        }

        private void Buy_album_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Buy_album.xaml", UriKind.Relative));
        }

        private void Librairy_album_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Album_librairy.xaml", UriKind.Relative));
        }
    }
}
