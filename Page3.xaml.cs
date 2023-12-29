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
    /// Logique d'interaction pour Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        public void Songs_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Songs.xaml", UriKind.Relative));
        }

        public void Podcast_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Podcast.xaml", UriKind.Relative));
        }
        public void Album_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Album.xaml", UriKind.Relative));
        }
        public void Artists_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Artists.xaml", UriKind.Relative));
        }


    }
}
