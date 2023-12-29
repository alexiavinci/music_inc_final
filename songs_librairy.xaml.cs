using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Logique d'interaction pour songs_librairy.xaml
    /// </summary>
    public partial class songs_librairy : Page
    {
        private const string connectionString = "server=localhost;user=root;password=root;database=MusicPlatform";

        public ObservableCollection<SongViewModel> users_songs { get; private set; }

        public songs_librairy()
        {
            InitializeComponent();
            GetAllUserPurchases();
            lvUserLibrary.ItemsSource = users_songs;
        }

        //private void LoadUserLibrary()
        //{
        //    UserLibrary = new ObservableCollection<SongViewModel>();
        //    int currentUserId = SessionManager.CurrentUser.IdUser; // Get the current user ID from session manager

        //    var userPurchases = UserPurchase.GetAllUserPurchases(connectionString, currentUserId);

        //    foreach (var purchase in userPurchases)
        //    {
        //        // Assuming you have a method to get the song details by its ID
        //        var songDetails = GetSongDetailsById(connectionString, purchase.MusicId);
        //        var songViewModel = new SongViewModel
        //        {
        //            Id_Music = reader.GetInt32("Id_Music"),
        //            Title = reader.GetString("Title"),
        //            Price = reader.GetDecimal("Price"),
        //            Release_Year = reader.GetDateTime("Release_Year"),
        //            ArtistName = reader.GetString("ArtistName"),
        //            AlbumTitle = reader.GetString("AlbumTitle"),
        //            BuyCommand = new BuyCommand(connectionString, SessionManager.CurrentUser.IdUser)
        //        };
        //        UserLibrary.Add(songViewModel);
        //    }
        //}

        //private void LoadUserSongs()
        //{
        //    int userId = SessionManager.CurrentUser.IdUser; // Assurez-vous que cela renvoie l'ID de l'utilisateur connecté

        //    using (var connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string query = @"
        //            SELECT m.id_music, m.title, m.price, m.release_year, m.id_artist, m.id_album
        //            FROM Music m
        //            INNER JOIN user_purchase_music upm ON m.id_music = upm.id_music
        //            WHERE upm.id_user = @UserId";

        //        using (var command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@UserId", userId);

        //            using (var reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    var song = new SongViewModel
        //                    {
        //                        Id_Music = reader.GetInt32("Id_Music"),
        //                        Title = reader.GetString("Title"),
        //                        Price = reader.GetDecimal("Price"),
        //                        Release_Year = reader.GetDateTime("Release_Year"),
        //                        ArtistName = reader.GetString("ArtistName"),
        //                        AlbumTitle = reader.GetString("AlbumTitle"),
        //                    };
        //                    UserSongs.Add(song);
        //                }
        //            }
        //        }
        //    }

        //    //lvUserLibrary.ItemsSource = UserSongs; // Assurez-vous que votre ListView s'appelle lvUserLibrary
        //}

        private void GetAllUserPurchases()
        {
            users_songs = new ObservableCollection<SongViewModel>();
            //List<UserPurchase> userPurchases = new List<UserPurchase>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                            SELECT up.id_user, up.id_music, up.purchase_date, m.title, m.price, 
                            a.Name_artist, al.album_title, m.release_year
                            FROM user_purchase_music up
                            JOIN Music m ON up.id_music = m.id_music
                            LEFT JOIN Artists a ON m.id_artist = a.id_artist
                            LEFT JOIN Album al ON m.id_album = al.id_album
                            WHERE up.id_user = @UserID";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", SessionManager.CurrentUser.IdUser);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var song = new SongViewModel
                            {
                                Id_Music = reader.GetInt32("Id_Music"),
                                Title = reader.GetString("Title"),
                                Price = reader.GetDecimal("Price"),
                                Release_Year = reader.GetDateTime("Release_Year"),
                                ArtistName = reader.GetString("Name_artist"),
                                AlbumTitle = reader.GetString("album_title"),
                            };
                            users_songs.Add(song);
                        }
                    }
                }
            }









            //    using (MySqlConnection connection = new MySqlConnection(connectionString))
            //{
            //    connection.Open();

            //    string query = @"
            //                SELECT up.id_user, up.id_music, up.purchase_date, m.title, m.price, 
            //                a.Name_artist, al.album_title, m.release_year
            //                FROM user_purchase_music up
            //                JOIN Music m ON up.id_music = m.id_music
            //                LEFT JOIN Artists a ON m.id_artist = a.id_artist
            //                LEFT JOIN Album al ON m.id_album = al.id_album
            //                WHERE up.id_user = @UserID";

            //    using (MySqlCommand command = new MySqlCommand(query, connection))
            //    {
            //        command.Parameters.AddWithValue("@UserID", SessionManager.CurrentUser.IdUser);
            //        using (MySqlDataReader reader = command.ExecuteReader())
            //        {


            //            while (reader.Read())
            //            {
            //                // Assuming your UserPurchase class has a constructor that takes reader values
            //                UserPurchase purchase = new UserPurchase(
            //                    Convert.ToInt32(reader["id_user"]),
            //                    Convert.ToInt32(reader["id_music"]),
            //                    Convert.ToDateTime(reader["purchase_date"])
            //                );
            //                Title = reader["title"].ToString();
            //                purchase.Price = Convert.ToDecimal(reader["price"]);
            //                purchase.ArtistName = reader.IsDBNull(reader.GetOrdinal("Name_artist")) ? null : reader["Name_artist"].ToString();
            //                purchase.AlbumTitle = reader.IsDBNull(reader.GetOrdinal("album_title")) ? null : reader["album_title"].ToString();
            //                purchase.ReleaseYear = Convert.ToDateTime(reader["release_year"]);

            //                userPurchases.Add(purchase);
            //            }
            //        }
            //    }
            //}

            //return userPurchases;
        }
    }

    
}
