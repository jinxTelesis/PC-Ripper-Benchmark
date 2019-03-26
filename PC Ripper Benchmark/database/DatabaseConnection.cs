﻿using PC_Ripper_Benchmark.exception;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace PC_Ripper_Benchmark.database {

    /// <summary>
    /// The <see cref="DatabaseConnection"/> class.
    /// <para> </para>
    /// Represents functions for connecting to the
    /// online Azure DB.
    /// <para>Author: David Hartglass
    /// all rights reserved.</para>
    /// </summary>

    public class DatabaseConnection {

        /// <summary>
        /// A <see cref="DatabaseConnection"/> instance
        /// used to create a database connection.
        /// All user data must be passed as parameters. 
        /// </summary>

        public SqlConnection Connection { get; set; }

        /// <summary>
        /// Default constructor. Sets the connection string.
        /// </summary>

        public DatabaseConnection(string connectionString) {
            this.Connection = new SqlConnection {
                ConnectionString = connectionString
            };
        }

        /// <summary>
        /// Opens the SQL database connection.
        /// </summary>
        /// <exception cref="RipperDatabaseException"></exception>

        public void Open() {
            try {
                this.Connection.Open();
            } catch {
                throw new RipperDatabaseException("Failed to open connection.");
            }
        }

        /// <summary>
        /// Member function that adds a user to the database.
        /// Takes a connection, and the required fields to
        /// insert into the database using a stored
        /// procedure
        /// </summary>

        public bool AddUserToDatabase(SqlConnection connection, util.UserData user) {
            try {
                SqlCommand addUser = new SqlCommand("UserAdd", connection) {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                addUser.Parameters.AddWithValue("@FirstName", user.FirstName.Trim());
                addUser.Parameters.AddWithValue("@LastName", user.LastName.Trim());
                addUser.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber.Trim());
                addUser.Parameters.AddWithValue("@Email", user.Email.Trim());
                addUser.Parameters.AddWithValue("@Password", user.Password.Trim());

                addUser.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Registration Successful");

                return true;
            } catch (SqlException e) {
                Clipboard.SetText(e.ToString());
                MessageBox.Show($"An account with that email already exists! {e.ToString()}");
                return false;
            }
        }

        /// <summary>
        /// Member function that checks if an 
        /// email exists in the database.
        /// The stored procedure will return 
        /// a 0 or 1 to determine if it exists.
        /// </summary>

        public bool CheckAccountExists(SqlConnection connection, string email, string password) {
            try {
                connection.Open();

                util.Encryption encrypter = new util.Encryption();

                SqlCommand checkAccount = new SqlCommand("SELECT * FROM Customer where Email=@Email and Password=@Password", connection);
                email = encrypter.EncryptText(email.ToUpper().Trim());
                password = encrypter.EncryptText(password.ToUpper().Trim());

                checkAccount.Parameters.AddWithValue("@Email", email);
                checkAccount.Parameters.AddWithValue("@Password", password);
                checkAccount.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(checkAccount);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                int count = ds.Tables[0].Rows.Count;

                if (count == 1) {
                    window.MainWindow mainWindow = new window.MainWindow();
                    mainWindow.Show();
                    connection.Close();
                    return true;
                } else {
                    MessageBox.Show("Invalid login!");
                    connection.Close();
                    return false;
                }
            } catch (Exception e) {
                MessageBox.Show($"Oh no. A RipperDatabaseException occured. {e.ToString()}");
                return false;
            }
        }
    }
}
