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
    /// Logique d'interaction pour Artists.xaml
    /// </summary>
    public partial class Artists : Page
    {
        public Artists()
        {
            InitializeComponent();
        }

        private void See_Artists_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("see_artists.xaml", UriKind.Relative));
        }

        
    }
}
