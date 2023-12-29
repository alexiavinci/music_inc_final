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
    /// Logique d'interaction pour Buy_album.xaml
    /// </summary>
    public partial class Buy_album : Page
    {
        public string connectionString = "server=localhost;user=root;password=root;database=MusicPlatform";

        public ObservableCollection<AlbumViewModel> Albums { get; private set; }

        public Buy_album()
        {
            InitializeComponent();
            LoadAlbums();
            lvAlbums.ItemsSource = Albums;

        }

        private void LoadAlbums()
        {
            Albums = new ObservableCollection<AlbumViewModel>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT a.id_album, a.album_title, a.release_year, ar.Name_artist, s.style_name
                                FROM Album a JOIN Artists ar ON a.id_artist = ar.id_artist
                                LEFT JOIN STYLE s ON a.id_style = s.id_style";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader =  command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var album = new AlbumViewModel
                            {
                                Id_Album = reader.GetInt32("id_album"),
                                Album_Title = reader.GetString("album_title"),
                                ReleaseYear = reader.GetDateTime("release_year"),
                                ArtistName = reader.GetString("Name_artist"),
                                Style = reader.GetString("style_name")
                            };
                            Albums.Add(album);
                        }
                    }
                }
            }
        }
    }

    public class AlbumViewModel
    {
        public int Id_Album { get; set; }
        public string Album_Title { get; set; }
        public DateTime ReleaseYear { get; set; }
        public string ArtistName { get; set; }
        public string Style { get; set; }



    }
}
