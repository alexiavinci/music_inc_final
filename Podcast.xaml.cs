﻿using System;
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
    /// Logique d'interaction pour Podcast.xaml
    /// </summary>
    public partial class Podcast : Page
    {
        public Podcast()
        {
            InitializeComponent();
        }

        private void Buy_podcast_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Buy_podcast.xaml", UriKind.Relative));
        }

        private void Librairy_podcast_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("podcast_librairy.xaml", UriKind.Relative));
        }
    }
}
