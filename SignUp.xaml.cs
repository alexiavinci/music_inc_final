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
    /// Logique d'interaction pour SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        private bool isDragging = false;
        private Point startPoint;
        public SignUp()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isDragging = true;
                startPoint = e.GetPosition(null);
            }
        }

        private void Backbutton(object sender, RoutedEventArgs e)
        {
            Page1 pa1 = new Page1();
            this.NavigationService.Navigate(pa1);
        }

        private void CreateButton(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=localhost;user=root;password=root;database=MusicPlatform";

            // Récupération des valeurs des champs
            string name = txtName.Text;
            string surname = txtSurname.Text;
            int age = Convert.ToInt32(txtAge.Text);  // Assurez-vous de gérer les exceptions pour les conversions!
            string mail = txtmail.Text;
            string password = txtPassword.Password;
            int subscriptionId = Convert.ToInt32((subscriptionComboBox.SelectedItem as ComboBoxItem).Tag);

            // Enregistrement de l'utilisateur dans la base de données
            try
            {
                User.InsertUserIntoDatabase(connectionString, name, surname, age, mail, password, subscriptionId);
                MessageBox.Show("Inscription réussie!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'inscription: " + ex.Message);
            }
        }
    }
    
}
