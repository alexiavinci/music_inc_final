using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LoginApp
{
    public class User
    {

        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Mail { get; set; }
        public string MotDePasse { get; set; }
        public int IdSuscription { get; set; }

        public User(int idUser, string name, string surname, int age, string mail, string motDePasse, int idSuscription)
        {
            IdUser = idUser;
            Name = name;
            Surname = surname;
            Age = age;
            Mail = mail;
            MotDePasse = motDePasse;
            IdSuscription = idSuscription;
        }
        // Method to authenticate a user based on email and password
        public static User AuthenticateUser(string connectionString, string email, string MotDePasse)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM User WHERE Mail = @Email AND mot_de_passe = @MotDePasse";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@MotDePasse", MotDePasse);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User(
                                Convert.ToInt32(reader["id_user"]),
                                Convert.ToString(reader["Name"]),
                                Convert.ToString(reader["Surname"]),
                                Convert.ToInt32(reader["Age"]),
                                Convert.ToString(reader["Mail"]),
                                Convert.ToString(reader["mot_de_passe"]),
                                Convert.ToInt32(reader["id_suscription"])
                            );
                        }
                    }
                }
            }

            // If no user is found, return null
            return null;
        }

        public static void InsertUserIntoDatabase(string connectionString, string name, string surname, int age, string mail, string motDePasse, int idSuscription)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO User (Name, Surname, Age, Mail, mot_de_passe, id_suscription) " +
                               "VALUES (@Name, @Surname, @Age, @Mail, @MotDePasse, @IdSuscription)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Surname", surname);
                    command.Parameters.AddWithValue("@Age", age);
                    command.Parameters.AddWithValue("@Mail", mail);
                    command.Parameters.AddWithValue("@MotDePasse", motDePasse);
                    command.Parameters.AddWithValue("@IdSuscription", idSuscription);

                    command.ExecuteNonQuery();
                }

                Console.WriteLine("User registered and inserted into the database successfully!");
            }
        }
    }
}