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
    /// Logique d'interaction pour Buy_songs.xaml
    /// </summary>
    public partial class Buy_songs : Page
    {
        private const string connectionString = "server=localhost;user=root;password=root;database=MusicPlatform";

        public ObservableCollection<SongViewModel> musics { get; private set; }
        public Buy_songs()
        {
            InitializeComponent();
            LoadSongs();
            lvSongs.ItemsSource = musics;
        }

        private void LoadSongs()
        {
            musics = new ObservableCollection<SongViewModel>();
            //string connectionString = "..."; // Replace with your connection string

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id_Music, Title, Price, Music.release_year AS Release_Year , Artists.Name_Artist AS ArtistName, Album.Album_Title AS AlbumTitle " +
                               "FROM Music " +
                               "INNER JOIN Artists ON Music.Id_Artist = Artists.Id_Artist " +
                               "INNER JOIN Album ON Music.Id_Album = Album.Id_Album";

                using (var command = new MySqlCommand(query, connection))
                {
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
                                ArtistName = reader.GetString("ArtistName"),
                                AlbumTitle = reader.GetString("AlbumTitle"),
                                BuyCommand = new BuyCommand(connectionString, SessionManager.CurrentUser.IdUser) // You need to implement ICommand interface for this
                            };
                            musics.Add(song);
                        }
                    }
                }
            }
        }
    }
    public class UserPurchase
    {
        public int UserId { get; set; }
        public int MusicId { get; set; }
        public DateTime PurchaseDate { get; set; }

        // Constructor
        public UserPurchase(int userId, int musicId, DateTime purchaseDate)
        {
            UserId = userId;
            MusicId = musicId;
            PurchaseDate = purchaseDate; 
        }

        // Method to get all user purchases from the database
        //public static List<UserPurchase> GetAllUserPurchases(string connectionString, int userId)
        //{
        //    List<UserPurchase> userPurchases = new List<UserPurchase>();

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        string query = "SELECT * FROM user_purchase_music WHERE id_user = @UserId";

        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@UserId", userId);

        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    // Assuming your UserPurchase class has a constructor that takes reader values
        //                    UserPurchase purchase = new UserPurchase(
        //                        Convert.ToInt32(reader["id_user"]),
        //                        Convert.ToInt32(reader["id_music"]),
        //                        Convert.ToDateTime(reader["purchase_date"])
        //                    );

        //                    userPurchases.Add(purchase);
        //                }
        //            }
        //        }
        //    }

        //    return userPurchases;
        //}

        //public static void MakeUserMusicPurchaseFromConsole(string connectionString, int userId)
        //{
        //    Console.WriteLine("Enter the ID of the music to purchase:");
        //    int musicId = int.Parse(Console.ReadLine());

        //    // Set the purchase date to the current date
        //    DateTime purchaseDate = DateTime.Now;

        //    UserPurchase purchase = new UserPurchase(userId, musicId, purchaseDate);
        //    AddUserMusicPurchase(connectionString, purchase);
        //    Console.WriteLine("User music purchase recorded successfully!");
        //}

        // Method to add a user music purchase to the database
        public static void AddUserMusicPurchase(string connectionString, UserPurchase purchase)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO user_purchase_music (id_user, id_music, purchase_date) " +
                               "VALUES (@UserId, @MusicId, @PurchaseDate)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", purchase.UserId);
                    command.Parameters.AddWithValue("@MusicId", purchase.MusicId);
                    command.Parameters.AddWithValue("@PurchaseDate", purchase.PurchaseDate);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
    public class SongViewModel
    {
        public int Id_Music { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime Release_Year { get; set; }
        public string ArtistName { get; set; }
        public string AlbumTitle { get; set; }
        public ICommand BuyCommand { get; set; }
    }

    public class BuyCommand : ICommand
    {
        private string connectionString;
        private int userId;


        public event EventHandler CanExecuteChanged;
        public BuyCommand(string connectionString, int userId)
        {
            this.connectionString = connectionString;
            this.userId = userId;
            
        }
        public bool CanExecute(object parameter)
        {
            return true; // Logic to determine if the command can execute
        }

        public void Execute(object parameter)
        {
            if (parameter is int musicId)
            {
                UserPurchase purchase = new UserPurchase(userId, musicId, DateTime.Now);
                AddUserMusicPurchase(connectionString, purchase);
                MessageBox.Show("Purchase successful!");
            }
        }

        public static void AddUserMusicPurchase(string connectionString, UserPurchase purchase)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO user_purchase_music (id_user, id_music, purchase_date) " +
                               "VALUES (@UserId, @MusicId, @PurchaseDate)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", purchase.UserId);
                    command.Parameters.AddWithValue("@MusicId", purchase.MusicId);
                    command.Parameters.AddWithValue("@PurchaseDate", purchase.PurchaseDate);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
