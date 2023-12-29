using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
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
    /// Logique d'interaction pour see_artists.xaml
    /// </summary>
    public partial class see_artists : Page
    {
        private const string connectionString = "server=localhost;user=root;password=root;database=MusicPlatform";
        public ObservableCollection<ArtistViewModel> artists { get; private set; }

        public see_artists()
        {
            InitializeComponent();
            LoadUserArtists();
            lvArtists.ItemsSource = artists;
        }

        private void LoadUserArtists()
        {
            artists = new ObservableCollection<ArtistViewModel>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @" SELECT a.id_artist, a.Name_artist, COUNT(upm.id_music) AS NumberOfPurchases
                                FROM Artists a
                                JOIN Music m ON a.id_artist = m.id_artist
                                JOIN user_purchase_music upm ON m.id_music = upm.id_music
                                WHERE upm.id_user = @UserId
                                GROUP BY a.id_artist, a.Name_artist;"; // afficher les artistes de la bibliothèque user et le nombre de musiques achetées

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", SessionManager.CurrentUser.IdUser);
                    using (var reader = command.ExecuteReader()) 
                    {
                        while (reader.Read())
                        {
                            var artist = new ArtistViewModel
                            {
                                Id_artist = reader.GetInt32("id_artist"),
                                Name_Artist = reader.GetString("Name_artist"),
                                Nb_songs = reader.GetInt32("NumberOfPurchases")
                            };
                            artists.Add(artist);
                        }
                    }
                }
            }
        }
    }

    public class ArtistViewModel
    {
        public int Id_artist { get; set; }
        public string Name_Artist { get; set; }
        public int Nb_songs { get; set; }
    }
}
