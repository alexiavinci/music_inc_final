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
    /// Logique d'interaction pour Buy_podcast.xaml
    /// </summary>
    public partial class Buy_podcast : Page
    {
        public string connectionString = "server=localhost;user=root;password=root;database=MusicPlatform" ;

        public ObservableCollection<PodcastViewModel> Podcasts { get; private set; }

        public Buy_podcast()
        {
            InitializeComponent();
            LoadPodcasts();
            lvPodcasts.ItemsSource = Podcasts; // Assurez-vous que la ListView dans votre XAML s'appelle lvPodcasts
        }

        private void LoadPodcasts()
        {
            Podcasts = new ObservableCollection<PodcastViewModel>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                SELECT p.id_podcast, p.Subject, p.Price, a.name_author
                FROM Podcast p
                JOIN Author a ON p.id_author = a.id_author";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var podcast = new PodcastViewModel
                            {
                                Id_Podcast = reader.GetInt32("id_podcast"),
                                Subject = reader.GetString("Subject"),
                                AuthorName = reader.GetString("name_author"),
                                Price = reader.GetDecimal("Price"),
                                BuyCommandPdc = new BuyCommandPdc(connectionString, SessionManager.CurrentUser.IdUser)
                            };
                            Podcasts.Add(podcast);
                        }
                    }
                }
            }
        }
    }

    public class UserPurchasePdcs
    {
        public int UserId { get; set; }
        public int PodcastId { get; set; } // Since it cannot be null
        public DateTime PurchaseDate { get; set; }


        // propriétés pour afficher les infos avec jointures aux autres classes
        //public string Author { get; set; }
        //public string Subject { get; set; }
        //public decimal Price { get; set; }



        // Constructor
        public UserPurchasePdcs(int userId, int podcastId, DateTime purchaseDate)
        {
            UserId = userId;
            PodcastId = podcastId;
            PurchaseDate = purchaseDate;
        }

        // Method to get all user purchases from the database
        //public static List<UserPurchasePdcs> GetAllUserPodcastPurchases(string connectionString, int userID)
        //{
        //    List<UserPurchasePdcs> userPodcastPurchases = new List<UserPurchasePdcs>();

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        //string query = "SELECT * FROM user_purchase_podcast WHERE id_user = @UserID";

        //        string query = @"
        //                SELECT up.id_user, up.id_podcast, up.purchase_date, p.Subject, p.Price, a.name_author
        //                FROM user_purchase_podcast up
        //                JOIN Podcast p ON up.id_podcast = p.id_podcast
        //                LEFT JOIN Author a ON p.id_author = a.id_author
        //                WHERE up.id_user = @UserID";


        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@UserID", userID);

        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    // Assuming your UserPurchase class has a constructor that takes reader values
        //                    UserPurchasePdcs podcastPurchase = new UserPurchasePdcs(
        //                        Convert.ToInt32(reader["id_user"]),
        //                        Convert.ToInt32(reader["id_podcast"]),
        //                        Convert.ToDateTime(reader["purchase_date"])
        //                    );

        //                    podcastPurchase.Subject = reader["Subject"].ToString();
        //                    podcastPurchase.Author = reader["name_author"].ToString();
        //                    podcastPurchase.Price = Convert.ToDecimal(reader["Price"]);

        //                    userPodcastPurchases.Add(podcastPurchase);
        //                }
        //            }
        //        }
        //    }

        //    return userPodcastPurchases;
        //}

        //public static List<UserPurchasePdcs> GetAllUserPodcastPurchases(string connectionString, int userID)
        //{
        //    List<UserPurchasePdcs> userPodcastPurchases = new List<UserPurchasePdcs>();

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        //string query = "SELECT * FROM user_purchase_podcast WHERE id_user = @UserID";

        //        string query = @"
        //                SELECT up.id_user, up.id_podcast, up.purchase_date, p.Subject, p.Price, a.name_author
        //                FROM user_purchase_podcast up
        //                JOIN Podcast p ON up.id_podcast = p.id_podcast
        //                LEFT JOIN Author a ON p.id_author = a.id_author
        //                WHERE up.id_user = @UserID";


        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@UserID", userID);

        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    // Assuming your UserPurchase class has a constructor that takes reader values
        //                    UserPurchasePdcs podcastPurchase = new UserPurchasePdcs(
        //                        Convert.ToInt32(reader["id_user"]),
        //                        Convert.ToInt32(reader["id_podcast"]),
        //                        Convert.ToDateTime(reader["purchase_date"])
        //                    );

        //                    podcastPurchase.Subject = reader["Subject"].ToString();
        //                    podcastPurchase.Author = reader["name_author"].ToString();
        //                    podcastPurchase.Price = Convert.ToDecimal(reader["Price"]);

        //                    userPodcastPurchases.Add(podcastPurchase);
        //                }
        //            }
        //        }
        //    }

        //    return userPodcastPurchases;
        //}

        public static void AddUserPodcastPurchase(string connectionString, UserPurchasePdcs purchase)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query1 = "INSERT INTO user_purchase_podcast (id_user, id_podcast, purchase_date) " +
                               "VALUES (@UserId, @PodcastId, @PurchaseDate)";

                using (var command = new MySqlCommand(query1, connection))
                {
                    command.Parameters.AddWithValue("@UserId", purchase.UserId);
                    command.Parameters.AddWithValue("@PodcastId", purchase.PodcastId);
                    command.Parameters.AddWithValue("@PurchaseDate", purchase.PurchaseDate);

                    command.ExecuteNonQuery();
                }
            }
        }
    }

    public class PodcastViewModel
    {
        public string connectionString = "server=localhost;user=root;password=root;database=MusicPlatform";
        public int Id_Podcast { get; set; }
        public string Subject { get; set; }
        public decimal Price { get; set; }
        public string AuthorName { get; set; }
        public ICommand BuyCommandPdc { get; set; }
    }

        //public PodcastViewModel()
        //{
        //    BuyCommand = new RelayCommand(ExecuteBuyCommand);
        //}

        //private void ExecuteBuyCommand(object parameter)
        //{
        //    // L'implémentation de la commande d'achat va ici
        //    UserPurchasePdcs purchase = new UserPurchasePdcs(user)
        //    MessageBox.Show($"Podcast {Id_Podcast} bought!");
        //    UserPurchasePdcs.AddUserPodcastPurchase(connectionString, purchase);

        //}

    public class BuyCommandPdc : ICommand
    {
        private string connectionString;
        private int userId;


        public event EventHandler CanExecuteChanged;
        public BuyCommandPdc(string connectionString, int userId)
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
            if (parameter is int podcastId)
            {
                UserPurchasePdcs purchase = new UserPurchasePdcs(userId, podcastId, DateTime.Now);
                AddUserPodcastPurchase(connectionString, purchase);
                MessageBox.Show("Purchase successful!");
            }
        }

        public static void AddUserPodcastPurchase(string connectionString, UserPurchasePdcs purchase)
        {

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO user_purchase_podcast (id_user, id_podcast, purchase_date) " +
                               "VALUES (@UserId, @PodcastId, @PurchaseDate)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", purchase.UserId);
                    command.Parameters.AddWithValue("@PodcastId", purchase.PodcastId);
                    command.Parameters.AddWithValue("@PurchaseDate", purchase.PurchaseDate);

                    command.ExecuteNonQuery();
                }
            }
            //using (MySqlConnection connection = new MySqlConnection(connectionString))
            //{
            //    connection.Open();

            //    string query = "INSERT INTO user_purchase_podcast (id_user, id_podcast, purchase_date) " +
            //                   "VALUES (@UserId, @PodcastId, @PurchaseDate)";

            //    using (MySqlCommand command = new MySqlCommand(query, connection))
            //    {
            //        command.Parameters.AddWithValue("@UserId", purchase.UserId);
            //        command.Parameters.AddWithValue("@PodcastId", purchase.PodcastId);
            //        command.Parameters.AddWithValue("@PurchaseDate", purchase.PurchaseDate);

            //        command.ExecuteNonQuery();
            //    }
            //}
        }
    }

    //public class RelayCommand : ICommand
    //{
    //    private Action<object> execute;
    //    private Func<object, bool> canExecute;

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add { CommandManager.RequerySuggested += value; }
    //        remove { CommandManager.RequerySuggested -= value; }
    //    }

    //    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    //    {
    //        this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
    //        this.canExecute = canExecute;
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        return canExecute == null || canExecute(parameter);
    //    }

    //    public void Execute(object parameter)
    //    {
    //        execute(parameter);
    //    }
    //}


    
}
