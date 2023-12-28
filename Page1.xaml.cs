using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LoginApp
{
    public partial class Page1 : Page
    {
        private bool isDragging = false;
        private Point startPoint;
        public Page1()
        {
            InitializeComponent();
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void textPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password))
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Votre chaîne de connexion à la base de données
            string connectionString = "server=localhost;user=root;password=root;database=MusicPlatform";
;

            // Récupérer les valeurs des TextBox et PasswordBox
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            // Effectuer l'authentification
            try
            {
                var user = User.AuthenticateUser(connectionString, email, password);
                if (user != null)
                {
                    MessageBox.Show("Successfully signed in!");
                }
                else
                {
                    MessageBox.Show("Email or password is incorrect");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isDragging = true;
                startPoint = e.GetPosition(null);
            }
        }


        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUpPage = new SignUp();
            this.Content = signUpPage; 
        }

        private void SignUpButton(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            this.NavigationService.Navigate(signUp);
        }
    }
}
