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
    /// Logique d'interaction pour podcast_librairy.xaml
    /// </summary>
    public partial class podcast_librairy : Page
    {
        private const string connectionString = "server=localhost;user=root;password=root;database=MusicPlatform";
        public ObservableCollection<PodcastViewModel> users_podcasts { get; private set; }

        public podcast_librairy()
        {
            InitializeComponent();
            GetAllUserPurchasePdc();
            lvUserPodcasts.ItemsSource = users_podcasts;
        }

        private void GetAllUserPurchasePdc()
        {
            users_podcasts = new ObservableCollection<PodcastViewModel>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @" SELECT up.id_podcast, up.purchase_date, 
                                p.Subject, p.Price, a.name_author
                                FROM user_purchase_podcast up
                                JOIN Podcast p ON up.id_podcast = p.id_podcast
                                LEFT JOIN Author a ON p.id_author = a.id_author
                                WHERE up.id_user = @UserID";
                    
                    
                //    @"
                //SELECT p.id_podcast, p.Subject, p.Price, a.name_author
                //FROM user_purchase_podcast up
                //JOIN Author a ON p.id_author = a.id_author
                //WHERE p.id_user = @UserID";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", SessionManager.CurrentUser.IdUser);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var podcast = new PodcastViewModel
                            {
                                Id_Podcast = reader.GetInt32("id_podcast"),
                                Subject = reader.GetString("Subject"),
                                AuthorName = reader.GetString("name_author"),
                                Price = reader.GetDecimal("Price")
                            };
                            users_podcasts.Add(podcast);
                        }
                    }
                }
            }
        }

    }
}
