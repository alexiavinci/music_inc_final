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
    /// Logique d'interaction pour Page0.xaml
    /// </summary>
    public partial class Page0 : Page
    {
        public Page0()
        {
            InitializeComponent();
        }

        private void Gobutton(object sender, RoutedEventArgs e)
        {
            Page1 page1= new Page1();
            this.NavigationService.Navigate(page1);
        }
    }
}
