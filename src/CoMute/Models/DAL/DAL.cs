using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CoMute.Web.Models.Dto;
using CoMute.Web.Controllers.Web;

namespace CoMute.Web.Models.DAL
{
    public class DAL
    {
        //Local connection String
        public static string connectionS = "Data Source=DESKTOP-7F27R3N;Initial Catalog=Co_Mute_DB;Integrated Security=True";

        
        public static void RegisterUser( string password, string email, string name, string surname, string phoneNumber)
        {
            //Using the SqlConnection with the connection string.
            using (SqlConnection con = new SqlConnection(connectionS))
            {
                //The command that implements the storage procedure that adds values to  User table
                SqlCommand cmd = new SqlCommand("AddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //add the values with the parameters given in the stored procedure
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@EmailAddress", email);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Surname", surname);
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                //open connection
                con.Open();
                //execute the stored procedure
                cmd.ExecuteNonQuery();
                //close the connection
                con.Close();
            }
        }

        public static User getUser(string email)
        {
            User user = null;
            //Using the SqlConnection with the connection string.
            using (SqlConnection con = new SqlConnection(connectionS))
            {

                SqlCommand sqlCmd = new SqlCommand("SELECT UserID, Name, Surname, PhoneNumber, EmailAddress FROM [User] WHERE EmailAddress = @EmailAddress", con);

                //sqlCmd parameters that will first add the attribute and the datatype of it in SQL Server. Then equate the value of that attribute to the value in this program.
                sqlCmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar);
                sqlCmd.Parameters["@EmailAddress"].Value = email;
                con.Open();//Open the connection
                SqlDataReader reader = sqlCmd.ExecuteReader();//Execute the reader.
                //While the reader reads the output for each row.
                while (reader.Read())
                {
                    user.Name = reader["Name"].ToString();
                    user.Surname = reader["Surname"].ToString();
                    user.PhoneNumber = reader["PhoneNumber"].ToString();
                    user.EmailAddress = reader["EmailAddress"].ToString();
                }
                reader.Close();//Close the reader
                con.Close();//close the connection
            }
            UserController.currentUser = user;
            //return the user object
            return user;
        }


        //Method to check the user and password
        public static bool checkPassword(string email, string password)
        {
            bool bool1 = new Boolean();//Delcare a new Boolean variable bool1.

            //Using the SqlConnection with the connection string.
            using (SqlConnection con = new SqlConnection(connectionS))
            {
                bool1 = false;
                SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE EmailAddress = @EmailAddress AND Password = HASHBYTES('SHA2_512', @Password)", con);//The commmand sql that implements the query  that counts the row where the userID and password match with the parameters given to the database.


                //sqlCmd parameters that will first add the attribute and the datatype of it in SQL Server. Then equate the value of that attribute to the value in this program.
                sqlCmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar);
                sqlCmd.Parameters["@EmailAddress"].Value = email;
                sqlCmd.Parameters.Add("@Password", SqlDbType.VarChar);
                sqlCmd.Parameters["@Password"].Value = password;

                con.Open();//Open the connection
                int isCorrectPassword = (int)sqlCmd.ExecuteScalar();//Execute and  retun a single row of the output from the query and convert it to int.
                //if isCorrectPassword is equal to 1 then 
                if (isCorrectPassword == 1)
                {
                    //Make bool1 be equal to true.
                    bool1 = true;
                }
                //Otherwise
                else
                {
                    //Make bool1 be equal to false.
                    bool1 = false;
                }
                con.Close();//Close the connection.
            }
            return bool1;//return bool1 value.
        }

        public static void registerCarPool(registerCarPoolRequest registerCarPoolRequest)
        {
            //Using the SqlConnection with the connection string.
            using (SqlConnection con = new SqlConnection(connectionS))
            {
                //The command that implements the storage procedure that adds values to  User table
                SqlCommand cmd = new SqlCommand("AddCarPool", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //add the values with the parameters given in the stored procedure
                cmd.Parameters.AddWithValue("@Origin", registerCarPoolRequest.origin);
                cmd.Parameters.AddWithValue("@DaysAvailable", registerCarPoolRequest.daysAvailable.ToString());
                cmd.Parameters.AddWithValue("@EmailAddress", registerCarPoolRequest.destination);
                cmd.Parameters.AddWithValue("@DepartureTime", registerCarPoolRequest.departureTime);
                cmd.Parameters.AddWithValue("@ArrivalTime", registerCarPoolRequest.arrivalTime);
                cmd.Parameters.AddWithValue("@UserID", registerCarPoolRequest.owner);
                cmd.Parameters.AddWithValue("@NumOfAvailableSeats", registerCarPoolRequest.numOfAvailableSeats);
                cmd.Parameters.AddWithValue("@Notes", registerCarPoolRequest.notes);
                //open connection
                con.Open();
                //execute the stored procedure
                cmd.ExecuteNonQuery();
                //close the connection
                con.Close();
            }
        }

        public static void displayCarPools()
        {
            //Using the SqlConnection with the connection string.
            using (SqlConnection con = new SqlConnection(connectionS))
            {
                //A string command of the query to be executed
                string command = "SELECT * FROM CarPool";

                SqlCommand sqlCmd = new SqlCommand(command, con);//Sql command that will implement the query shown on top with the connection to the database.


                //sqlCmd parameters that will first add the attribute and the datatype of it in SQL Server. Then equate the value of that attribute to the value in this program.

                con.Open();//Open the connection
                SqlDataReader reader = sqlCmd.ExecuteReader();//Execute the reader
                //While the reader reads the output for each row.
                while (reader.Read())
                {
                    Car_Pool carPool = new Car_Pool();

                    carPool.carPoolID = Convert.ToInt32(reader["CarPoolID"].ToString());
                    carPool.departureTime = Convert.ToDateTime(reader["DepartureTime"].ToString());
                    carPool.origin = reader["Origin"].ToString();
                    carPool.destination = reader["Destination"].ToString();
                    carPool.arrivalTime = Convert.ToDateTime(reader["ArrivalTime"].ToString());
                    carPool.numOfAvailableSeats = Convert.ToInt32(reader["NumOfAvailableSeats"].ToString());

                    Car_Pool.carPools.Add(carPool);//Add the Module to the ModulesList.
                }
                reader.Close();//close the reader
                con.Close();//close connection
            }
        }
    }
}